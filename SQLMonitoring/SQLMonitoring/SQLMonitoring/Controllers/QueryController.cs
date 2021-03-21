using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLMonitoring.Collections;
using SQLMonitoring.Services;
using SQLMonitoring.Models;
using SQLMonitoring.Model;

namespace SQLMonitoring.Controllers
{
    public class QueryController : Controller
    {

        public IConfiguration _configuration;

        public DatabaseContext _db;

        public QueryController(DatabaseContext db, IConfiguration config)
        {
            _db = db;
            _configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Execute(string server, string database, string queryText)
        {
            SqlConnection connection = null;
            SqlCommand cmd = null;
            SqlDataReader dataReader = null;
            var connectionStringTemplate = _configuration.GetValue<string>("Templates:ServerConnectionString");
            Server srv = _db.Servers.Where(srv => srv.ServerName == server).FirstOrDefault();

            try
            {
                var connectionString = string.Format(
                    connectionStringTemplate,
                    server,
                    database,
                    srv.UserName,
                    CryptographyService.DecryptString(srv.Password));

                connection = new SqlConnection(connectionString);
                connection.Open();

                cmd = new SqlCommand(queryText, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    
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

            return null;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            byte[] userIdByteArray;
            HttpContext.Session.TryGetValue("Id", out userIdByteArray);
            int userId = BitConverter.ToInt32(userIdByteArray);

            var servers = _db.Servers.Where(server => server.Owner.Id == userId);
            var connectionStringTemplate = _configuration.GetValue<string>("Templates:ServerConnectionString");

            foreach (var server in servers)
            {
                List<string> databases = new List<string>()
                {
                    "master",
                    "model",
                    "msdb",
                    "tempdb"
                };

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

                    cmd = new SqlCommand(QueryCollection.DatabaseList, connection);
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        databases.Add((string)dataReader.GetValue(0));
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

                server.DatabaseList = databases;
            }

            return View("../Query/Edit", servers);
        }
    }
}
