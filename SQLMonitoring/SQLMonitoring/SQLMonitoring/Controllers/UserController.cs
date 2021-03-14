using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.DatabaseConnection;
using SQLMonitoring.Model;
using SQLMonitoring.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Controllers
{
    public class UserController : Controller
    {

        private DatabaseContext _db;

        private IConfiguration _configuration;

        private EmailService _emailService;

        public UserController(DatabaseContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            _emailService = new EmailService(configuration);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            var user = _db.Users.Where(user => user.Id == id).FirstOrDefault();
            return View("../User/Profile", user);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _db.Users.Where(user => user.Id == id).FirstOrDefault();
            _db.Users.Remove(user);
            _db.SaveChanges();

            var users = _db.Users;
            return View("../Admin/AdminHomepage", users);
        }
    }
}
