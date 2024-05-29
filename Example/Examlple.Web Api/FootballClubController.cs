using Examlple.Web_Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Example.Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballClubController : ControllerBase
    {
        private readonly ILogger<FootballClubController> _logger;
        private static List<FootballClub> FootballClubs = new List<FootballClub>
        {
            
        };

        public FootballClubController(ILogger<FootballClubController> logger)
        {
            _logger = logger;
        }


        [HttpGet("GetFootballClubs")]
        public IEnumerable<FootballClub> Get()
        {
            return FootballClubs;
        }


        [HttpPost("AddFootballClub")]
        public HttpResponseMessage Post(FootballClub club)
        {
            if (club == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Club data is null.") };
            }


            if (FootballClubs.Any(existingClub => existingClub.Id == club.Id))
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict) { Content = new StringContent($"Club with ID {club.Id} already exists.") };
            }

            FootballClubs.Add(club);
            return new HttpResponseMessage(HttpStatusCode.OK) { 
                Content = new StringContent("Club added successfully.")
            };
        }

        [HttpPost("AddFootballClubData")]
        public HttpResponseMessage Post(int id, int playerCount, string name)
        {
            if (FootballClubs.Any(existingClub => existingClub.Id == id))
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict) { 
                    Content = new StringContent($"Club with ID {id} already exists.")
                };
            }


            var newClub = new FootballClub
            {
                Id = id,
                PlayersCount = playerCount,
                Name = name

            };


            FootballClubs.Add(newClub);


            return new HttpResponseMessage(HttpStatusCode.OK) { 
                Content = new StringContent("Club added successfully.") 
            };
        }

        [HttpPut("UpdateFootballClub/{id}")]
        public HttpResponseMessage Put(int id,FootballClub updatedClub)
        {
            if (FootballClubs.Count == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No clubs available to update.")
                };
            }


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
                return new HttpResponseMessage(HttpStatusCode.NotFound) { 
                    Content = new StringContent($"Club with ID {id} not found.") 
                };
            }


            clubToUpdate.Name = updatedClub.Name;
            clubToUpdate.Country = updatedClub.Country;
            clubToUpdate.PlayersCount = updatedClub.PlayersCount;

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Club updated successfully.") };
        }
        [HttpDelete("DeleteFootballClub/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (FootballClubs.Count == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No clubs available to delete.")
                };
            }


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
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"Club with ID {id} not found.")
                };
            }

            
            FootballClubs.Remove(clubToDelete);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"Club with ID {id} has been deleted.")
            };
        }
    }
    }

