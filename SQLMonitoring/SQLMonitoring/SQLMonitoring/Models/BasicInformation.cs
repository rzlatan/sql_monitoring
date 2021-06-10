using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class BasicInformation
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public string ServerVersion { get; set; }

        public string Edition { get; set; }

        public string ProcessorCount { get; set; }

        public string Memory { get; set; }

        public int WorkerCount { get; set; }

        public string CompatLevel { get; set; }

        public string Collation { get; set; }

        public string IsXTPEnabled { get; set; }

        public string IsHADREnabled { get; set; }

        public string HyperThreadRatio { get; set; }

        public List<Database> Databases { get; set; }

        public List<DatabaseFile> DatabaseFiles { get; set; }
    }
}
