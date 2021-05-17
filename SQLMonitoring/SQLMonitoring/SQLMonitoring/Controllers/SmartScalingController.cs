using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.DatabaseConnection;
using SQLMonitoring.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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

            return View("../SmartScaling/Home", servers);
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
            }


            _db.SmartPredictionStats.Add(stats);
            _db.SaveChanges();
        }
    }
}
