using Asp.Versioning;
using GameManager.Core.DTO;
using GameWeb.API.DbContextRepo;
using GameWeb.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;

namespace GameWeb.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class GameController : CustomControllerBase
    {
        private readonly GameDbContext _applicationDb;
        private ILogger<GameController> _logger;
        private readonly CacheMemRepository _cachedata;

        public GameController(
          GameDbContext dbContext,
          ILogger<GameController> logger,
          CacheMemRepository cacheMem)
        {
            _applicationDb = dbContext;
            _logger = logger;
            _cachedata = cacheMem;
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        
        public async Task<ActionResult<IEnumerable<GameReadDTO>>> GetGame()
        {
            var games = await _cachedata.GetGames();
            if (games == null || !games.Any())
            {
                return NotFound("No games found.");
            }

            return Ok(games);
        }

        [HttpGet("summary/{gameId}", Name = "GetGameById")]
        [Produces("application/xml")]
        public async Task<ActionResult<GameSummaryDTO>> GetGameById(int gameId)
        {
            if (gameId == 0)
            {
                return Problem(
                    detail: "Invalid Game Id",
                    statusCode: 400,
                    title: "Game By Id Search"
                );
            }

            var game = await _applicationDb.Games
                .AsNoTracking()
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.GameId == gameId);

            if (game == null)
            {
                return NotFound();
            }

            var gameSummaryDto = game.ToGameSummary();
            return Ok(gameSummaryDto);
        }


        [HttpGet("details/{id}", Name = "GetGameDetailsById")]
        public async Task<ActionResult<GameReadDTO>> GetGameDetailsById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid Game Id");
            }

            var game = await _applicationDb.Games
                .AsNoTracking()
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.GameId == id);

            if (game == null)
            {
                return NotFound();
            }

            var gameReadDto = game.ToGameRead();
            return Ok(gameReadDto);
        }


        [HttpPost]
        public async Task<ActionResult<GameReadDTO>> AddGame(GameCreateDTO addGame)
        {
            if (addGame == null)
            {
                return BadRequest("Game data cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Validate GenreId
            var genre = await _applicationDb.Genres.FindAsync(addGame.GenreId);
            if (genre == null)
            {
                return BadRequest("Invalid genre id.");
            }
            //Model validations
            ValidationHelper.ModelValidation(addGame);

            var newGame = addGame.ToGame();
            _applicationDb.Games.Add(newGame);
            await _applicationDb.SaveChangesAsync();

            var readDto = newGame.ToGameRead();

            // Clear cache so fresh data will be retrieved next time
            _cachedata.RemoveGamesFromCache();

            return CreatedAtRoute(
                routeName: "GetGameDetailsById",
                routeValues: new { id = readDto.GameId },
                value: readDto
            );
        }


        [HttpPut("{gameId}")]
        [ProducesResponseType(typeof(GameReadDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<GameReadDTO>> UpdateGame(int gameId, GameUpdateDTO dto)
        {
            if (dto == null || gameId != dto.GameId)
            {
                return BadRequest("Invalid game data or mismatched ID.");
            }

            var genre = await _applicationDb.Genres.FindAsync(dto.GenreId);
            if (genre == null)
            {
                return BadRequest("Invalid genre id.");
            }

            var existing = await _applicationDb.Games.FindAsync(gameId);
            if (existing == null)
            {
                return NotFound();
            }
            //validation
            ValidationHelper.ModelValidation(existing);

            // Update properties
            existing.GameName = dto.GameName;
            existing.GenreId = dto.GenreId;
            existing.GamePrice = dto.GamePrice;
            existing.ReleaseDate = dto.ReleaseDate;
            existing.ImageUri = dto.ImageUri;

            await _applicationDb.SaveChangesAsync();

            // Clear cache so fresh data will be retrieved next time
            _cachedata.RemoveGamesFromCache();

            var updatedDto = existing.ToGameRead();
            return Ok(updatedDto);

        }


        [HttpPut("updateGameCost/{gameId}")]
        public async Task<IActionResult> PutGame(int gameId, [Bind(new string[] { "GameId", "GameName" })] GameUpdateDTO gameUpdate)
        {
            if (gameId == 0)
            {
                return NotFound("Invalid game id.");
            }

            var existingGame = await _applicationDb.Games.FindAsync(gameId);
            if (existingGame == null)
            {
                return NotFound();
            }

            // Update only the fields you want to allow
            existingGame.GamePrice = gameUpdate.GamePrice;

            try
            {
                await _applicationDb.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(gameId))
                {
                    _logger.LogError("Error occurred while updating price: Id doesn't exist.");
                    return NotFound();
                }
                throw; // rethrow so higher middleware can handle
            }

            return NoContent();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGame(int gameId)
        {
            if (_applicationDb.Games == null)
            {
                return NotFound("Games collection not available.");
            }

            if (gameId == 0)
            {
                return BadRequest("Invalid game id.");
            }

            var game = await _applicationDb.Games.FindAsync(gameId);
            if (game == null)
            {
                return NotFound();
            }

            _applicationDb.Games.Remove(game);
            await _applicationDb.SaveChangesAsync();

            // Clear cache so fresh data will be retrieved next time
            _cachedata.RemoveGamesFromCache();

            return NoContent();

        }
        [ApiExplorerSettings(IgnoreApi = true)]

        [NonAction]
        public bool GameExists(int gameId)
        {
            return _applicationDb.Games.Any(g => g.GameId == gameId);
        }



    }
}
