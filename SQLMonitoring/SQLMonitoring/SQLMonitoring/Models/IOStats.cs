using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public enum IOStatsType
    {
        Throughput,
        Latency,
        IOPS,
        TopQueries
    }

    public class IOStats
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public long TotalMBps { get; set; }

        public long WriteMBps { get; set; }

        public long ReadMBps { get; set; }

        public long TotalIOPS { get; set; }

        public long ReadIOPS { get; set; }

        public long WriteIOPS { get; set; }

        public long ReadLatency { get; set; }

        public long WriteLatency { get; set; }

        public string PlanId { get; set; }

        public long TotalIOs { get; set; }

        public long TotalReadIOs { get; set; }

        public long TotalWriteIOs { get; set; }

        public IOStatsType Type { get; set; }
    }
}
