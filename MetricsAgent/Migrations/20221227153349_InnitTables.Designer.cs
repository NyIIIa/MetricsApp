﻿// <auto-generated />
using MetricsAgent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MetricsAgent.Migrations
{
    [DbContext(typeof(MetricsDbContext))]
    [Migration("20221227153349_InnitTables")]
    partial class InnitTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MetricsAgent.Models.CpuMetric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CurrentTimeInMilliseconds")
                        .HasColumnType("bigint")
                        .HasColumnName("CurrentDate");

                    b.Property<double>("Utilization")
                        .HasMaxLength(100)
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("CpuMetrics");
                });

            modelBuilder.Entity("MetricsAgent.Models.GpuMetric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CurrentTimeInMilliseconds")
                        .HasColumnType("bigint")
                        .HasColumnName("CurrentDate");

                    b.Property<double>("Utilization")
                        .HasMaxLength(100)
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("GpuMetrics");
                });

            modelBuilder.Entity("MetricsAgent.Models.RamMetric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CurrentTimeInMilliseconds")
                        .HasColumnType("bigint")
                        .HasColumnName("CurrentDate");

                    b.Property<double>("Utilization")
                        .HasMaxLength(100)
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("RamMetrics");
                });
#pragma warning restore 612, 618
        }
    }
}
