﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.Collections;
using SQLMonitoring.DatabaseConnection;
using SQLMonitoring.Model;
using SQLMonitoring.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Controllers
{
    public class ReportController : Controller
    {
        public IConfiguration _configuration;

        public DatabaseContext _db;

        public ReportController(DatabaseContext db, IConfiguration config)
        {
            _db = db;
            _configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void UploadBasicResourceUsage(string Timestamp, string Server, string Cpu, string Memory, string Network, string Connections, string BatchRequests)
        {
            BasicResourceUsage stats = new BasicResourceUsage();

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ServerName = Server;
            stats.CpuUsage = Double.Parse(Cpu);
            stats.MemoryUsage = Double.Parse(Memory);
            stats.NetworkUsage = Double.Parse(Network);
            stats.UserConnections = Double.Parse(Connections);
            stats.BatchRequests = Double.Parse(BatchRequests);

            _db.BasicResourceUsageStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpGet]
        public void GenerateBasicInformation(string serverName)
        {
            byte[] userIdByteArray;
            HttpContext.Session.TryGetValue("Id", out userIdByteArray);
            int userId = BitConverter.ToInt32(userIdByteArray);

            var server = _db.Servers.Where(srv => srv.ServerName == serverName).FirstOrDefault();
            var connectionStringTemplate = _configuration.GetValue<string>("Templates:ServerConnectionString");
            
            DateTime date = DateTime.Now;
            BasicInformation basicInformation = new BasicInformation();
            basicInformation.ServerName = serverName;
            basicInformation.Date = date;

            SqlConnection connection = null;
            SqlCommand cmd = null;
            SqlDataReader dataReader = null;

            try
            {
                var connectionString = string.Format(
                    connectionStringTemplate,
                    server.ServerName,
                    "master",
                    server.UserName,
                    CryptographyService.DecryptString(server.Password));

                connection = new SqlConnection(connectionString);
                connection.Open();

                // Server Version
                //
                cmd = new SqlCommand(QueryCollection.ServerVersion, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.ServerVersion = (string)dataReader.GetValue(0);
                }
                dataReader.Close();

                // Server Edition
                //
                cmd = new SqlCommand(QueryCollection.ServerEdition, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.Edition = (string)dataReader.GetValue(0);
                }
                dataReader.Close();

                // HADR
                //
                cmd = new SqlCommand(QueryCollection.IsHadrEnabled, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.IsHADREnabled = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();

                // XTP
                //
                cmd = new SqlCommand(QueryCollection.IsXTPEnable, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.IsXTPEnabled = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();

                // Collation
                //
                cmd = new SqlCommand(QueryCollection.Collation, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.Collation = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();

                // Compat level
                //
                cmd = new SqlCommand(QueryCollection.CompatLevel, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.CompatLevel = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();

                // Basic VM info
                //
                cmd = new SqlCommand(QueryCollection.BaseVmInfo, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.ProcessorCount = dataReader.GetValue(0).ToString();
                    basicInformation.Memory = dataReader.GetValue(1).ToString();
                    basicInformation.HyperThreadRatio = dataReader.GetValue(2).ToString();
                    basicInformation.WorkerCount = (int) dataReader.GetValue(3);
                }
                dataReader.Close();

                // Databases
                //
                List<Database> databaseList = new List<Database>();
                cmd = new SqlCommand(QueryCollection.DatabaseInfo, connection);
                dataReader = cmd.ExecuteReader();
                while(dataReader.Read())
                {
                    Database database = new Database();

                    database.DatabaseId = (int) dataReader.GetValue(0);
                    database.Name = (string) dataReader.GetValue(1);
                    database.State = (string) dataReader.GetValue(2);
                    database.SnapshotIsolationLevel = (string)dataReader.GetValue(3);
                    database.RecoveryModel = (string)dataReader.GetValue(4);
                    database.IsEncrypted = dataReader.GetValue(5).ToString();

                    var dbConnectionString = string.Format(
                        connectionStringTemplate,
                        server.ServerName,
                        "'" + database.Name + "'",
                        server.UserName,
                        CryptographyService.DecryptString(server.Password));

                    SqlConnection dbConnection = new SqlConnection(connectionString);
                    var dbSizeQuery = string.Format(
                            QueryCollection.DatabaseSizes,
                            database.Name);

                    dbConnection.Open();
                    SqlCommand additionalInfoCmd = new SqlCommand(dbSizeQuery, dbConnection);
                    SqlDataReader dataReaderAdditional = null;

                    dataReaderAdditional = additionalInfoCmd.ExecuteReader();

                    while (dataReaderAdditional.Read())
                    {
                        database.DatabaseSize = dataReaderAdditional.GetValue(0).ToString();

                    }

                    dataReaderAdditional.Close();
                    dbConnection.Close();

                    databaseList.Add(database);
                }
                dataReader.Close();
                basicInformation.Databases = databaseList;

                // Database files
                //
                List<DatabaseFile> databaseFiles = new List<DatabaseFile>();
                cmd = new SqlCommand(QueryCollection.DatabaseFiles, connection);
                dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    DatabaseFile file = new DatabaseFile();
                    file.Name = dataReader.GetValue(0).ToString();
                    file.Location = dataReader.GetValue(1).ToString();
                    file.Db = databaseList.Where(db => db.Id == (int)dataReader.GetValue(2)).FirstOrDefault();
                    file.State = dataReader.GetValue(3).ToString();
                    file.Type = dataReader.GetValue(4).ToString();
                    file.Size = Double.Parse(dataReader.GetValue(5).ToString());
                    file.MaxSize = dataReader.GetValue(6).ToString();
                    file.AutoGrowth = dataReader.GetValue(7).ToString();

                    databaseFiles.Add(file);
                }
                dataReader.Close();
                basicInformation.DatabaseFiles = databaseFiles;

                _db.BasicInformationStats.Add(basicInformation);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }

                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
        }
    }
}
