using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class Report
    {
        public long Id { get; set; }

        public DateTime CreationTime { get; set; }

        public User User { get; set; }

        public Server Server { get; set; }

        public string ResultPath { get; set; }

        public Guid? ReportGuid { get; set; }
    }
}
