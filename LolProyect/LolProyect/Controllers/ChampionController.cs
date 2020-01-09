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
    [Route("api/region/{regionId:int}/champions")]
    public class ChampionController : ControllerBase
    {
            private IChampionService championService;

            public ChampionController(IChampionService championService)
            {
                this.championService = championService;
            }

            [HttpGet()]
            public async Task<ActionResult<IEnumerable<Champion>>> getChampions(int regionId)
            {
                try
                {
                    return Ok(await championService.GetChampions(regionId));
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }

            }

            [HttpPost()]
            public async Task<ActionResult<Champion>> PostChampion(int regionId, [FromBody] Champion champion)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    var newSonge = await championService.CreateChampionAsync(regionId, champion);
                    return Created($"/api/region/{regionId}/champs/{champion.Id}", newSonge);
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

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

            [HttpGet("{id:int}")]
            public async Task<ActionResult<Champion>> getChampion(int regionId, int id)
            {
                try
                {
                    var songe = await championService.GetChampionAsync(regionId, id);
                    return Ok(songe);
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            [HttpDelete("{champId:int}")]
            public async Task<ActionResult<bool>> DeleteChampion(int champId, int regionId)
            {
                try
                {
                    var NoMoreSonge = await championService.DeleteChampion(regionId, champId);
                    return Ok(NoMoreSonge);
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



            [HttpPut("{champId:int}")]
            public async Task<ActionResult<Champion>> PutChampion(int regionId, int champId, [FromBody] Champion champion)
            {
                try
                {
                    return Ok(await championService.UpdateChampionAsync(regionId, champId, champion));
                }
                catch
                {
                    throw new Exception("Not possible to show");
                }
        }
        }
}
