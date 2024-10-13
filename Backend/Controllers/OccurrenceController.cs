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
using Backend.Models.DTOs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(404, Type = typeof(NotFoundResult))]
    [ProducesResponseType(400, Type = typeof(BadRequestResult))]
    [ProducesResponseType(500, Type = typeof(RequestError))]
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
        [ProducesResponseType(200, Type = typeof(List<Occurrence>))]
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

            [HttpGet("diversity/")]
            public async Task<List<Diversity>> getDiversity([FromQuery] int skip = 0, [FromQuery] int limit = 50, [FromQuery] string? sortBy = "interval_name", [FromQuery] string? sortDir = "ASC")
            {
                PaginationDTO pagination = new PaginationDTO()
                {
                    limit = limit,
                    skip = skip,
                    sortBy = sortBy,
                    sortDir = sortDir
                };
                var diversity = await _service.getDiversity(pagination);
                return diversity;
            }
            
            [HttpGet("diversity/{intervalName}/")]
            public async Task<ActionResult<Diversity>>getDiversityByIntervalName(string intervalName)
            {
                var diversity = await _service.getDiversityByIntervalName(intervalName);
                return diversity;
            }
 
        // GET: api/Interval/5
        [HttpGet("{id}")]

        public async Task<OccurrenceDTO> findById(int id)
        {
            var occurrence = await _service.Get(id);
            return occurrence;
        }
        
        
      
    }
}
