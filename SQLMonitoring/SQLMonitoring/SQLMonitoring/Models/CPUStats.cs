using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public enum CpuStatsType
    {
        TotalAndUserCPU,
        Top5QueriesByCpu,
        Top5WorkloadGroupsByCPU,
        CompileAndExecCPU
    }

    public class CPUStats
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public CpuStatsType Type { get; set; }

        public long? CPUTotalTime { get; set; }

        public long? CPUUserTime { get; set; }

        public long? CompileCPUTime { get; set; }

        public long? ExecCPUTime { get; set; }

        public string? WorkloadGroup { get; set; }

        public long? WorkloadGroupCPUTime { get; set; }

        public string? QueryHash { get; set; }

        public long? QueryExecTime { get; set; }
    }
}
