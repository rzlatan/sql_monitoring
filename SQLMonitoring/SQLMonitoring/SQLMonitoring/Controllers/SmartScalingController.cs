﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.DatabaseConnection;
using SQLMonitoring.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using System.IO;
using CsvHelper;
using System.Data;

namespace SQLMonitoring.Controllers
{
    public class SmartScalingController : Controller
    {
        public IConfiguration _configuration;

        public DatabaseContext _db;

        public SmartScalingController(DatabaseContext db, IConfiguration config)
        {
            _db = db;
            _configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Home()
        {
            byte[] userIdByteArray;
            HttpContext.Session.TryGetValue("Id", out userIdByteArray);
            int userId = BitConverter.ToInt32(userIdByteArray);

            var servers = _db.Servers.Include(srv => srv.Owner).Where(server => server.Owner.Id == userId);

            return View("../SmartScaling/Home", servers);
        }

        [HttpGet]
        public IActionResult Check(string server)
        {
            byte[] userIdByteArray;
            HttpContext.Session.TryGetValue("Id", out userIdByteArray);
            int userId = BitConverter.ToInt32(userIdByteArray);
            ViewBag.SmartStatsErrorMessage = string.Empty;
        
            var servers = _db.Servers.Include(srv => srv.Owner).Where(server => server.Owner.Id == userId);

            if (server == null)
            {
                ViewBag.SmartStatsErrorMessage = "You haven't selected any server from the list.";
                return View("../SmartScaling/Home", servers);
            }

            var serverObject = servers.Where(srv => srv.ServerName == server).FirstOrDefault();

            if (serverObject == null || !serverObject.SmartPredictionsEnabled)
            {
                ViewBag.SmartStatsErrorMessage = "Server is not  configured for smart scaling. Please read the documentation and execute the provided script from the client machine to enable smart prediction";
                return View("../SmartScaling/Home", servers);
            }

            var ResultGuid = PredictScalingNeeds(server);

            return View("SmartScalingServer", ResultGuid);
        }

        public Guid PredictScalingNeeds(string ServerName)
        {
            Guid result = Guid.NewGuid();

            GetTrainingData(ServerName, result);

            return result;
        }

        public void GetTrainingData(string ServerName, Guid guid)
        {
            var stats = _db.SmartPredictionStats.Where(server => server.ServerName == ServerName).
                            OrderByDescending(server => server.Timestamp).Take(200).ToList();

            DataTable cpuDT = new DataTable();
            cpuDT.Columns.Add("Day", typeof(int));
            cpuDT.Columns.Add("Hour", typeof(int));
            cpuDT.Columns.Add("Minute", typeof(int));
            cpuDT.Columns.Add("CPU", typeof(double));

            DataTable memoryDT = new DataTable();
            memoryDT.Columns.Add("Day", typeof(int));
            memoryDT.Columns.Add("Hour", typeof(int));
            memoryDT.Columns.Add("Minute", typeof(int));
            memoryDT.Columns.Add("Memory", typeof(double));

            DataTable networkDT = new DataTable();
            networkDT.Columns.Add("Day", typeof(int));
            networkDT.Columns.Add("Hour", typeof(int));
            networkDT.Columns.Add("Minute", typeof(int));
            networkDT.Columns.Add("Netwokr", typeof(double));

            foreach (var result in stats)
            {
                cpuDT.Rows.Add(result.Day, result.Hour, result.Minute, result.CpuUsage);
                memoryDT.Rows.Add(result.Day, result.Hour, result.Minute, result.MemoryUsage);
                networkDT.Rows.Add(result.Day, result.Hour, result.Minute, result.NetworkUsage);
            }

            var cpuFilePath = Directory.GetCurrentDirectory() + "/" + guid + "/smart_scaling_cpu.csv";
            var memoryFilePath = Directory.GetCurrentDirectory() + "/" + guid + "/smart_scaling_memory.csv";
            var networkFilePath = Directory.GetCurrentDirectory() + "/" + guid + "/smart_scaling_network.csv";

            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + guid);

            System.IO.File.Create(cpuFilePath).Close();
            System.IO.File.Create(memoryFilePath).Close();
            System.IO.File.Create(networkFilePath).Close();

            ToCSV(cpuDT, cpuFilePath);
            ToCSV(memoryDT, memoryFilePath);
            ToCSV(networkDT, networkFilePath);
        }


        public static void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);

            // Headers
            //
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            
            // Data
            //
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        [HttpPost]
        public void UploadStats(string Timestamp, string Server, string Cpu, string Memory, string Network, string Disk)
        {
            SmartPredictionStats stats = new SmartPredictionStats();

            stats.Timestamp = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.Day = (int)stats.Timestamp.DayOfWeek;
            stats.Hour = stats.Timestamp.Hour;
            stats.Minute = stats.Timestamp.Minute;

            stats.ServerName = Server;
            stats.CpuUsage = Double.Parse(Cpu);
            stats.MemoryUsage = Double.Parse(Memory);
            stats.NetworkUsage = Double.Parse(Network);
            stats.DiskLatency = Double.Parse(Disk);

            var srv = _db.Servers.Where(server => server.ServerName == Server).FirstOrDefault();
            if (srv.SmartPredictionsEnabled == false)
            {
                srv.SmartPredictionsEnabled = true;
                _db.SaveChanges();
            }

            _db.SmartPredictionStats.Add(stats);
            _db.SaveChanges();
        }
    }
}
