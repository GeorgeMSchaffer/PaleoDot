using Backend.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
namespace Backend.Services;
using Backend.Models;
using AutoMapper;
using Backend.Services;

public interface IOccurrenceService
{
    Task<List<OccurrenceDTO>> GetAll(PaginationDTO pagination);
    Task<OccurrenceDTO> Get(int id);
    Task<OccurrenceDTO> Add(OccurrenceDTO occurrence);
    Task<Occurrence?> Update(OccurrenceDTO occurrence);
    Task<bool> Delete(int id);
    Task<List<OccurrenceDTO>> GetOccurrencesByIntervalName(String intervalName,PaginationDTO pagination);
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

    public object OccurrenceJsonDTO { get; set; }

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
