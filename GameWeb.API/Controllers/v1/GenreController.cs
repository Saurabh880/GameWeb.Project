using Asp.Versioning;
using GameManager.Core.Domain.Entities;
using GameWeb.API.DbContextRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;

namespace GameWeb.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class GenreController : CustomControllerBase
    {
        private readonly GameDbContext _applicationDb;
        private ILogger<GameController> _logger;

        public GenreController(
          GameDbContext dbContext,
          ILogger<GameController> logger)
        {
            _applicationDb = dbContext;
            _logger = logger;
            
        }
        [HttpGet(Name = "GetGenre")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenre()
        {
            var games = await _applicationDb.Genres
                .AsNoTracking()
                .ToListAsync();
            if (games == null || !games.Any())
            {
                return NotFound("No Genre found.");
            }

            return Ok(games);
        }
        [HttpPost]
        public async Task<ActionResult<Genre>> AddGenre(Genre genre)
        {
            if(genre == null)
            {
                return BadRequest("Genre data cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ValidationHelper.ModelValidation(genre);

            _applicationDb.Genres.Add(genre);
            await _applicationDb.SaveChangesAsync();
            return CreatedAtRoute(
                routeName: "GetGenre",
                routeValues: new { id = genre.GenreId},
                value: genre
            );
        }
    }
}
