using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class BasicResourceUsage
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public double CpuUsage { get; set; }

        public double MemoryUsage { get; set; }

        public double NetworkUsage { get; set; }

        public double BatchRequests { get; set; }

        public double UserConnections { get; set; }
    }
}
