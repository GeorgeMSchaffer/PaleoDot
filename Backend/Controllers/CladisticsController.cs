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
using Backend.POJO;
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

    public class CladisticsController : ControllerBase
    {
        private readonly CladisticsService _service;

        public CladisticsController(CladisticsService service)
        {
            _service = service;
        }

        // GET: api/Cladistics
        [HttpGet("")]
        public async Task<ActionResult<List<CladisticsDTO>>> GetCladisitics([FromQuery] int skip = 0, [FromQuery] int limit = 10, [FromQuery] string? sortBy = "interval_no", [FromQuery] string? sortDir = "ASC")
        {
            PaginationDTO pagination = new PaginationDTO()
            {
                limit = limit,
                skip = skip,
                sortBy = sortBy,
                sortDir = sortDir
            };
            var cladisticsDTOs = await _service.getAll(pagination);
            if(cladisticsDTOs == null)
            {
                return NotFound();
            }
            return Ok(cladisticsDTOs);
        }
        

        // GET: api/Cladistics/kindom
        [HttpGet("{rank}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<CladisticsDTO>> findByRank(EnumCladistics rank)
        {
                return  await _service.getClaedisticsByRank(rank);
        }
}   
}
