﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SQLMonitoring.DatabaseConnection;

namespace SQLMonitoring.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Servers");
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
                    b.HasOne("SQLMonitoring.Model.User", null)
                        .WithMany("ServerList")
                        .HasForeignKey("UserId");
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
