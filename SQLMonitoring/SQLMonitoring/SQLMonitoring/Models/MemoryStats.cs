using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public enum MemoryStatsType
    {
        TotalAndTargetMemory,
        BufferHitRatioAndPageLifeExpectancy,
        MemoryClerkStats
    }

    public class MemoryStats
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public long TotalMemory { get; set; }

        public long TargetMemory { get; set; }

        public long BufferCacheHitRatio { get; set; }

        public long PageLifeExpectancy { get; set; }

        public string MemoryClerk { get; set; }

        public long MemoryClerkSize { get; set; }

        public MemoryStatsType Type { get; set; }
    }
}
