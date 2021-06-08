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
    [Migration("20210608210128_resoure usage stats")]
    partial class resoureusagestats
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
