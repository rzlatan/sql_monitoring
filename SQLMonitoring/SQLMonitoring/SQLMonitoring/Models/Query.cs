using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Models
{
    public class Query
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string QueryText { get; set; }
    }
}
