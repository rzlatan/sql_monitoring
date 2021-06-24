using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public enum BlockingAndDeadlockType
    {
        LongActiveTransactions,
        DeadlocksInfo,
        TotalDeadlocks
    }

    public class BlockingAndDeadlocks
    {
        public long Id { get; set; }
        
        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public BlockingAndDeadlockType Type { get; set; }

        public long? TransactionId { get; set; }

        public string? Name { get; set; }

        public DateTime? BeginTime { get; set; }

        public long? DurationMin { get; set; }

        public string? State { get; set; }

        public int? ProcessId { get; set; }

        public int? Blocked { get; set; }

        public string? Status { get; set; }

        public long? WaitTime { get; set; }

        public string? WaitResource { get; set; }

        public int? DatabaseId { get; set; }

        public long TotalDeadlocks { get; set; }

    }
}
