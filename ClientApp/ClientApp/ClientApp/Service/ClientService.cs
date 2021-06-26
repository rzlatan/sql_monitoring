using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Service
{
    class ClientService
    {
        public static bool BasicInformationEnabled { get; set; }

        public static bool BasicResourceUsageStatsEnabled { get; set; }

        public static bool BlockingAndDeadlocksEnabled { get; set; }

        public static bool CommonProblemsEnabled { get; set; }

        public static bool CpuDetailsEnabled { get; set; }

        public static bool IODetailsEnabled { get; set; }

        public static bool MemoryDetailsEnabled { get; set; }

        public static bool QueryStatsEnabled { get; set; }

        public static bool TempdbStatsEnabled { get; set; }

        public static bool BackupStatsEnabled { get; set; }

        public static bool WaitStatsEnabled { get; set; }

        public static string ServerName { get; set; }

        public static string Password { get; set; }
    
    
        public static void Confgure()
        {

        }
    }
}
