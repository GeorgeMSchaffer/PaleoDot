using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Models.DTOs;
using Backend.Services;
using Backend.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status201Created)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public class IntervalController : ControllerBase
    {
        private readonly IntervalService _service;

        public IntervalController(IntervalService service)
        {
            _service = service;
        }

        // GET: api/Interval
        [HttpGet("", Name="GetIntervals")]
        public async Task<ActionResult<List<IntervalDTO>>> GetIntervals(
            [FromQuery] 
            int skip = 0, 
            [FromQuery] 
            int limit = 10, 
            [FromQuery] 
            string orderBy = "MinMya", 
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
            var intervalDTOs = await _service.GetIntervals(pagination);
            if (intervalDTOs == null)
            {
                return NotFound();
            }
            return Ok(intervalDTOs);
        }

        [HttpGet("occurrences/{intervalName}", Name="GetIntervalOccurrences")]
        [SwaggerOperation("Returns a list of occurrences for a given interval name")]
        public ActionResult<List<IntervalDTO>> GetIntervalOccurrences(
            [FromQuery,SwaggerParameter("The number of records to skip. Defaulted to 0.")] 
            int skip = 0, 
            [FromQuery, SwaggerParameter("The number of records to return. Defaulted to 10 maximum of 999.")]
            int limit = 10, 
            [FromQuery, SwaggerParameter("The field to sort the results by. Defaulted to the Start MYA of the interval")]
            string orderBy = "MinMya", 
            [FromQuery, SwaggerParameter("The direction to sort the results in.", Required = true), DefaultValue("ASC")]
            string sortDir = "ASC", 
            [FromQuery, SwaggerParameter("The name of the interval to search for.")]
            string? intervalName = null)
        {
            if (limit > 999)
            {
                limit = 999;
            }
            PaginationDTO pagination = new PaginationDTO()
            {
                limit = limit,
                skip = skip,
                sortBy = orderBy,
                orderDir = sortDir
            };
            var intervalDTOs = _service.getIntervalOccurrences(intervalName, pagination);
            if (intervalDTOs == null)
            {
                return NotFound();
            }
            return Ok(intervalDTOs);
        }

      [HttpGet("occurrences/", Name="GetAllIntervalOccurrences")]
        [SwaggerOperation("Returns a list of occurrences for a given interval name")]
        public ActionResult<List<IntervalDTO>> GetAllIntervalOccurrences(
            [FromQuery,SwaggerParameter("The number of records to skip. Defaulted to 0.")] 
            int skip = 0, 
            [FromQuery, SwaggerParameter("The number of records to return. Defaulted to 10 maximum of 999.")]
            int limit = 10, 
            [FromQuery, SwaggerParameter("The field to sort the results by. Defaulted to the Start MYA of the interval")]
            string orderBy = "MinMya", 
            [FromQuery, SwaggerParameter("The direction to sort the results in.", Required = true), DefaultValue("ASC")]
            string orderDir = "ASC", 
            [FromQuery, SwaggerParameter("The name of the interval to search for.")]
            string? intervalName = null)
        {
            if (limit > 999)
            {
                limit = 999;
            }
            PaginationDTO pagination = new PaginationDTO()
            {
                limit = limit,
                skip = skip,
                sortBy = "intervalName",
                orderDir = orderDir
            };
            List<IntervalDTO> intervalDTOs;
                            intervalDTOs = _service.getIntervalOccurrences(pagination);

            // if(intervalName == null)
            // {
            //     intervalDTOs = _service.getIntervalOccurrences(pagination);
            //     if (intervalDTOs == null)
            //     {
            //         return NotFound();
            //     }
            //     return Ok(intervalDTOs);
            // }
            // else{
            //      intervalDTOs = _service.getIntervalOccurrences(intervalName, pagination);
            // }
            // if (intervalDTOs == null)
            // {
            //     return NotFound();
            // }
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

                return Ok(interval);
            }
            catch (Exception e)
            {
                // Log the error
                return StatusCode(500, e.Message);
            }
        }
    }
}