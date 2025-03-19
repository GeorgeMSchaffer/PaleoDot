using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Backend.Models;
using Microsoft.Extensions.Logging;

namespace Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using AutoMapper;
using Backend.Mapper;
using System.Diagnostics;
using SQLitePCL;
public class AppDbContext : DbContext
    {
        private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public DbSet<Interval> Intervals { get; set; }
        public DbSet<Occurrence> Occurrences { get; set; }
        public DbSet<Cladistics> Cladistics { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(_loggerFactory) // Use the logger factory
                    .EnableSensitiveDataLogging() // Enable sensitive data logging
                    .EnableDetailedErrors(); // Enable detailed errors
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Interval>()
                .HasMany(i => i.Occurrences)
                .WithOne(o => o.Interval)
                .HasForeignKey(o => o.EarlyIntervalNo);
        }
    }