using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Services;

namespace Backend.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public class IntervalController : ControllerBase
    {
        private readonly IntervalService _service;

        public IntervalController(IntervalService service)
        {
            _service = service;
        }

        // GET: api/Interval
        [HttpGet("")]
        public async Task<ActionResult<List<IntervalDTO>>> GetIntervals([FromQuery] int skip = 0, [FromQuery] int limit = 10, [FromQuery] string? sortBy = "interval_no", [FromQuery] string? sortDir = "ASC")
        {
            PaginationDTO pagination = new PaginationDTO()
            {
                limit = limit,
                skip = skip,
                sortBy = sortBy,
                sortDir = sortDir
            };
            var intervalDTOs =  await _service.GetIntervals(pagination);
            if(intervalDTOs == null)
            {
                return NotFound();
            }
            return Ok(intervalDTOs);
        }
        
        [HttpGet("occurrencces/{intervalName}")]
        public async Task<ActionResult<List<IntervalDTO>>> GetIntervalOccurrences([FromQuery] int skip = 0, [FromQuery] int limit = 10, [FromQuery] string? sortBy = "interval_no", [FromQuery] string? sortDir = "ASC", string intervalName = "Carnian")
        {
            PaginationDTO pagination = new PaginationDTO()
            {
                limit = limit,
                skip = skip,
                sortBy = sortBy,
                sortDir = sortDir
            };
            var intervalDTOs =  _service.getIntervalOccurrences(intervalName, pagination);
            if(intervalDTOs == null)
            {
                return NotFound();
            }
            return Ok(intervalDTOs);
        }
        
        // GET: api/Interval/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IntervalDTO>> GetInterval(int id)
        {
            try
            {
                var interval = await _service.findIntervalByID(id);

                if (interval == null)
                {
                    return NotFound();
                }

                return interval;
            }
            catch (Exception e)
            {
                //[TODO:] Log the error
                return StatusCode(500, e.Message);
            }
    }
}   
}
