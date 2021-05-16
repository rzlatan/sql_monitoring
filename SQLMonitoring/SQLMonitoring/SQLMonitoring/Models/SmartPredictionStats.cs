using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class SmartPredictionStats
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public string ServerName { get; set; }

        public double CpuUsage { get; set; }

        public double MemoryUsage { get; set; }

        public double NetworkUsage { get; set; }

        public double DiskLatency { get; set; }
    }

}
