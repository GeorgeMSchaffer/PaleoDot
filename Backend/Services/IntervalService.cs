
using AutoMapper;
using Backend.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTOs;
namespace Backend.Services;
public interface IIntervalService
{
    public Task<List<Interval>> GetIntervals(PaginationDTO pagination);
    public Task<IntervalDTO> findIntervalByID(int id);
    public void UpdateInterval(IntervalDTO intervalDTO);
    public List<IntervalDTO> getIntervalsByType(String type, PaginationDTO pagination);
    
}
public class IntervalService : IIntervalService{
    private AppDbContext _context;
    private IMapper _mapper;
    private ILogger<IntervalService> _logger;
    
    public IntervalService(AppDbContext context,ILogger<IntervalService> logger)
    {
        this._context = context;
        this._logger = logger;
    }

    //[TODO:] 
    public List<IntervalDTO> getIntervalsByType(string intervalType, PaginationDTO pagination)
    {
        var intervals = _context.Intervals.Where(i => i.RecordType == intervalType).Skip(pagination.skip)
            .Take(pagination.limit).ToList();
        var intervalDTOs = _mapper.Map<List<IntervalDTO>>(intervals);
        return intervalDTOs;
    }
    
    public List<IntervalDTO> getIntervalOccurrences(string intervalName, PaginationDTO pagination)
    {
        var intervals = _context.Intervals
            .Include(i => i.Occurrences)
            .OrderBy(i=>i.IntervalName)
            .Where(i => i.IntervalName == intervalName)
            
            .Skip(pagination.skip)
            .Take(pagination.limit)
            .ToList();

        var intervalDTOs = _mapper.Map<List<IntervalDTO>>(intervals);
        return intervalDTOs;
    }
    // public async Task<List<OccurrenceDTO>> getOccurrencesByIntervalName(string intervalName,Pagination pagination)
    // {
    //     var intervals =  await _context.Intervals
    //         .Where<Interval>(i => i.IntervalName == intervalName)
    //         .Take(pagination.limit)
    //         .Skip(pagination.skip)
    //         .ToListAsync();
    //     // The interval name was not found so we need to bail out
    //    _logger.LogInformation($"Found {intervals.Count()} intervals with name {intervalName}");
    //     if(intervals.Count == 0)
    //     {
    //         return new List<OccurrenceDTO>();
    //     }
    //     
    //     var occurrences = await _context.Occurrences
    //             .Where<Occurrence>(o => o.EarlyInterval == intervals.First().IntervalName)
    //             .ToListAsync();
    //     _logger.LogInformation($"Found {occurrences.Count()} occurrences for interval name {intervalName}");
    //     var occurrenceDTOs = _mapper.Map<List<OccurrenceDTO>>(occurrences);
    //     return occurrenceDTOs;
    // }
    public async Task<List<Backend.Models.Interval>> GetIntervals(PaginationDTO pagination)
    {

        //[TODO:] Add ordering
        var intervals = await _context.Intervals.Skip(pagination.skip).Take(pagination.limit).ToListAsync();
        if (intervals ==  null)
        {
            return new List<Interval>();
        }
        //var intervalDTOs = _mapper.Map<List<IntervalDTO>>(intervals);
        return intervals;
    }
    
    public async Task<List<Occurrence>> getOccurrencesByIntervalName(String intervalName,PaginationDTO pagination)
    {
        var occurrences = await _context.Occurrences
            .Where(o => o.EarlyInterval == intervalName)
            .Skip(pagination.skip)
            .Take(pagination.limit)
            .ToListAsync();
        // var result = await _context.Intervals // Left table
        //      .Join(_context.Occurrences, // Right table
        //         interval => interval.IntervalNo, // Left Key 
        //         occurrence => occurrence.EarlyIntervalNo, // Right Key
        //         // Projection to create new Tuple-like instance with information from both tables
        //         //(interval, occurrence) => new { Interval = interval, Occurrence = occurrence }) //result selector
        //         (occurrence) => new { Occurrence = occurrence }).ToListAsync<Occurrence>();
        return occurrences;
    }
    public async Task<IntervalDTO> findIntervalByID(int id)
    {
        var interval = _context.Intervals.Find(id);
        var intervalDTO = _mapper.Map<IntervalDTO>(interval);
        //var intervalJsonDTO = _mapper.Map<IntervalJsonDTO>(interval);

        return intervalDTO;
    }

    public async void AddInterval(IntervalDTO intervalDTO)
    {
        var interval = _mapper.Map<Interval>(intervalDTO);
        _context.Intervals.Add(interval);
        await _context.SaveChangesAsync();
    }


    public void UpdateInterval(IntervalDTO intervalDTO)
    {
        var interval = _mapper.Map<Interval>(intervalDTO);
        _context.Intervals.Update(interval);
        _context.SaveChanges();
    }


    public void DeleteInterval(int id)
    {
        var interval = _context.Intervals.Find(id);
        _context.Intervals.Remove(interval);
        _context.SaveChanges();
    }

}