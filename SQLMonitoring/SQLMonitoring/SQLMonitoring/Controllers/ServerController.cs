using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.DatabaseConnection;
using SQLMonitoring.Model;
using SQLMonitoring.Services;
using SQLMonitoring.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace SQLMonitoring.Controllers
{
    public class ServerController : Controller
    {
        public IConfiguration _configuraton;

        public DatabaseContext _db;

        public EmailService _emailService;

        public ServerController(DatabaseContext db, IConfiguration config)
        {
            _db = db;
            _configuraton = config;
            _emailService = new EmailService(config);
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

            return View("../Server/Home", servers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("../Server/Add");
        }

        [HttpPost]
        public IActionResult Add(string serverName, string username, string password, string confirmPassword)
        {
            byte[] userIdByteArray;
            HttpContext.Session.TryGetValue("Id", out userIdByteArray);
            int userId = BitConverter.ToInt32(userIdByteArray);

            var oldServer =_db.Servers.Where(server => server.Owner.Id == userId && server.ServerName == serverName).FirstOrDefault();

            if (oldServer != null)
            {
                ViewBag.ErrorMessage = "Existing server for given user already exists";
                return View("../Server/Add");
            }

            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Password and password confirmation must match";
                return View("../Server/Add");
            }

            var owner = _db.Users.Where(user => user.Id == userId).FirstOrDefault();
            var server = new Server();

            server.Owner = owner;
            server.ServerName = serverName;
            server.UserName = username;
            server.Password = CryptographyService.EncryptString(password);

            _db.Servers.Add(server);
            _db.SaveChanges();

            var servers = _db.Servers.Where(server => server.Owner == owner);
            return RedirectToAction("Home");
        }
    }
}
