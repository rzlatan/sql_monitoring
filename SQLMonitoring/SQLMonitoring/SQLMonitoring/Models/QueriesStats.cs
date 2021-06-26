using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public enum QueriesStatsType
    {
        TopQueriesByCpu,
        TopQueriesByIO
    }

    public class QueriesStats
    {
        public long Id { get; set; }

        public string ServerName { get; set; }

        public DateTime Date { get; set; }

        public QueriesStatsType Type { get; set; }

        public string? QueryHash { get; set; }

        public long? ExecCount { get; set; }

        public DateTime? LastExecTime { get; set; }

        public long? TotalWorkerTime { get; set; }

        public double? AvgCpuTime { get; set; }
    
        public long? TotalLogicalReads { get; set; }

        public long? TotalLogicalWrites { get; set; }

        public double? AvgIOsPerExecution { get; set; }
    }
}
