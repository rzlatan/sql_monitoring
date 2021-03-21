using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Collections
{
    public class QueryCollection
    {
        public static string DatabaseList =
            $@"
                SELECT name FROM sys.databases
                WHERE database_id > 4
            ";
    }
}
