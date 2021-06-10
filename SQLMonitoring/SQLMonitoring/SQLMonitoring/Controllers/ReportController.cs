using Microsoft.AspNetCore.Mvc;
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

                // Server Edition
                //
                cmd = new SqlCommand(QueryCollection.ServerEdition, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.Edition = (string)dataReader.GetValue(0);
                }

                // HADR
                //
                cmd = new SqlCommand(QueryCollection.IsHadrEnabled, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.IsHADREnabled = (string)dataReader.GetValue(0);
                }

                // XTP
                //
                cmd = new SqlCommand(QueryCollection.IsXTPEnable, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.IsXTPEnabled = (string)dataReader.GetValue(0);
                }

                // Collation
                //
                cmd = new SqlCommand(QueryCollection.Collation, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.Collation = (string)dataReader.GetValue(0);
                }

                // Compat level
                //
                cmd = new SqlCommand(QueryCollection.CompatLevel, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.CompatLevel = (string)dataReader.GetValue(0);
                }


                dataReader.Close();

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
