using Asp.Versioning;
using GameWeb.API.DbContextRepo;
using GameWeb.API.DTO;
using GameWeb.API.Repository;
using GameWebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace GameWeb.API.Controllers.v2
{
    [ApiVersion("2.0")]
    public class GameController : CustomControllerBase
    {
        private readonly ApplicationDbContext _applicationDb;
        private ILogger<GameController> _logger;
        private readonly CacheMemRepository _cachedata;
        public GameController(
          ApplicationDbContext dbContext,
          ILogger<GameController> logger,
          CacheMemRepository cacheMem)
        {
            this._applicationDb = dbContext;
            this._logger = logger;
            this._cachedata = cacheMem;
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        
        public async Task<ActionResult<IEnumerable<GameDetailsDTO>>> GetGame()
        {
            var games = await _cachedata.GetGamesv2();
            if (games == null || !games.Any())
            {
                return NotFound("No games found.");
            }

            return Ok(games);
        }
    }
}
