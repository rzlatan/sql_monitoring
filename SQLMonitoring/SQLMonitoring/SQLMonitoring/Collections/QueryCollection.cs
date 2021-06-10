using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Collections
{
    public class QueryCollection
    {
        public static string ServerVersion =
            $@"
                SELECT @@VERSION
            ";

        public static string ServerEdition =
            $@"
                SELECT SERVERPROPERTY ('Edition')
              ";

        public static string IsHadrEnabled =
            $@"
                SELECT SERVERPROPERTY('IsHadrEnabled')
             ";

        public static string IsXTPEnable =
            $@"
                SELECT SERVERPROPERTY('IsXTPSupported')
            ";

        public static string DatabaseList =
            $@"
                SELECT name FROM sys.databases
                WHERE database_id > 4
            ";

        public static string BaseVmInfo =
            $@"
                SELECT cpu_count,
                       physical_memory_kb,
                       hyperthread_ratio,
                       max_workers_count
                FROM sys.dm_os_sys_info
             ";

        public static string CompatLevel =
            $@"
                SELECT TOP 1 compatibility_level
                FROM sys.databases
            ";

        public static string Collation =
            $@"
                SELECT CONVERT (varchar(256), SERVERPROPERTY('collation')); 
            ";

        public static string DatabaseInfo =
            $@"
                SELECT database_id, name, state_desc, snapshot_isolation_state_desc, recovery_model_desc, is_encrypted FROM sys.databases
            ";

        public static string DatabaseSizes =
            $@"
                EXEC sp_spaceused
            ";

        public static string DatabaseFiles =
            $@"
                SELECT name, physical_name, database_id, state_desc, type_desc, size, max_size, growth FROM sys.master_files
             ";
    }
}
