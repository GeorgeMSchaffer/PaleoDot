using Backend.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models;
using AutoMapper;
using Backend.Services;
namespace Backend.Services;

public interface IOccurrenceService
{
    Task<List<OccurrenceDTO>> GetAll(PaginationDTO pagination);
    Task<OccurrenceDTO> Get(int id);
    Task<OccurrenceDTO> Add(OccurrenceDTO occurrence);
    Task<Occurrence?> Update(OccurrenceDTO occurrence);
    Task<bool> Delete(int id);
    Task<List<OccurrenceDTO>> GetOccurrencesByIntervalName(String intervalName,PaginationDTO pagination);
    public Task<DiversityCountsDTO> getDiversityByIntervalName(string intervalName);
    
}

public class OccurrenceService : IOccurrenceService
{
    // Assuming there is a DbContext instance
    private readonly AppDbContext _context;
    private readonly ILogger<OccurrenceService> _logger;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration config = new MapperConfiguration(cfg =>
    { 
        cfg.CreateMap<Occurrence, OccurrenceDTO>();
        cfg.CreateMap<OccurrenceDTO, Occurrence>();
    }); 

    //private ILogger logger;
    public OccurrenceService(AppDbContext context,ILogger<OccurrenceService> logger, IMapper _mapper)
    {
        this._context = context;
        this._logger = (ILogger<OccurrenceService>?)logger;
       this._mapper = _mapper;
    }
    public  async Task<List<OccurrenceDTO>> GetAll(PaginationDTO pagination)
    {
        var occurrences = await _context.Occurrences.Skip(pagination.skip).Take(pagination.limit).ToListAsync();
        _logger.LogInformation("Returning Occurrence  " + occurrences.Count + " occurrences");
        //logger.LogInformation("Returning Occurrence with ID " + occurrences. + " occurrences");
        
        var occurrenceDTOs = _mapper.Map<List<OccurrenceDTO>>(occurrences);
        return occurrenceDTOs;
    }

    public async Task<DiversityCountsDTO> getDiversityByIntervalName(string intervalName = "")
    {
        var result = _context
            .Occurrences
                // Trying to do a conditional where we get diversity for all intervals if the intervalName is empty
            .Where(o => intervalName== "" ? o.EarlyInterval == intervalName: 1==1)
            .GroupBy(o => o.EarlyInterval)
            .Select(g => new
            {
                IntervalName = g.Select(o=>o.EarlyInterval).FirstOrDefault(),
                CountOfPhylum = g.Select(o => o.Phylum).Distinct().Count(),
                CountOfClasses = g.Select(o => o.Class).Distinct().Count(),
                CountOfOrders = g.Select(o => o.Order).Distinct().Count(),
                CountOfFamilies = g.Select(o => o.Family).Distinct().Count(),
                CountOfGenera = g.Select(o => o.Genus).Distinct().Count(),
            })
            .FirstOrDefault();    

        return new DiversityCountsDTO(result.IntervalName,result.CountOfPhylum, result.CountOfClasses, result.CountOfOrders, result.CountOfFamilies, result.CountOfGenera);
    }
    
    public async Task<List<DiversityCountsDTO>> getDiversity(string intervalName = "")
    {
        List<DiversityCountsDTO> diversities = new List<DiversityCountsDTO>();
        var data = _context
            .Occurrences
            // Trying to do a conditional where we get diversity for all intervals if the intervalName is empty
            .GroupBy(o => o.EarlyInterval)
            .Select(g => new
            {
                CountOfPhylum = g.Select(o => o.Phylum).Distinct().Count(),
                CountOfClasses = g.Select(o => o.Class).Distinct().Count(),
                CountOfOrders = g.Select(o => o.Order).Distinct().Count(),
                CountOfFamilies = g.Select(o => o.Family).Distinct().Count(),
                CountOfGenera = g.Select(o => o.Genus).Distinct().Count(),
                interval = g.Select(o=>o.EarlyInterval).FirstOrDefault()
            }).ToListAsync();
        data.Result.ForEach(
            data =>
            {
                var dto = new DiversityCountsDTO(
                   data.interval,
                   data.CountOfPhylum,
                   data.CountOfClasses,
                   data.CountOfOrders,
                   data.CountOfFamilies,
                   data.CountOfGenera
                );
                diversities.Add(dto);
            });
        return diversities;
        //return new DiversityCountsDTO(result, result.CountOfClasses, result.CountOfOrders, result.CountOfFamilies, result.CountOfGenera);
    }

    public async Task<OccurrenceDTO> Get(int id)
    {
        var occurrence = await _context.Occurrences.FindAsync(id);
        if (occurrence == null)
        {
            return null;
        }
        var occurrenceDTOs = _mapper.Map<OccurrenceDTO>(occurrence);

        return occurrenceDTOs;
    }

    public async Task<OccurrenceDTO> Add(OccurrenceDTO occurrenceDTO)
    {
        
        var occurrence = new Occurrence
        {
            // Map OccurrenceDTO properties to Occurrence 
        };

        _context.Occurrences.Add(occurrence);
        await _context.SaveChangesAsync();
        occurrence.OccurrenceNo = occurrence.OccurrenceNo;
        var occurrenceDTOtoReturn = _mapper.Map<OccurrenceDTO>(occurrence);

        return occurrenceDTOtoReturn;
    }

    public async Task<Occurrence?> Update(OccurrenceDTO occurrence)
    {
        Occurrence? occurrenceDto = await _context.Occurrences.FindAsync(occurrence.OccurrenceNo);
        

        // Map OccurrenceDTO properties to Occurrence 
        _context.Update(occurrenceDto);
        await _context.SaveChangesAsync();
        return occurrenceDto;
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

    public async Task<List<OccurrenceDTO>> GetOccurrencesByIntervalName(String intervalName,PaginationDTO pagination)
    {
        //[TODO:] We could check if the interval being matched has chidlren intervals then if someone searches for Permian it would include any 
        var occurrences = await _context.Occurrences.Where(o => o.EarlyInterval == intervalName)
            .Skip(pagination.skip)
            .Take(pagination.limit)
            .ToListAsync();
        _logger.LogInformation("Returning Occurrence  " + occurrences.Count + " occurrences");
        var occurrenceDTOtoReturn = _mapper.Map<List<OccurrenceDTO>>(occurrences);

        return occurrenceDTOtoReturn; 
    }
}
