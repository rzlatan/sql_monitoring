using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SQLMonitoring.Model;
using SQLMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SQLMonitoring.DatabaseConnection;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.Utilities;

namespace SQLMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private DatabaseContext _db;

        private IConfiguration _configuration;

        private EmailService _emailService;

        public HomeController(ILogger<HomeController> logger, DatabaseContext db, IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _configuration = configuration;
            _emailService = new EmailService(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            ViewBag.ErrorMessage = "";
            ViewBag.IsAdmin = false;

            var adminUsername = _configuration.GetValue<string>("AdminUser:Username");
            var adminPassword = _configuration.GetValue<string>("AdminUser:Password");

            if (adminUsername == email && adminPassword == password)
            {
                var users = _db.Users.ToList<User>();
                ViewBag.IsAdmin = true;
                return View("../Admin/AdminHomepage", users);
            }

            var user = _db.Users.Where(user => user.Email == email && user.Password == password).FirstOrDefault();

            if (user != null)
            {
                return View("Homepage", user);
            }
            else
            {
                ViewBag.ErrorMessage = "Wrong username or password";
                return View("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Signup()
        {
            return View("Signup");
        }

        [HttpPost]
        public async Task<IActionResult> Signup(string email, string password, string confirmPassword)
        {
            var user = _db.Users.Where(user => user.Email == email).FirstOrDefault();

            if (user != null)
            {
                ViewBag.ErrorMessage = "Account with specified email already exists";
                return View("Signup");
            }

            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Password and confirm password must match.";
                return View("Signup");
            }

            if (password.Length < 5)
            {
                ViewBag.ErrorMessage = "Password must be longer than 5 characters";
                return View("Signup");
            }

            _db.Users.Add(new Model.User() { AccountType = UserType.CLIENT, Email = email, Password = password, EmailConfirmed = false });
            _db.SaveChanges();

            _emailService.SendEmail(email);

            return View("EmailConfirmation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
