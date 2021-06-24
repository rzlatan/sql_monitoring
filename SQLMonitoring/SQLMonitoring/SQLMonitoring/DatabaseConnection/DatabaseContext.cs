using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLMonitoring.Model;
using Microsoft.Extensions.Configuration;

namespace SQLMonitoring.DatabaseConnection
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SmartPredictionStats> SmartPredictionStats { get; set; }
        public DbSet<BasicResourceUsage> BasicResourceUsageStats { get; set; }
        public DbSet<BasicInformation> BasicInformationStats { get; set; }
        public DbSet<GlobalWaitStats> GlobalWaitStats { get; set; }
        public DbSet<GlobalSpinlockStats> GlobalSpinlockStats { get; set; }
        public DbSet<CPUStats> GlobalCPUStats { get; set; }
        public DbSet<MemoryStats> GlobalMemoryStats { get; set; }
        public DbSet<IOStats> GlobalIOStats { get; set; }
        public DbSet<BlockingAndDeadlocks> GlobalBlockingStats { get; set; }
    }
}
