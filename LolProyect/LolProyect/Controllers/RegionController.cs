using LolProyect.Exceptions;
using LolProyect.Models;
using LolProyect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect.Controllers
{
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        private IRegionService regionService;

        public RegionController(IRegionService regionService)
        {
            this.regionService = regionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions(string orderBy = "Id", bool showChamps= false)
        {
            try
            {
                return Ok(await regionService.GetRegionsAsync(orderBy, showChamps));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");

            }
        }
        [HttpGet("{regionId:int}")]
        public async Task<ActionResult<Region>> GetRegion(int regionId, bool showChamps = true)
        {
            try
            {
                var region = await this.regionService.GetRegionAsync(regionId, showChamps);
                return Ok(region);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Region>> PostRegion([FromBody] Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newRegion= await this.regionService.CreateRegionAsync(region);
            return Created($"/api/regions/{newRegion.Id}", newRegion);
        }

        [HttpDelete("{regionId:int}")]
        public async Task<ActionResult<bool>> DeleteRegion(int regionId)
        {
            try
            {
                return Ok(await this.regionService.DeleteRegionAsync(regionId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }

        [HttpPut("{regionId}")]
        public async Task<ActionResult<Region>> UpdateRegion(int regionId, [FromBody] Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var regionUpdated = await this.regionService.UpdateRegionAsync(regionId, region);
                return Ok(regionUpdated);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }
    }
}
