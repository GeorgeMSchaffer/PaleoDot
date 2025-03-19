using Backend.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Backend.Services;

public interface IOccurrenceService
{
    Task<List<OccurrenceDTO>> GetAll(PaginationDTO pagination);
    Task<OccurrenceDTO> Get(int id);
    Task<OccurrenceDTO> Add(OccurrenceDTO occurrence);
    Task<Occurrence?> Update(OccurrenceDTO occurrence);
    Task<bool> Delete(int id);
    Task<List<OccurrenceDTO>> GetOccurrencesByIntervalName(string intervalName, PaginationDTO pagination);
    Task<Diversity> getDiversityByIntervalName(string intervalName);
}

public class OccurrenceService : IOccurrenceService
{
    private readonly AppDbContext _context;
    private readonly ILogger<OccurrenceService> _logger;
    private readonly IMapper _mapper;

    public OccurrenceService(AppDbContext context, ILogger<OccurrenceService> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

public async Task<List<OccurrenceDTO>> GetAll(PaginationDTO pagination)
{
    var occurrences = await _context.Occurrences
        .Include(o => o.Interval) // Include the related Interval entity
        .Skip(pagination.skip)
        .Take(pagination.limit)
        .ToListAsync();

    _logger.LogInformation("Returning {Count} occurrences", occurrences.Count);

    var occurrenceDTOs = _mapper.Map<List<OccurrenceDTO>>(occurrences);
    return occurrenceDTOs;
}

    public async Task<Diversity> getDiversityByIntervalName(string intervalName)
    {
        var interval = await _context.Intervals.FirstOrDefaultAsync(i => i.IntervalName == intervalName);

        var result = await _context
            .Occurrences
            .Where(o => intervalName == "" ? o.EarlyInterval == intervalName : true)
            .GroupBy(o => o.EarlyInterval)
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                //[TODO:] Add back the one to many relation between interval and occurrence to use this
                color = "", // = g.Where(o => o.EarlyInterval == intervalName).Select(i => i.Interval.Color),
                IntervalName = g.Select(o => o.EarlyInterval).FirstOrDefault(),
                CountOfPhylum = g.Select(o => o.Phylum).Distinct().Count(),
                CountOfClasses = g.Select(o => o.Class).Distinct().Count(),
                CountOfOrders = g.Select(o => o.Order).Distinct().Count(),
                CountOfFamilies = g.Select(o => o.Family).Distinct().Count(),
                CountOfGenera = g.Select(o => o.Genus).Distinct().Count()
            })
            .FirstOrDefaultAsync();

        var dto = new Diversity
        {
            intervalName = result.IntervalName,
            CountOfPhylum = result.CountOfPhylum,
            CountOfClasses = result.CountOfClasses,
            CountOfOrders = result.CountOfOrders,
            CountOfFamilies = result.CountOfFamilies,
            CountOfGenera = result.CountOfGenera
        };

        return dto;
    }

    public async Task<List<Diversity>> getDiversity(PaginationDTO pagination)
    {
        var data = await _context
            .Occurrences
            .GroupBy(o => o.EarlyInterval)
            .Select(g => new
            {
                CountOfOccurences = g.Count(),
                CountOfPhylum = g.Select(o => o.Phylum).Distinct().Count(),
                CountOfClasses = g.Select(o => o.Class).Distinct().Count(),
                CountOfOrders = g.Select(o => o.Order).Distinct().Count(),
                CountOfFamilies = g.Select(o => o.Family).Distinct().Count(),
                CountOfGenera = g.Select(o => o.Genus).Distinct().Count(),
                interval = g.Select(o => o.EarlyInterval).FirstOrDefault(),
                maxMya = g.Select(o => o.MaxMa).FirstOrDefault(),
                minMya = g.Select(o => o.MinMa).FirstOrDefault(),
            })
            .Skip(pagination.skip)
            .Take(pagination.limit)
            .ToListAsync();

        var diversities = data.Select(d => new Diversity
        {
            minMya = d.minMya,
            maxMya = d.maxMya,
            intervalName = d.interval,
            CountOfPhylum = d.CountOfPhylum,
            CountOfClasses = d.CountOfClasses,
            CountOfOrders = d.CountOfOrders,
            CountOfFamilies = d.CountOfFamilies,
            CountOfGenera = d.CountOfGenera,
            CountOfOccurences = d.CountOfOccurences
        }).ToList();

        return diversities;
    }

    public async Task<OccurrenceDTO> Get(int id)
    {
        var occurrence = await _context.Occurrences.FindAsync(id);
        if (occurrence == null)
        {
            return new OccurrenceDTO();
        }
        var occurrenceDTO = _mapper.Map<OccurrenceDTO>(occurrence);

        return occurrenceDTO;
    }

    public async Task<OccurrenceDTO> Add(OccurrenceDTO occurrenceDTO)
    {
        var occurrence = _mapper.Map<Occurrence>(occurrenceDTO);

        _context.Occurrences.Add(occurrence);
        await _context.SaveChangesAsync();

        var occurrenceDTOtoReturn = _mapper.Map<OccurrenceDTO>(occurrence);
        return occurrenceDTOtoReturn;
    }

    public async Task<Occurrence?> Update(OccurrenceDTO occurrenceDTO)
    {
        var occurrenceEntity = await _context.Occurrences.FindAsync(occurrenceDTO.OccurrenceNo);
        if (occurrenceEntity == null)
        {
            return null;
        }

        _mapper.Map(occurrenceDTO, occurrenceEntity);
        _context.Update(occurrenceEntity);
        await _context.SaveChangesAsync();
        return occurrenceEntity;
    }

    public async Task<bool> Delete(int id)
    {
        var occurrence = await _context.Occurrences.FindAsync(id);
        if (occurrence == null)
        {
            return false;
        }

        _context.Remove(occurrence);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<OccurrenceDTO>> GetOccurrencesByIntervalName(string intervalName, PaginationDTO pagination)
    {
        var occurrences = await _context.Occurrences
            .Where(o => o.EarlyInterval == intervalName)
            .Skip(pagination.skip)
            .Take(pagination.limit)
            .ToListAsync();

        _logger.LogInformation("Returning {Count} occurrences", occurrences.Count);
        var occurrenceDTOs = _mapper.Map<List<OccurrenceDTO>>(occurrences);

        return occurrenceDTOs;
    }
}