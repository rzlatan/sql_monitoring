using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class GlobalWaitStats
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public string WaitType { get; set; }

        public long WaitTimeMs { get; set; }

        public long MaxWaitTimeMs { get; set; }

        public long SignalWaitTimeMs { get; set; }
    }
}
