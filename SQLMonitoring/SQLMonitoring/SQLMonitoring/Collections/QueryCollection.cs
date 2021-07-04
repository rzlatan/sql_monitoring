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
            @"
                SELECT SUM(SizeMB)
                FROM (
                    SELECT DB_NAME(database_id) AS DatabaseName,
                    Name AS Logical_Name,
                    Physical_Name,
                    (size * 8) / 1024 SizeMB
                    FROM sys.master_files
                    WHERE DB_NAME(database_id) = '{0}'
                ) AS TEMP
            ";

        public static string DatabaseFiles =
            $@"
                SELECT name, physical_name, database_id, state_desc, type_desc, size, max_size, growth FROM sys.master_files
             ";

        public static string BackupsInLast24h =
            $@"SELECT 
                msdb.dbo.backupset.database_name, 
                msdb.dbo.backupset.backup_start_date, 
                msdb.dbo.backupset.backup_finish_date, 
                DATEDIFF(MINUTE, msdb.dbo.backupset.backup_finish_date, msdb.dbo.backupset.backup_start_date) AS backup_duration, 
                CASE msdb..backupset.type 
                    WHEN 'D' THEN 'Database' 
                    WHEN 'L' THEN 'Log' 
                END AS backup_type, 
                msdb.dbo.backupset.backup_size, 
                msdb.dbo.backupmediafamily.physical_device_name
                FROM msdb.dbo.backupmediafamily 
                INNER JOIN msdb.dbo.backupset ON msdb.dbo.backupmediafamily.media_set_id = msdb.dbo.backupset.media_set_id 
                WHERE (CONVERT(datetime, msdb.dbo.backupset.backup_start_date, 102) >= GETDATE() - 1)
                ORDER BY 
                msdb.dbo.backupset.database_name, 
                msdb.dbo.backupset.backup_finish_date 
            ";

        public static string LastRecoverablePoint =
            $@"SELECT  
               msdb.dbo.backupset.database_name,  
               MAX(msdb.dbo.backupset.backup_finish_date) AS last_db_backup_date 
              FROM   msdb.dbo.backupmediafamily  
              INNER JOIN msdb.dbo.backupset ON msdb.dbo.backupmediafamily.media_set_id = msdb.dbo.backupset.media_set_id  
              WHERE  msdb..backupset.type = 'D' 
              GROUP BY 
              msdb.dbo.backupset.database_name  
              ORDER BY  
              msdb.dbo.backupset.database_name
            ";

        public static string DatabasesWithoutBackup =
            $@"SELECT 
                msdb.dbo.backupset.database_name, 
                MAX(msdb.dbo.backupset.backup_finish_date) AS last_db_backup_date, 
                DATEDIFF(hh, MAX(msdb.dbo.backupset.backup_finish_date), GETDATE()) AS [Backup Age (Hours)] 
               FROM    msdb.dbo.backupset 
               WHERE     msdb.dbo.backupset.type = 'D'  
               GROUP BY msdb.dbo.backupset.database_name 
               HAVING  (MAX(msdb.dbo.backupset.backup_finish_date) < DATEADD(hh, - 24, GETDATE()))  
               UNION  
               --Databases without any backup history 
                SELECT      
                    master.dbo.sysdatabases.NAME AS database_name,  
                    NULL AS [Last Data Backup Date],  
                    9999 AS [Backup Age (Hours)]  
                FROM 
                    master.dbo.sysdatabases LEFT JOIN msdb.dbo.backupset 
                    ON master.dbo.sysdatabases.name  = msdb.dbo.backupset.database_name 
                WHERE msdb.dbo.backupset.database_name IS NULL AND master.dbo.sysdatabases.name <> 'tempdb' 
                ORDER BY  
                msdb.dbo.backupset.database_name";
        
        public static string TempdbFileLayout =
            $@"SELECT 
                file_id AS FileId,
                name as Name,
                physical_name as Location,
                type_desc as Type, size as Size,
                max_size as MaxSize,
                growth as Growth 
              FROM sys.master_files 
              WHERE database_id = 2";

        public static string SpinlockStatForPeriod =
             @"SELECT MAX(Collisions) AS MaxCollisions, MIN(Collisions) AS MinCollisions, MAX(Collisions) - MIN(Collisions) as Collisions, Spinlock, DATEPART(HOUR, [Date]) AS HOUR,  DATEPART(DAY,[Date]) AS DAY, DATEPART(YEAR,[Date]) AS Year, DATEPART(MONTH, [Date]) AS Month
                FROM dbo.GlobalSpinlockStats
                WHERE [Server] = '{0}' AND 
                [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
                GROUP BY Spinlock, DATEPART(DAY,[Date]), DATEPART(HOUR, [Date]), DATEPART(YEAR,[Date]), DATEPART(MONTH, [Date])";

        public static string WaitStatsForPeriod =
            @"SELECT MAX(WaitTimeMs) AS MaxWaitTimeMs, MIN(WaitTimeMs) AS MinWaitTimeMs, MAX(WaitTimeMs) - MIN(WaitTimeMs) as WaitTimeMs, WaitType, DATEPART(HOUR, [Date]) AS HOUR,  DATEPART(DAY,[Date]) AS DAY, DATEPART(YEAR,[Date]) AS Year, DATEPART(MONTH, [Date]) AS Month
               FROM dbo.GlobalWaitStats
               WHERE [ServerName] = '{0}' AND 
               [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
               GROUP BY WaitType, DATEPART(DAY,[Date]), DATEPART(HOUR, [Date]), DATEPART(YEAR,[Date]), DATEPART(MONTH, [Date])";

        public static string CPUQueryStatsForPeriod =
            @"SELECT TOP 3 QueryHash, MAX(LastExecTime) as LastExecTime, MAX(ExecCount) - MIN(ExecCount) AS ExecCount, Max(TotalWorkerTime) - MIN(TotalWorkerTime) AS TotalWorkerTime, AVG(AvgCpuTime) AS AvgCpuTime
              FROM dbo.GlobalQueryStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
              GROUP BY QueryHash
              ORDER BY TotalWorkerTime DESC";

        public static string IOQueryStatsForPeriod =
            @"SELECT TOP 3 QueryHash, MAX(LastExecTime) as LastExecTime, MAX(ExecCount) - MIN(ExecCount) AS ExecCount, Max(TotalLogicalWrites) - MIN(TotalLogicalWrites) AS TotalLogicalWrites, Max(TotalLogicalReads) - MIN(TotalLogicalReads) AS TotalLogicalReads, AVG(AvgIOsPerExecution) AS AvgIOsPerExecution, MAX(TotalLogicalWrites) + MAX(TotalLogicalReads) - MIN(TotalLogicalWrites) - MIN(TotalLogicalReads) AS TotalIOs
              FROM dbo.GlobalQueryStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
              GROUP BY QueryHash
              ORDER BY TotalIOs DESC";

        public static string TempdbSizeThroughTime =
            @"SELECT ServerName, Date, DataSizeMb, LogSizeMb
              FROM dbo.GlobalTempdbStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
			  DATEPART(MINUTE, [Date]) % 10 = 0";

        public static string LongTransactions =
            @"SELECT TOP 5 [Date] as LastDateTime, TransactionId as TxnId, Name, BeginTime, DurationMin, State
              FROM dbo.GlobalBlockingStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 0
              ORDER BY DurationMin DESC";

        public static string BlockedProcesses =
            @"SELECT TOP 5 [Date] as LastDateTime, ProcessId, Status, WaitTime, WaitResource, DatabaseId
              FROM dbo.GlobalBlockingStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 1
              ORDER BY WaitTime DESC
            ";

        public static string DeadlocksThroughTime =
            @"SELECT ServerName, Date, TotalDeadlocks
              FROM dbo.GlobalBlockingStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
			  DATEPART(MINUTE, [Date]) % 15 = 0 AND
			  Type = 3";

        public static string TargetMemoryStats =
            @"SELECT Date, TotalMemory, TargetMemory
              FROM dbo.GlobalMemoryStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 0
              AND DATEPART(mi, Date) % 10 = 0
              ORDER BY Date ASC"; 

        public static string BufferHitRatioAndPLE =
            @"SELECT Date, BufferCacheHitRatio, PageLifeExpectancy
              FROM dbo.GlobalMemoryStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 1
              AND DATEPART(mi, Date) % 10 = 0
              ORDER BY Date ASC";

        public static string TopMemoryClerks =
            @"SELECT DATEADD(minute, 10 * (DATEDIFF(minute, 0, Date) / 10), 0) AS Date, MemoryClerk, MemoryClerkSize
              FROM dbo.GlobalMemoryStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 2
              ORDER BY Date ASC";

        public static string BasicResourceUsageQuery =
             @"SELECT Date, CpuUsage, MemoryUsage, NetworkUsage, BatchRequests, UserConnections
               FROM dbo.BasicResourceUsageStats              
               WHERE [ServerName] = '{0}' AND 
               [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
               AND DATEPART(mi, Date) % 10 = 0";

        public static string TotalAndUserCpu =
            @"SELECT Date, CPUTotalTime, CPUUserTime
              FROM dbo.GlobalCPUStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 0
              AND DATEPART(mi, Date) % 10 = 0
              ORDER BY Date ASC";

        public static string CompileAndExecCpuTime =
            @"SELECT Date, CompileCPUTime, ExecCPUTime,Type
              FROM dbo.GlobalCPUStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 1
              AND DATEPART(mi, Date) % 10 = 0
              ORDER BY Date ASC";

        public static string TopQueriesByCpu =
            @"SELECT TOP 5 QueryHash, MAX(QueryExecTime) - MIN(QueryExecTime) as ExecTime
              FROM dbo.GlobalCPUStats
              WHERE QueryHash IS NOT NULL AND 
              [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
              GROUP BY QueryHash
              ORDER BY ExecTime DESC";

        public static string TopWorkloadGroups =
            @"SELECT TOP 5 WorkloadGroup, MAX(WorkloadGroupCPUTime) - MIN(WorkloadGroupCPUTime) as ExecTime
              FROM dbo.GlobalCPUStats
              WHERE WorkloadGroup IS NOT NULL AND
              Type = 3 AND [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
              GROUP BY WorkloadGroup
              ORDER BY ExecTime DESC";

        public static string ThroughputThroughTime =
            @"SELECT Date, TotalMBps, ReadMBps, WriteMBps
              FROM dbo.GlobalIOStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 0
              AND DATEPART(mi, Date) % 10 = 0
            ";

        public static string IOPSThroughTime =
            @"SELECT Date, TotalIOPS, ReadIOPS, WriteIOPS
              FROM dbo.GlobalIOStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 1
              AND DATEPART(mi, Date) % 10 = 0
            ";

        public static string LatencyThroughTime =
            @"SELECT Date, ReadLatency, WriteLatency
              FROM dbo.GlobalIOStats
              WHERE [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Type = 2
              AND DATEPART(mi, Date) % 10 = 0
            ";

        public static string TopQueriesByIOs =
            @"SELECT TOP 5 Date, PlanId, TotalIOs, TotalReadIOs, TotalWriteIOs, Id
             FROM dbo.GlobalIOStats
             WHERE Type = 3 AND [ServerName] = '{0}' AND 
             [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
             ORDER BY TotalIOs DESC";

        public static string LogFullProblem =
            @"SELECT DISTINCT DbName, MIN(FreeSpaceMb) AS FreeSpaceMb FROM dbo.GlobalCommonProblems
              WHERE Type = 0 AND
              [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              FreeSpaceMb < 1024
              GROUP BY DbName, FreeSpaceMb";

        public static string LongTransactionProblem =
            @"SELECT DISTINCT TransactionId, TransactionName, MAX(Duration) AS Duration, State FROM dbo.GlobalCommonProblems
              WHERE Type = 1 AND
              [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              Duration > 350
              GROUP BY TransactionId, TransactionName, state, Duration";

        public static string MissingBackups =
            @"SELECT DISTINCT DbName, MAX(HoursSinceLastBackup) as HoursSinceLastBackup FROM dbo.GlobalCommonProblems
              WHERE Type = 2 AND
              [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}') AND
              HoursSinceLastBackup > 24
              GROUP BY DbName, HoursSinceLastBackup
            ";

        public static string MissingIndexes =
            @"SELECT DISTINCT DatabaseId, EqualityColumns, InequalityColumns FROM dbo.GlobalCommonProblems
              WHERE Type = 3 AND
              [ServerName] = '{0}' AND 
              [Date] > CONVERT(DATETIME,'{1}') AND [Date] < CONVERT(DATETIME, '{2}')
            ";
    }
}
