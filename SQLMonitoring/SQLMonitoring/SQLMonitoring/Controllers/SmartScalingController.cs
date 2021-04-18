using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.DatabaseConnection;
using System;
using System.Collections.Generic;
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
    }
}
