using chirpAPI.model;
using chirpAPI.Services;
using chirpAPI.Services.Model.DTOs;
using chirpAPI.Services.Model.Filters;
using chirpAPI.Services.Model.ViewModel;
using chirpAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chirpAPI.Controllers
{
    [Route("api/[controller]")]//todo poi chirpshashtag, hashtag e user !!
    [ApiController]
    public class ChirpsController : ControllerBase
    {
        private readonly IChirpsService _chirpsService;
        private readonly ILogger<ChirpsController> _logger;

        public ChirpsController(IChirpsService chirpsService, ILogger<ChirpsController> logger)
        {
            _chirpsService = chirpsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetChirpsByFilter([FromQuery] ChirpFilter filter)
        {
            _logger.LogInformation("Received request to get chirps with filter: {@Filter}", filter);

            List<ChirpViewModel> result = await _chirpsService.GetChirpsByFilter(filter);

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No chirps found for the given filter: {@Filter}", filter);
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Found {Count} chirps for the given filter: {@Filter}", result.Count, filter);
                return Ok(result);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllChirps()
        {
            var result = await _chirpsService.GetAllChirps();

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No chirps found.");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Found {Count} chirps.", result.Count);
                return Ok(result);
            }   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChirpById([FromRoute] int id)
        {
            var chirp = await _chirpsService.GetChirpById(id);

            if (chirp == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(chirp);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChirp([FromRoute] int id, [FromBody] ChirpUpdateDTO chirp)
        {
            var result = await _chirpsService.UpdateChirp(id, chirp);

            if (result == false)
            {
                return BadRequest("Chirp non esistente!");
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostChirp([FromBody] ChirpCreateDTO chirp)
        {
            var chirpId = await _chirpsService.CreateChirp(chirp);

            if (chirpId == null)
            {
                return BadRequest("Text obbligatorio!");
            }

            return Created($"api/Chirp/{chirp}", chirpId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChirp([FromRoute] int id)
        {
            int? result = await _chirpsService.DeleteChirp(id);

            if (result == null)
            {
                return BadRequest("Chirp non esistente!");
            }
            if (result == -1)
            {
                return BadRequest("Attenzione eliminare prima tutti i commenti associati alla Chirp!");
            }

            return Ok(result);
        }
    }
}
