using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class Server
    {
        public long Id { get; set; }

        public string ServerName { get; set; }

        public string Password { get; set; }

        public List<Report> ReportList { get; set; }
    }
}
