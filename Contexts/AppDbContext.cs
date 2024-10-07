using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Backend.Models;

namespace Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using AutoMapper;
using Backend.Mapper;

public class AppDbContext : DbContext
{
    private readonly IMapper _mapper;
    public IConfigurationProvider OccurrenceProfile { get; }
    public IConfigurationProvider IntervalProfile { get; }
    private readonly ILogger<DbContext> _logger;

    public AppDbContext(
        DbContextOptions<AppDbContext> options
        , IMapper _mapper
        )
        : base(options)
    {
        this._mapper = _mapper;
        this._logger = (ILogger<DbContext>?)_logger;
    }

    public DbSet<Interval> Intervals => Set<Interval>();
    public DbSet<Occurrence> Occurrences => Set<Occurrence>();
  //  public DbSet<Species> Species => Set<Species>();
   // public DbSet<Taxa> Taxas => Set<Taxa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
//         var occurrencesJSON = System.IO.File.ReadAllText("Data/carnian-occurrences.json");
//         var intervals = System.IO.File.ReadAllText("Data/intervals.json");
//        // var deserializedOccurrences = JsonSerializer.Deserialize<List<Occurrence>>(occurrences);
//        
//        
//        modelBuilder.Entity<Occurrence>().HasData(occurrencesJSON);
// //       modelBuilder.Entity<Interval>().HasData(intervals);
//         List<Occurrence> occurrences = _mapper.Map<List<Occurrence>>(occurrencesJSON);
//        _logger.LogInformation($"Try to insert {occurrences.Count} occurrences");
//        
        // modelBuilder.Entity<Occurrence>()
        //     .HasMany<Species>(navigationExpression: o => o.Species)
        //     .WithOne(navigationExpression: s => s.Occurrence);
        //    .HasForeignKey(foreignKeyExpression: s => s.AcceptedNo);

        // modelBuilder.Entity<Occurrence>()
        //     .HasOne(o => o.Interval)
        //     .WithMany(i => i.Occurrences)
        //     .HasForeignKey(o => o.EarlyIntervalNo);

       // modelBuilder.Entity<Occurrence>()
       //     .HasOne<Interval>(o => o.EarlyInterval);
           //.WithOne(o => o.EarlyInterval)
           //.HasForeignKey("interval_no");

     //  modelBuilder.Entity<Interval>().HasMany<Occurrence>().WithOne(o=>o.LateInterval);

     //  modelBuilder.Entity<Occurrence>().HasOne<Interval>();
       
        // modelBuilder.Entity<Occurrence>()
        //     .HasOne<Interval>(navigationExpression: o => o.Interval)
        //     .WithMany(navigationExpression: i => i.Occurrences)
        //     .HasForeignKey(foreignKeyExpression: o => o.EarlyInterval);
        //

        // modelBuilder.Entity<Species>()
        //     .HasMany(s => s.OccurrenceNo)
        base.OnModelCreating(modelBuilder);

    }

}
