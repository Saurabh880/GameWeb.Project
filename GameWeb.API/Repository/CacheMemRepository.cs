using GameWeb.API.DbContextRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using GameManager.Core.DTO;

namespace GameWeb.API.Repository
{
    public class CacheMemRepository
    {
        private readonly GameDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromSeconds(5L);
        private const string GamesCacheKey = "Games";

        public CacheMemRepository(GameDbContext context, IMemoryCache cache)
        {
            this._context = context;
            this._cache = cache;
        }

        public async Task<List<GameReadDTO>> GetGames()
        {
            // Try to get cached games
            if (_cache.TryGetValue("Games", out List<GameReadDTO> cachedGames))
            {
                return cachedGames;
            }

            // Fetch from database with Genre included
            var gamesFromDb = await _context.Games
                .Include(g => g.Genre)
                .AsNoTracking()
                .ToListAsync();

            // Map to DTOs and order by GameName
            var gameDtos = gamesFromDb
                .Select(game => game.ToGameRead())
                .OrderBy(dto => dto.GameName)
                .ToList();

            // Cache the result
            _cache.Set("Games", gameDtos, _cacheExpiration);

            return gameDtos;

        }
        public async Task<List<GameDetailsDTO>> GetGamesv2()
        {
            // Try to get cached games
            if (_cache.TryGetValue("Gamesv2", out List<GameDetailsDTO> cachedGames))
            {
                return cachedGames;
            }

            // Fetch from database with Genre included
            var summaries = await _context.Games
                    .AsNoTracking()
                    .OrderBy(g => g.GameName)
                    .Select(g => g.ToGameDetails())
                    .ToListAsync();

            // Cache the result
            _cache.Set("Gamesv2", summaries, _cacheExpiration);

            return summaries;

        }

        public void RemoveGamesFromCache() => this._cache.Remove((object)"Games");
    }
}
