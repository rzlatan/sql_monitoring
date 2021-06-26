using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public enum CommonProblemType
    {
        MissingBackup,
        LogFull,
        LongTransaction,
        MissingIndexes
    }

    public class CommonProblems
    {
        public long Id { get; set; }

        public string ServerName { get; set; }

        public DateTime Date { get; set; }

        public CommonProblemType Type { get; set; }

        public string? DbName { get; set; }

        public string? FileName { get; set; }

        public double? CurrentSizeMb { get; set; }

        public int? Growth { get; set; }

        public double? FreeSpaceMb { get; set; }

        public int? TransactionId { get; set; }

        public string? TransactionName { get; set; }

        public DateTime? BeginTime { get; set; }

        public long? Duration { get; set; }

        public string? State { get; set; }

        public long? HoursSinceLastBackup { get; set; }

        public long? DatabaseId { get; set; }
        
        public string? EqualityColumns { get; set; }
    
        public string? InequalityColumns { get; set; }
    }
}
