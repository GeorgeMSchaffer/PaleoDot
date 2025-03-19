
using AutoMapper;
using Backend.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.EntityFrameworkCore.Query;
using Backend.Utilities;
using Newtonsoft.Json;

namespace Backend.Services;
public interface IIntervalService
{
    public Task<List<Interval>> GetIntervals(PaginationDTO pagination);
    
    public Task<IntervalDTO> findIntervalByID(int id);
    public Task UpdateInterval(IntervalDTO intervalDTO);
    public List<IntervalDTO> getIntervalsByType(String type, PaginationDTO pagination);
    public List<IntervalDTO> getIntervalOccurrences(string intervalName, PaginationDTO pagination);

    List<IntervalDTO> getIntervalOccurrences(PaginationDTO pagination);
    
}
public class IntervalService : IIntervalService{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<IntervalService> _logger;
    
    public IntervalService(AppDbContext context,ILogger<IntervalService> logger, IMapper mapper)
    {
        this._context = context;
        this._logger = logger;
        this._mapper = mapper;
    }

    //[TODO:] 
    public List<IntervalDTO> getIntervalsByType(string type, PaginationDTO pagination)
    {
        _logger.LogInformation("Getting intervals by type - type: {Type}", type);
        var intervals = this._context.Intervals.Where(i => i.RecordType == type).Skip(pagination.skip)
            .Take(pagination.limit).ToList();
        var intervalDTOs = _mapper.Map<List<IntervalDTO>>(intervals);
        return intervalDTOs;
    }
    
   
    public async Task<List<Interval>> GetIntervals(PaginationDTO pagination)
    {
        
        var qry = await this._context.Intervals.ToListAsync();
       // .OrderBy(i => i.IntervalName)
        // .Skip(pagination.skip)
        // .Take(pagination.limit)
        // //.Include(i => i.Occurrences)
        // .OrderByPropertyOrField(pagination.orderBy, pagination.orderDir == "ASC")
        // .ToList<Interval>();

        //var intervals = await this._context.Intervals.Skip(pagination.skip).Take(pagination.limit).OrderBy(i => EF.Property<object>(i, pagination.orderBy)).ToListAsync();
        if (qry ==  null)
        {
            return new List<Interval>();
        }
        return qry;
    }
    public async Task<IntervalDTO> findIntervalByID(int id)
    {
        var interval = await this._context.Intervals.FindAsync(id);
        var intervalDTO = _mapper.Map<IntervalDTO>(interval);
    
        return intervalDTO;
    }

    public async Task AddInterval(IntervalDTO intervalDTO)
    {
        var interval = this._mapper.Map<Interval>(intervalDTO);
        _context.Intervals.Add(interval);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateInterval(IntervalDTO intervalDTO)
    {
        var interval = _mapper.Map<Interval>(intervalDTO);
        _context.Intervals.Update(interval);
        await _context.SaveChangesAsync();
    }


    public void DeleteInterval(int id)
    {
        var interval = _context.Intervals.Find(id);
        if(!(interval is null))
            _context.Intervals.Remove(interval);
         _context.SaveChanges();
    }
public List<IntervalDTO> getIntervalOccurrences(string intervalName, PaginationDTO pagination){
    
    Console.WriteLine($"\r\n---- Getting intervals by name - name: {intervalName} - skip: {pagination.skip}, limit: {pagination.limit}, sortby: {pagination.sortBy}, sort dir {pagination.orderDir}");
    var intervals = this._context.Intervals.ToList();
//        .Where(i => i.IntervalName == intervalName)
        // .Skip(pagination.skip)
        // .Take(pagination.limit)
        // .OrderByPropertyOrField(pagination.orderBy, pagination.orderDir == "ASC")
        // .Include(i => i.Occurrences)
        // .Where(i => i.IntervalName == intervalName)
        // .ToList();
     //   Console.WriteLine("\r\n---- Got {cnt} interval occurrences by name - name: {Name}",intervals, 
       // intervals.Count,intervalName);
        if(intervals.Count == 0)
        {
            return new List<IntervalDTO>();
        }
        var intervalDTOs = _mapper.Map<List<IntervalDTO>>(intervals);
        Console.WriteLine("\r\n---- Got {cnt} interval occurrences by name",intervalDTOs.Count);
        return intervalDTOs;
    }

        public List<IntervalDTO> getIntervalOccurrences(PaginationDTO pagination)
    {
        var intervals = this._context.Intervals.ToList();
        Console.WriteLine($"\r\n ---- Got {intervals.Count} interval occurrences");
        var intervalDTOs = _mapper.Map<List<IntervalDTO>>(intervals);
        return intervalDTOs;
    }

    public async Task ImportIntervalsFromJsonAsync(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("The specified JSON file was not found.", jsonFilePath);
            }

            var jsonData = await File.ReadAllTextAsync(jsonFilePath);
            var intervals = JsonConvert.DeserializeObject<List<Interval>>(jsonData);

            if (intervals == null)
            {
                throw new InvalidDataException("The JSON file does not contain valid Interval data.");
            }

           // try {
                await _context.Intervals.AddRangeAsync(intervals);
                await _context.SaveChangesAsync();
            //}
            // catch(Exception ex){

            // }
        }
}