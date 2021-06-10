using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class DatabaseFile
    {
        public int Id { get; set; }

        public Database Db { get; set; }

        public string Name { get; set; }

        public double Size { get; set; }

        public string MaxSize { get; set; }

        public string Type { get; set; }

        public string State { get; set; }

        public string AutoGrowth { get; set; }

        public string Location { get; set; }
    }
}
