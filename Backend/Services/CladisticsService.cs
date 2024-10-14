using Backend.Models;
using Backend.Contexts;
using AutoMapper;
using Backend.Models.DTOs;
using Backend.POJO;
namespace Backend.Services;

public interface ICladisticsService
{
  public Task<List<CladisticsDTO>> getAll(PaginationDTO pagination);
  public Task<List<CladisticsDTO>> getClaedisticsByRank(EnumCladistics rank);
  public Task<CladisticsDTO> getById(int id);
}

public class CladisticsService : ICladisticsService
{
  private AppDbContext _context;
  private IMapper _mapper;
  private ILogger<CladisticsService> _logger;
    
  public CladisticsService(AppDbContext context,ILogger<CladisticsService> logger, IMapper mapper)
  {
    this._context = context;
    this._logger = logger;
    this._mapper = mapper;
  }
  public async Task<List<CladisticsDTO>> getAll(PaginationDTO pagination)
  {
    var results = _context.Cladistics.Skip(pagination.skip).Take(pagination.limit).ToList();
    return _mapper.Map<List<CladisticsDTO>>(results);
  }
  public async Task<List<CladisticsDTO>> getClaedisticsByRank(EnumCladistics rank)
  {
    var results = _context.Cladistics.Where(c => c.Rank == rank).ToList();
    return _mapper.Map<List<CladisticsDTO>>(results);

  }
  public async Task<CladisticsDTO> getById(int id)
  {
    var results =  _context.Cladistics.Where(c => c.Id == id).FirstOrDefault();
    return _mapper.Map<CladisticsDTO>(results);

  }
}