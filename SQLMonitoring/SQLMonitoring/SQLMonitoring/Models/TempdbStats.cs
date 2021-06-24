using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public enum TempdbStatsType
    {
        FileLayout,
        TempdbSizeThroughTime
    }

    public class TempdbStats
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public int? FileId { get; set; }

        public string? FileName { get; set; }

        public string? Location { get; set; }

        public string? FileType { get; set; }

        public int? FileSize { get; set; }

        public int? FileMaxSize { get; set; }

        public int? Growth { get; set; }

        public int? DataSizeMb { get; set; }
        
        public int? LogSizeMb { get; set; }

        public TempdbStatsType Type { get; set; }
    }
}
