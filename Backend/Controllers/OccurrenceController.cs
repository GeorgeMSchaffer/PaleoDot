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
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
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
        [HttpGet("", Name = "GetOccurrences")]
        [SwaggerResponse(200, Type = typeof(List<Occurrence>))]
        public async Task<ActionResult<List<Occurrence>>> GetOccurrences
        (
            [FromQuery,SwaggerParameter("The number of records to skip.")] int skip = 0, 
            [FromQuery,SwaggerParameter("The number of records to return. With the maximum of 999")] int limit = 25, 
            [FromQuery] 
            string orderBy = "interval_no", 
            [FromQuery]
            string sortDir = "ASC", 
            [FromQuery] 
        string? filterBy = null)
        {
            //[TODO:] Create a filter type that can be passed to all search functions getIntervals(filters,pagination)
            PaginationDTO pagination = new PaginationDTO()
            {
                limit = limit,
                skip = skip,
                sortBy = orderBy,
                orderDir = sortDir
            };


            // Checking if the filter is a valid property of the Occurrence class
            if (!string.IsNullOrEmpty(filterBy))
            {
                var occurrenceProperties = typeof(Occurrence).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                bool isValidProperty = occurrenceProperties.Any(prop => prop.Name.Equals(filterBy, StringComparison.OrdinalIgnoreCase));

                if (!isValidProperty)
                {
                    return BadRequest($"Invalid Filter Field property: {filterBy}");
                }
            }
            //[TODO:] We need to determine the field(s) to filter and the value(s) to filter by

//            var occurrences =  await _service.GetIntervals(pagination);
            var occurrences = await _service.GetAll(pagination);
                
                //_context.Occurrences.Skip(pagination.skip).Take(pagination.limit).ToListAsync();
            if(occurrences == null)
            {
                return NotFound();
            }
            return Ok(occurrences);
        }

            [HttpGet("diversity/", Name = "GetDiversity")]
            [SwaggerResponse(200, Type = typeof(List<Diversity>))]
            public async Task<List<Diversity>> getDiversity(
                [FromQuery] 
                int skip = 0, 
                [FromQuery] 
                int limit = 50,
                [FromQuery] 
                string orderBy = "interval_name",
                [FromQuery] 
                string sortDir = "ASC")
            {
                PaginationDTO pagination = new PaginationDTO()
                {
                    limit = limit,
                    skip = skip,
                    sortBy = orderBy,
                    orderDir = sortDir
                };
                var diversity = await _service.getDiversity(pagination);
                return diversity;
            }
            
            [HttpGet("diversity/{intervalName}/", Name = "GetDiversityByIntervalName")]
            public async Task<ActionResult<Diversity>>getDiversityByIntervalName(string intervalName)
            {
                var diversity = await _service.getDiversityByIntervalName(intervalName);
                return diversity;
            }
 
        // GET: api/Interval/5
        [HttpGet("{id}", Name = "GetOccurrence")]

        public async Task<OccurrenceDTO> findById(int id)
        {
            var occurrence = await _service.Get(id);
            return occurrence;
        }
        
        
      
    }
}
