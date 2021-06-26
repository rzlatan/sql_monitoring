﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SQLMonitoring.DatabaseConnection;

namespace SQLMonitoring.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210626005840_Common Problems")]
    partial class CommonProblems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SQLMonitoring.Model.BackupInformation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BackupDuration")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("BackupEndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("BackupLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("BackupSize")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("BackupStartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("BackupType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatabaseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("HoursSinceLastBackup")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastRestorablePoint")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GlobalBackupStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.BasicInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Collation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompatLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Edition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HyperThreadRatio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsHADREnabled")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsXTPEnabled")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Memory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessorCount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkerCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BasicInformationStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.BasicResourceUsage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("BatchRequests")
                        .HasColumnType("float");

                    b.Property<double>("CpuUsage")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("MemoryUsage")
                        .HasColumnType("float");

                    b.Property<double>("NetworkUsage")
                        .HasColumnType("float");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UserConnections")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("BasicResourceUsageStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.BlockingAndDeadlocks", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Blocked")
                        .HasColumnType("int");

                    b.Property<int?>("DatabaseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DurationMin")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProcessId")
                        .HasColumnType("int");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TotalDeadlocks")
                        .HasColumnType("bigint");

                    b.Property<long?>("TransactionId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("WaitResource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("WaitTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("GlobalBlockingStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.CPUStats", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CPUTotalTime")
                        .HasColumnType("bigint");

                    b.Property<long?>("CPUUserTime")
                        .HasColumnType("bigint");

                    b.Property<long?>("CompileCPUTime")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ExecCPUTime")
                        .HasColumnType("bigint");

                    b.Property<long?>("QueryExecTime")
                        .HasColumnType("bigint");

                    b.Property<string>("QueryHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("WorkloadGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("WorkloadGroupCPUTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("GlobalCPUStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.CommonProblems", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<double?>("CurrentSizeMb")
                        .HasColumnType("float");

                    b.Property<long?>("DatabaseId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DbName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Duration")
                        .HasColumnType("bigint");

                    b.Property<string>("EqualityColumns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("FreeSpaceMb")
                        .HasColumnType("float");

                    b.Property<int?>("Growth")
                        .HasColumnType("int");

                    b.Property<long?>("HoursSinceLastBackup")
                        .HasColumnType("bigint");

                    b.Property<string>("InequalityColumns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GlobalCommonProblems");
                });

            modelBuilder.Entity("SQLMonitoring.Model.Database", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BasicInformationId")
                        .HasColumnType("int");

                    b.Property<int>("DatabaseId")
                        .HasColumnType("int");

                    b.Property<string>("DatabaseSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsEncrypted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecoveryModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SnapshotIsolationLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BasicInformationId");

                    b.ToTable("Database");
                });

            modelBuilder.Entity("SQLMonitoring.Model.DatabaseFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AutoGrowth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BasicInformationId")
                        .HasColumnType("int");

                    b.Property<int?>("DbId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaxSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BasicInformationId");

                    b.HasIndex("DbId");

                    b.ToTable("DatabaseFile");
                });

            modelBuilder.Entity("SQLMonitoring.Model.GlobalSpinlockStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Backoffs")
                        .HasColumnType("bigint");

                    b.Property<long>("Collisions")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Server")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Spinlock")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GlobalSpinlockStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.GlobalWaitStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long>("MaxWaitTimeMs")
                        .HasColumnType("bigint");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SignalWaitTimeMs")
                        .HasColumnType("bigint");

                    b.Property<long>("WaitTimeMs")
                        .HasColumnType("bigint");

                    b.Property<string>("WaitType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GlobalWaitStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.IOStats", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlanId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ReadIOPS")
                        .HasColumnType("bigint");

                    b.Property<long>("ReadLatency")
                        .HasColumnType("bigint");

                    b.Property<long>("ReadMBps")
                        .HasColumnType("bigint");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TotalIOPS")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalIOs")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalMBps")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalReadIOs")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalWriteIOs")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<long>("WriteIOPS")
                        .HasColumnType("bigint");

                    b.Property<long>("WriteLatency")
                        .HasColumnType("bigint");

                    b.Property<long>("WriteMBps")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("GlobalIOStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.MemoryStats", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BufferCacheHitRatio")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemoryClerk")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MemoryClerkSize")
                        .HasColumnType("bigint");

                    b.Property<long>("PageLifeExpectancy")
                        .HasColumnType("bigint");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TargetMemory")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalMemory")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GlobalMemoryStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.QueriesStats", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("AvgCpuTime")
                        .HasColumnType("float");

                    b.Property<double?>("AvgIOsPerExecution")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ExecCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastExecTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("QueryHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("TotalLogicalReads")
                        .HasColumnType("bigint");

                    b.Property<long?>("TotalLogicalWrites")
                        .HasColumnType("bigint");

                    b.Property<long?>("TotalWorkerTime")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GlobalQueryStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.Report", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResultPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ServerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("SQLMonitoring.Model.Server", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SmartPredictionsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("SQLMonitoring.Model.SmartPredictionStats", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CpuUsage")
                        .HasColumnType("float");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<double>("DiskLatency")
                        .HasColumnType("float");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<double>("MemoryUsage")
                        .HasColumnType("float");

                    b.Property<int>("Minute")
                        .HasColumnType("int");

                    b.Property<double>("NetworkUsage")
                        .HasColumnType("float");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SmartPredictionStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.TempdbStats", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DataSizeMb")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FileId")
                        .HasColumnType("int");

                    b.Property<int?>("FileMaxSize")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FileSize")
                        .HasColumnType("int");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Growth")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LogSizeMb")
                        .HasColumnType("int");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GlobalTempdbStats");
                });

            modelBuilder.Entity("SQLMonitoring.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SQLMonitoring.Model.Database", b =>
                {
                    b.HasOne("SQLMonitoring.Model.BasicInformation", null)
                        .WithMany("Databases")
                        .HasForeignKey("BasicInformationId");
                });

            modelBuilder.Entity("SQLMonitoring.Model.DatabaseFile", b =>
                {
                    b.HasOne("SQLMonitoring.Model.BasicInformation", null)
                        .WithMany("DatabaseFiles")
                        .HasForeignKey("BasicInformationId");

                    b.HasOne("SQLMonitoring.Model.Database", "Db")
                        .WithMany()
                        .HasForeignKey("DbId");

                    b.Navigation("Db");
                });

            modelBuilder.Entity("SQLMonitoring.Model.Report", b =>
                {
                    b.HasOne("SQLMonitoring.Model.Server", "Server")
                        .WithMany("ReportList")
                        .HasForeignKey("ServerId");

                    b.HasOne("SQLMonitoring.Model.User", "User")
                        .WithMany("ReportList")
                        .HasForeignKey("UserId");

                    b.Navigation("Server");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SQLMonitoring.Model.Server", b =>
                {
                    b.HasOne("SQLMonitoring.Model.User", "Owner")
                        .WithMany("ServerList")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SQLMonitoring.Model.BasicInformation", b =>
                {
                    b.Navigation("DatabaseFiles");

                    b.Navigation("Databases");
                });

            modelBuilder.Entity("SQLMonitoring.Model.Server", b =>
                {
                    b.Navigation("ReportList");
                });

            modelBuilder.Entity("SQLMonitoring.Model.User", b =>
                {
                    b.Navigation("ReportList");

                    b.Navigation("ServerList");
                });
#pragma warning restore 612, 618
        }
    }
}
