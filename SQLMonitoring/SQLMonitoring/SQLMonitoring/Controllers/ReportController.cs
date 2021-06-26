using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SQLMonitoring.Collections;
using SQLMonitoring.DatabaseConnection;
using SQLMonitoring.Model;
using SQLMonitoring.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Controllers
{
    public class ReportController : Controller
    {
        public IConfiguration _configuration;

        public DatabaseContext _db;

        public ReportController(DatabaseContext db, IConfiguration config)
        {
            _db = db;
            _configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void UploadLogFullProblem(string Timestamp, string Server, string DbName, string FileName, string CurrentSizeMb, string Growth, string FreeSpaceMb)
        {
            CommonProblems stats = new CommonProblems();
            stats.Type = CommonProblemType.LogFull;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.DbName = DbName;
            stats.FileName = FileName;
            stats.CurrentSizeMb = double.Parse(CurrentSizeMb);
            stats.Growth = int.Parse(Growth);
            stats.FreeSpaceMb = double.Parse(FreeSpaceMb);

            _db.GlobalCommonProblems.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadMissingBackupProblem(string Timestamp, string Server, string DatabaseName, string HoursSinceLastBackup)
        {
            CommonProblems stats = new CommonProblems();
            stats.Type = CommonProblemType.MissingBackup;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.DbName = DatabaseName;
            stats.HoursSinceLastBackup = long.Parse(HoursSinceLastBackup);

            _db.GlobalCommonProblems.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadLongTransactionProblem(string Timestamp, string Server, string TransactionId, string Name, string BeginTime, string Duration, string State)
        {
            CommonProblems stats = new CommonProblems();
            stats.Type = CommonProblemType.LongTransaction;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.TransactionId = int.Parse(TransactionId);
            stats.TransactionName = Name;
            stats.BeginTime = DateTime.Parse(BeginTime);
            stats.Duration = long.Parse(Duration);
            stats.State = State;

            _db.GlobalCommonProblems.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadMissingIndexes(string Timestamp, string Server, string DatabaseId, string EqualityColumns, string InequalityColumns)
        {
            CommonProblems stats = new CommonProblems();
            stats.Type = CommonProblemType.MissingIndexes;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.DatabaseId = long.Parse(DatabaseId);
            stats.EqualityColumns = EqualityColumns;
            stats.InequalityColumns = InequalityColumns;

            _db.GlobalCommonProblems.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTop5ActiveTransactions(string Timestamp, string Server, string TransactionId, string Name, string BeginTime, string DurationMin, string State)
        {
            BlockingAndDeadlocks stats = new BlockingAndDeadlocks();

            stats.Type = BlockingAndDeadlockType.LongActiveTransactions;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.TransactionId = long.Parse(TransactionId);
            stats.Name = Name;
            stats.BeginTime = DateTime.Parse(BeginTime);
            stats.DurationMin = long.Parse(DurationMin);
            stats.State = State;

            _db.GlobalBlockingStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadBlockingSessions(string Timestamp, string Server, string ProcessId, string Blocked, string Status, string WaitTime, string WaitResource, string DatabaseId)
        {
            BlockingAndDeadlocks stats = new BlockingAndDeadlocks();

            stats.Type = BlockingAndDeadlockType.DeadlocksInfo;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ProcessId = int.Parse(ProcessId);
            stats.Blocked = int.Parse(Status);
            stats.Status = Status;
            stats.WaitTime = long.Parse(WaitTime);
            stats.WaitResource = WaitResource;
            stats.DatabaseId = int.Parse(DatabaseId);

            _db.GlobalBlockingStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTotalDeadlocks(string Timestamp, string Server, string TotalDeadlocks)
        {
            BlockingAndDeadlocks stats = new BlockingAndDeadlocks();

            stats.Type = BlockingAndDeadlockType.TotalDeadlocks;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.TotalDeadlocks = long.Parse(TotalDeadlocks);

            _db.GlobalBlockingStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadThroughput(string Timestamp, string Server, string ReadMBps, string WriteMBps, string TotalMBps)
        {
            IOStats stats = new IOStats();

            stats.Type = IOStatsType.Throughput;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ReadMBps = (long)double.Parse(ReadMBps);
            stats.WriteMBps = (long)double.Parse(WriteMBps);
            stats.TotalMBps = (long)double.Parse(TotalMBps);

            _db.GlobalIOStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadIOPS(string Timestamp, string Server, string ReadIOPS, string WriteIOPS, string TotalIOPS)
        {
            IOStats stats = new IOStats();

            stats.Type = IOStatsType.IOPS;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ReadIOPS = (long)double.Parse(ReadIOPS);
            stats.WriteIOPS = (long)double.Parse(WriteIOPS);
            stats.TotalIOPS = (long)double.Parse(TotalIOPS);

            _db.GlobalIOStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadLatency(string Timestamp, string Server, string ReadLatency, string WriteLatency)
        {
            IOStats stats = new IOStats();

            stats.Type = IOStatsType.Latency;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ReadLatency = (long)double.Parse(ReadLatency);
            stats.WriteLatency = (long)double.Parse(WriteLatency);

            _db.GlobalIOStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTop5IOPlans(string Timestamp, string Server, string PlanId, string LogicalIOs, string LogicalReads, string LogicalWrites)
        {
            IOStats stats = new IOStats();
            stats.ServerName = Server;

            stats.Type = IOStatsType.TopQueries;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.TotalIOs = long.Parse(LogicalIOs);
            stats.TotalReadIOs = (long)double.Parse(LogicalReads);
            stats.TotalWriteIOs = (long)double.Parse(LogicalWrites);

            _db.GlobalIOStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTotalAndTargetMemory(string Timestamp, string Server, string TotalMemory, string TargetMemory)
        {
            MemoryStats stats = new MemoryStats();

            stats.Type = MemoryStatsType.TotalAndTargetMemory;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.TotalMemory = long.Parse(TotalMemory);
            stats.TargetMemory = long.Parse(TargetMemory);

            _db.GlobalMemoryStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadBufferHitRatioAndPageLifeExpectancy(string Timestamp, string Server, string BufferCacheHitRatio, string PageLifeExpectancy)
        {
            MemoryStats stats = new MemoryStats();

            stats.Type = MemoryStatsType.BufferHitRatioAndPageLifeExpectancy;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.BufferCacheHitRatio = long.Parse(BufferCacheHitRatio);
            stats.PageLifeExpectancy = long.Parse(PageLifeExpectancy);

            _db.GlobalMemoryStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTop5MemoryClerks(string Timestamp, string Server, string MemoryClerk, string SizeMb)
        {
            MemoryStats stats = new MemoryStats();

            stats.Type = MemoryStatsType.MemoryClerkStats;
            stats.ServerName = Server;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.MemoryClerk = MemoryClerk;
            stats.MemoryClerkSize = long.Parse(SizeMb);

            _db.GlobalMemoryStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadBasicResourceUsage(string Timestamp, string Server, string Cpu, string Memory, string Network, string Connections, string BatchRequests)
        {
            BasicResourceUsage stats = new BasicResourceUsage();

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ServerName = Server;
            stats.CpuUsage = Double.Parse(Cpu);
            stats.MemoryUsage = Double.Parse(Memory);
            stats.NetworkUsage = Double.Parse(Network);
            stats.UserConnections = Double.Parse(Connections);
            stats.BatchRequests = Double.Parse(BatchRequests);

            _db.BasicResourceUsageStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void GlobalWaitStats(string Timestamp, string Server, string WaitType, string WaitTimeMs, string MaxWaitTimeMs, string SignalWaitTimeMs)
        {
            GlobalWaitStats stats = new GlobalWaitStats();

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ServerName = Server;
            stats.WaitType = WaitType;
            stats.WaitTimeMs = long.Parse(WaitTimeMs);
            stats.MaxWaitTimeMs = long.Parse(MaxWaitTimeMs);
            stats.SignalWaitTimeMs = long.Parse(SignalWaitTimeMs);

            _db.GlobalWaitStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTempdbDataAndLogSize(string Timestamp, string Server, string DataSize, string LogSize)
        {
            TempdbStats stats = new Model.TempdbStats();

            stats.Type = TempdbStatsType.TempdbSizeThroughTime;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ServerName = Server;
            stats.DataSizeMb = int.Parse(DataSize);
            stats.LogSizeMb = int.Parse(LogSize);

            _db.GlobalTempdbStats.Add(stats);
            _db.SaveChanges();
        }


        [HttpPost]
        public void GlobalSpinlockStats(string Timestamp, string Server, string Spinlock, string Collisions, string Backoffs)
        {
            GlobalSpinlockStats stats = new Model.GlobalSpinlockStats();

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.Server = Server;
            stats.Spinlock = Spinlock;
            stats.Collisions = long.Parse(Collisions);
            stats.Backoffs = long.Parse(Backoffs);

            _db.GlobalSpinlockStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTop5CPUWorkloadGroups(string Timestamp, string Server, string WorkloadGroup, string WorkloadGroupCpuMs)
        {
            CPUStats stats = new Model.CPUStats();

            stats.Type = CpuStatsType.Top5WorkloadGroupsByCPU;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ServerName = Server;
            stats.WorkloadGroup = WorkloadGroup;
            stats.WorkloadGroupCPUTime = long.Parse(WorkloadGroupCpuMs);

            _db.GlobalCPUStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTop5CPUQueries(string Timestamp, string Server, string QueryHash, string QueryTime)
        {
            CPUStats stats = new Model.CPUStats();

            stats.Type = CpuStatsType.Top5QueriesByCpu;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ServerName = Server;
            stats.QueryHash = QueryHash;
            stats.QueryExecTime = long.Parse(QueryTime);

            _db.GlobalCPUStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadCompileAndExecTime(string Timestamp, string Server, string QueryExecTime, string QueryCompileTime)
        {
            CPUStats stats = new Model.CPUStats();

            stats.Type = CpuStatsType.CompileAndExecCPU;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ServerName = Server;
            stats.CompileCPUTime = long.Parse(QueryCompileTime);
            stats.ExecCPUTime = long.Parse(QueryExecTime);

            _db.GlobalCPUStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTop5QueriesByCpuConsumption(string Timestamp, string Server, string QueryHash, string LastExecTime, string TotalWorkerTime, string AvgCpuTime, string ExecCount)
        {
            QueriesStats stats = new QueriesStats();

            stats.Type = QueriesStatsType.TopQueriesByCpu;

            stats.Date = DateTime.ParseExact(
               Timestamp,
               "dd/MM/yyyy HH:mm",
               CultureInfo.InvariantCulture);

            stats.ServerName = Server;

            stats.QueryHash = QueryHash;
            stats.LastExecTime = DateTime.Parse(LastExecTime);
            stats.TotalWorkerTime = (long) double.Parse(TotalWorkerTime);
            stats.AvgCpuTime = double.Parse(AvgCpuTime);
            stats.ExecCount = long.Parse(ExecCount);

            _db.GlobalQueryStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadTop5QueriesByIOConsumption(string Timestamp, string Server, string QueryHash, string LastExecTime, string TotalLogicalReads, string TotalLogicalWrites, string ExecCount, string IOsPerExecution)
        {
            QueriesStats stats = new QueriesStats();

            stats.Type = QueriesStatsType.TopQueriesByIO;

            stats.Date = DateTime.ParseExact(
               Timestamp,
               "dd/MM/yyyy HH:mm",
               CultureInfo.InvariantCulture);

            stats.ServerName = Server;

            stats.QueryHash = QueryHash;
            stats.LastExecTime = DateTime.Parse(LastExecTime);
            stats.TotalLogicalReads = long.Parse(TotalLogicalReads);
            stats.TotalLogicalWrites = long.Parse(TotalLogicalWrites);
            stats.ExecCount = long.Parse(ExecCount);
            stats.AvgIOsPerExecution = double.Parse(IOsPerExecution);

            _db.GlobalQueryStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpPost]
        public void UploadCpuTotalAndUserTime(string Timestamp, string Server, string TotalCpu, string UserCpu)
        {
            CPUStats stats = new Model.CPUStats();

            stats.Type = CpuStatsType.TotalAndUserCPU;

            stats.Date = DateTime.ParseExact(
                Timestamp,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture);

            stats.ServerName = Server;
            stats.CPUTotalTime = (long)double.Parse(TotalCpu);
            stats.CPUUserTime = (long)double.Parse(UserCpu);

            _db.GlobalCPUStats.Add(stats);
            _db.SaveChanges();
        }

        [HttpGet]
        public void GenerateTempdbFileLayout(string serverName)
        {
            byte[] userIdByteArray;
            HttpContext.Session.TryGetValue("Id", out userIdByteArray);
            int userId = BitConverter.ToInt32(userIdByteArray);

            var server = _db.Servers.Where(srv => srv.ServerName == serverName).FirstOrDefault();
            var connectionStringTemplate = _configuration.GetValue<string>("Templates:ServerConnectionString");

            DateTime date = DateTime.Now;

            SqlConnection connection = null;
            SqlCommand cmd = null;
            SqlDataReader dataReader = null;

            try
            {
                var connectionString = string.Format(
                    connectionStringTemplate,
                    server.ServerName,
                    "master",
                    server.UserName,
                    CryptographyService.DecryptString(server.Password));

                connection = new SqlConnection(connectionString);
                connection.Open();

                cmd = new SqlCommand(QueryCollection.TempdbFileLayout, connection);

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    TempdbStats tempdbInformation = new TempdbStats();
                    tempdbInformation.Type = TempdbStatsType.FileLayout;
                    tempdbInformation.ServerName = server.ServerName;
                    tempdbInformation.Date = date;

                    tempdbInformation.FileId = (int)dataReader.GetValue(0);
                    tempdbInformation.FileName = (string)dataReader.GetValue(1);
                    tempdbInformation.Location = (string)dataReader.GetValue(2);
                    tempdbInformation.FileType = (string)dataReader.GetValue(3);
                    tempdbInformation.FileSize = Convert.ToInt32(dataReader.GetValue(4));
                    tempdbInformation.FileMaxSize = Convert.ToInt32(dataReader.GetValue(5));
                    tempdbInformation.Growth = Convert.ToInt32(dataReader.GetValue(6));

                    _db.GlobalTempdbStats.Add(tempdbInformation);
                    _db.SaveChanges();
                }
                dataReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }

                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
        }

        [HttpGet]
        public void GenerateBackupInformation(string serverName)
        {
            byte[] userIdByteArray;
            HttpContext.Session.TryGetValue("Id", out userIdByteArray);
            int userId = BitConverter.ToInt32(userIdByteArray);

            var server = _db.Servers.Where(srv => srv.ServerName == serverName).FirstOrDefault();
            var connectionStringTemplate = _configuration.GetValue<string>("Templates:ServerConnectionString");

            DateTime date = DateTime.Now;

            SqlConnection connection = null;
            SqlCommand cmd = null;
            SqlDataReader dataReader = null;

            try
            {
                var connectionString = string.Format(
                    connectionStringTemplate,
                    server.ServerName,
                    "master",
                    server.UserName,
                    CryptographyService.DecryptString(server.Password));

                connection = new SqlConnection(connectionString);
                connection.Open();

                // Backup information for last 24h
                //
                cmd = new SqlCommand(QueryCollection.BackupsInLast24h, connection);

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    BackupInformation backupInformation = new BackupInformation();
                    backupInformation.Type = BackupInformationType.LastBackups;
                    backupInformation.ServerName = server.ServerName;
                    backupInformation.Date = date;

                    backupInformation.DatabaseName = (string)dataReader.GetValue(0);
                    backupInformation.BackupStartTime = (DateTime)dataReader.GetValue(1);
                    backupInformation.BackupEndTime = (DateTime)dataReader.GetValue(2);
                    backupInformation.BackupDuration = Convert.ToInt64(dataReader.GetValue(3));
                    backupInformation.BackupType = (string)dataReader.GetValue(4);
                    backupInformation.BackupSize = Convert.ToInt64(dataReader.GetValue(5));
                    backupInformation.BackupLocation = (string)dataReader.GetValue(6);

                    _db.GlobalBackupStats.Add(backupInformation);
                    _db.SaveChanges();
                }
                dataReader.Close();

                // Last Restorable points in time
                //
                cmd = new SqlCommand(QueryCollection.LastRecoverablePoint, connection);

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    BackupInformation backupInformation = new BackupInformation();
                    backupInformation.Type = BackupInformationType.LastRestorablePoint;
                    backupInformation.ServerName = server.ServerName;
                    backupInformation.Date = date;

                    backupInformation.DatabaseName = (string)dataReader.GetValue(0);
                    backupInformation.LastRestorablePoint = (DateTime)dataReader.GetValue(1);

                    _db.GlobalBackupStats.Add(backupInformation);
                    _db.SaveChanges();
                }
                dataReader.Close();

                // Databases Without Backup
                //
                cmd = new SqlCommand(QueryCollection.DatabasesWithoutBackup, connection);

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    BackupInformation backupInformation = new BackupInformation();
                    backupInformation.Type = BackupInformationType.DatabasesWithoutBackup;
                    backupInformation.ServerName = server.ServerName;
                    backupInformation.Date = date;

                    backupInformation.DatabaseName = (string)dataReader.GetValue(0);
                    backupInformation.HoursSinceLastBackup = Convert.ToInt64(dataReader.GetValue(2));

                    _db.GlobalBackupStats.Add(backupInformation);
                    _db.SaveChanges();
                }
                dataReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }

                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
        }

        [HttpGet]
        public void GenerateBasicInformation(string serverName)
        {
            byte[] userIdByteArray;
            HttpContext.Session.TryGetValue("Id", out userIdByteArray);
            int userId = BitConverter.ToInt32(userIdByteArray);

            var server = _db.Servers.Where(srv => srv.ServerName == serverName).FirstOrDefault();
            var connectionStringTemplate = _configuration.GetValue<string>("Templates:ServerConnectionString");
            
            DateTime date = DateTime.Now;
            BasicInformation basicInformation = new BasicInformation();
            basicInformation.ServerName = serverName;
            basicInformation.Date = date;

            SqlConnection connection = null;
            SqlCommand cmd = null;
            SqlDataReader dataReader = null;

            try
            {
                var connectionString = string.Format(
                    connectionStringTemplate,
                    server.ServerName,
                    "master",
                    server.UserName,
                    CryptographyService.DecryptString(server.Password));

                connection = new SqlConnection(connectionString);
                connection.Open();

                // Server Version
                //
                cmd = new SqlCommand(QueryCollection.ServerVersion, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.ServerVersion = (string)dataReader.GetValue(0);
                }
                dataReader.Close();

                // Server Edition
                //
                cmd = new SqlCommand(QueryCollection.ServerEdition, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.Edition = (string)dataReader.GetValue(0);
                }
                dataReader.Close();

                // HADR
                //
                cmd = new SqlCommand(QueryCollection.IsHadrEnabled, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.IsHADREnabled = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();

                // XTP
                //
                cmd = new SqlCommand(QueryCollection.IsXTPEnable, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.IsXTPEnabled = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();

                // Collation
                //
                cmd = new SqlCommand(QueryCollection.Collation, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.Collation = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();

                // Compat level
                //
                cmd = new SqlCommand(QueryCollection.CompatLevel, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.CompatLevel = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();

                // Basic VM info
                //
                cmd = new SqlCommand(QueryCollection.BaseVmInfo, connection);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    basicInformation.ProcessorCount = dataReader.GetValue(0).ToString();
                    basicInformation.Memory = dataReader.GetValue(1).ToString();
                    basicInformation.HyperThreadRatio = dataReader.GetValue(2).ToString();
                    basicInformation.WorkerCount = (int) dataReader.GetValue(3);
                }
                dataReader.Close();

                // Databases
                //
                List<Database> databaseList = new List<Database>();
                cmd = new SqlCommand(QueryCollection.DatabaseInfo, connection);
                dataReader = cmd.ExecuteReader();
                while(dataReader.Read())
                {
                    Database database = new Database();

                    database.DatabaseId = (int) dataReader.GetValue(0);
                    database.Name = (string) dataReader.GetValue(1);
                    database.State = (string) dataReader.GetValue(2);
                    database.SnapshotIsolationLevel = (string)dataReader.GetValue(3);
                    database.RecoveryModel = (string)dataReader.GetValue(4);
                    database.IsEncrypted = dataReader.GetValue(5).ToString();

                    var dbConnectionString = string.Format(
                        connectionStringTemplate,
                        server.ServerName,
                        "'" + database.Name + "'",
                        server.UserName,
                        CryptographyService.DecryptString(server.Password));

                    SqlConnection dbConnection = new SqlConnection(connectionString);
                    var dbSizeQuery = string.Format(
                            QueryCollection.DatabaseSizes,
                            database.Name);

                    dbConnection.Open();
                    SqlCommand additionalInfoCmd = new SqlCommand(dbSizeQuery, dbConnection);
                    SqlDataReader dataReaderAdditional = null;

                    dataReaderAdditional = additionalInfoCmd.ExecuteReader();

                    while (dataReaderAdditional.Read())
                    {
                        database.DatabaseSize = dataReaderAdditional.GetValue(0).ToString();

                    }

                    dataReaderAdditional.Close();
                    dbConnection.Close();

                    databaseList.Add(database);
                }
                dataReader.Close();
                basicInformation.Databases = databaseList;

                // Database files
                //
                List<DatabaseFile> databaseFiles = new List<DatabaseFile>();
                cmd = new SqlCommand(QueryCollection.DatabaseFiles, connection);
                dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    DatabaseFile file = new DatabaseFile();
                    file.Name = dataReader.GetValue(0).ToString();
                    file.Location = dataReader.GetValue(1).ToString();
                    file.Db = databaseList.Where(db => db.Id == (int)dataReader.GetValue(2)).FirstOrDefault();
                    file.State = dataReader.GetValue(3).ToString();
                    file.Type = dataReader.GetValue(4).ToString();
                    file.Size = Double.Parse(dataReader.GetValue(5).ToString());
                    file.MaxSize = dataReader.GetValue(6).ToString();
                    file.AutoGrowth = dataReader.GetValue(7).ToString();

                    databaseFiles.Add(file);
                }
                dataReader.Close();
                basicInformation.DatabaseFiles = databaseFiles;

                _db.BasicInformationStats.Add(basicInformation);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }

                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
        }
    }
}
