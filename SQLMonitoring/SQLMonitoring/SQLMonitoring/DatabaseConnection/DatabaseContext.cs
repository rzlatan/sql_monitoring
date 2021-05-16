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
    }
}
