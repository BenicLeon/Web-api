﻿using Examlple.Web_Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Example.Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballClubController : ControllerBase
    {
        private readonly ILogger<FootballClubController> _logger;
        private static List<FootballClub> FootballClubs = new List<FootballClub>
        {
            new FootballClub { Id = 1, Name = "Chelsea FC", Country = "England", PlayersCount = 25 },
            new FootballClub { Id = 2, Name = "Real Madrid", Country = "Spain", PlayersCount = 28 },
            new FootballClub { Id = 3, Name = "Manchester United", Country = "England", PlayersCount = 22 },
            new FootballClub { Id = 4, Name = "Manchester City", Country = "England", PlayersCount = 30 },
            new FootballClub { Id = 5, Name = "Juventus", Country = "Italy", PlayersCount = 27 }
        };

        public FootballClubController(ILogger<FootballClubController> logger)
        {
            _logger = logger;
        }

        
        [HttpGet(Name = "GetFootballClubs")]
        public IEnumerable<FootballClub> Get()
        {
            return FootballClubs;
        }


        [HttpPost(Name = "AddFootballClub")]
        public ActionResult Post([FromBody] FootballClub club)
        {
            if (club == null)
            {
                return BadRequest("Club data is null.");
            }

            
            foreach (var existingClub in FootballClubs)
            {
                if (existingClub.Id == club.Id)
                {
                    return Conflict($"Club with ID {club.Id} already exists.");
                }
            }

            FootballClubs.Add(club);
            return Ok(club);
        }

        [HttpPost("AddFootballClubData")]
        public ActionResult Post(int id, int playerCount, string name)
        {
            foreach (var existingClub in FootballClubs)
            {
                if (existingClub.Id == id)
                {
                    return Conflict($"Club with ID {id} already exists.");
                }
            }

            
            var newClub = new FootballClub
            {
                Id = id,
                PlayersCount = playerCount,
                Name = name

            };

            
            FootballClubs.Add(newClub);

            
            return Ok(newClub);
        }
        [HttpPut("UpdateFootballClub/{id}")]
        public ActionResult Put(int id, [FromBody] FootballClub updatedClub)
        {
            
            FootballClub clubToUpdate = null;
            foreach (var existingClub in FootballClubs)
            {
                
                if (existingClub.Id == id)
                {
                    clubToUpdate = existingClub;
                    break; 
                }
            }

            if (clubToUpdate == null)
            {
                return NotFound($"Club with ID {id} not found.");
            }

           
            clubToUpdate.Name = updatedClub.Name;
            clubToUpdate.Country = updatedClub.Country;
            clubToUpdate.PlayersCount = updatedClub.PlayersCount;

            return Ok(clubToUpdate);
        }
        [HttpDelete("DeleteFootballClub/{id}")]
public ActionResult Delete(int id)
{
            FootballClub clubToDelete = null;
            foreach (var existingClub in FootballClubs)
            {

                if (existingClub.Id == id)
                {
                    clubToDelete = existingClub;
                    break;
                }
            }

            if (clubToDelete == null)
            {
                return NotFound($"Club with ID {id} not found.");
            }


            FootballClubs.Remove(clubToDelete);

    
    return Ok($"Club with ID {id} has been deleted.");
}
    }
}