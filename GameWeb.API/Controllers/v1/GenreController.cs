using Asp.Versioning;
using GameManager.Core.Domain.Entities;
using GameWeb.API.DbContextRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace GameWeb.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class GenreController : CustomControllerBase
    {
        private readonly ApplicationDbContext _applicationDb;
        private ILogger<GameController> _logger;

        public GenreController(
          ApplicationDbContext dbContext,
          ILogger<GameController> logger)
        {
            _applicationDb = dbContext;
            _logger = logger;
            
        }
        [HttpGet]

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
    }
}
