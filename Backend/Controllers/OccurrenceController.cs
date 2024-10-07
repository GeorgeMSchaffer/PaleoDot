using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccurrenceController : ControllerBase
    {
        private readonly OccurrenceService _service;
        public OccurrenceController(OccurrenceService service)
        {
            _service = service;
        }

        // GET: api/Occurrence
        [HttpGet("")]
        public async Task<ActionResult<List<Occurrence>>> GetOccurrences([FromQuery] int skip = 0, [FromQuery] int limit = 10, [FromQuery] string? sortBy = "interval_no", [FromQuery] string? sortDir = "ASC")
        {
            PaginationDTO pagination = new PaginationDTO()
            {
                limit = limit,
                skip = skip,
                sortBy = sortBy,
                sortDir = sortDir
            };
//            var occurrences =  await _service.GetIntervals(pagination);
            var occurrences = await _service.GetAll(pagination);
                
                //_context.Occurrences.Skip(pagination.skip).Take(pagination.limit).ToListAsync();
            if(occurrences == null)
            {
                return NotFound();
            }
            return Ok(occurrences);
        }
 [HttpGet("interval/{intervalName}/")]
 
        // GET: api/Interval/5
        [HttpGet("{id}")]
        [ProducesResponseType(404, Type = typeof(NotFoundResult))]
        public async Task<List<OccurrenceDTO>> getOccurrencesByIntervalName(string intervalName,[FromQuery] int skip = 0, [FromQuery] int limit = 10, [FromQuery] string? sortBy = "interval_no", [FromQuery] string? sortDir = "ASC")
        {
            PaginationDTO pagination = new PaginationDTO()
            {
                limit = limit,
                skip = skip,
                sortBy = sortBy,
                sortDir = sortDir
            };
            if (pagination == null) throw new ArgumentNullException(nameof(pagination));

            List<OccurrenceDTO> occurrences = await _service.GetOccurrencesByIntervalName(intervalName, pagination);
            return occurrences;
        }
      
    }
}
