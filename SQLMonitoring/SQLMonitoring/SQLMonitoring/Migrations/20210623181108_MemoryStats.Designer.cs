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
    [Migration("20210623181108_MemoryStats")]
    partial class MemoryStats
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasKey("Id");

                    b.ToTable("GlobalMemoryStats");
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
