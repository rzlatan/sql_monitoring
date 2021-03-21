using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Controllers
{
    public class QueryController : Controller
    {

        public IConfiguration _configuraton;

        public DatabaseContext _db;

        public QueryController(DatabaseContext db, IConfiguration config)
        {
            _db = db;
            _configuraton = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View("../Query/Edit");
        }
    }
}
