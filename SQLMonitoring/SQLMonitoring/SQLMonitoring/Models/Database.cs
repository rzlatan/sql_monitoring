using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class Database
    {
        public int Id { get; set; }

        public int DatabaseId { get; set; }
        public string Name { get; set; }

        public string State { get; set; }

        public string RecoveryModel { get; set; }

        public string SnapshotIsolationLevel { get; set; }

        public string IsEncrypted { get; set; }

        public string DatabaseSize { get; set; }
    }
}
