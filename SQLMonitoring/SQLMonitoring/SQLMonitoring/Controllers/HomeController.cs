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
using SQLMonitoring.Services;
using System.Web;

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
            ViewBag.Message = "";
            ViewBag.IsAdmin = false;

            var adminUsername = _configuration.GetValue<string>("AdminUser:Username");
            var adminPassword = _configuration.GetValue<string>("AdminUser:Password");

            if (adminUsername == email && adminPassword == password)
            {
                var users = _db.Users.ToList<User>();
                ViewBag.IsAdmin = true;
                return View("../Admin/AdminHomepage", users);
            }

            var user = _db.Users.Where(user =>
                user.Email == email &&
                user.Password == CryptographyService.EncryptString(password))
                .FirstOrDefault();

            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    ViewBag.ErrorMessage = "Accout has not been validated !";
                    return View("Index");
                }

                HttpContext.Session.Set("Id", BitConverter.GetBytes(user.Id));
                return RedirectToAction( "Home", "Server");
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

            _db.Users.Add(new Model.User() { 
                AccountType = UserType.CLIENT,
                Email = email,
                Password = CryptographyService.EncryptString(password),
                EmailConfirmed = false });

            _db.SaveChanges();

            _emailService.SendEmail(email);

            ViewBag.Message = "Confirmation email has been sent. Please check your email to complete registration";
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(string email)
        {
            var rawEmail = CryptographyService.DecryptString(email);

            var user = _db.Users.Where(user => user.Email == rawEmail).FirstOrDefault();
            user.EmailConfirmed = true;
            _db.SaveChanges();

            return RedirectToAction("User", "Profile", new { user = user });
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            ViewBag.Message = "";
            ViewBag.ErrorMessage = "";
            HttpContext.Session.Set("Id", BitConverter.GetBytes(-1));
            return View("Index");
        }

    }
}
