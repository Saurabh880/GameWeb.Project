using GameWebAPI.DTO;
using GameWebAPI.Entites;

namespace GameFrontEnd.Mvc.Service
{
    public interface IGameService
    {
        Task<List<GameReadDTO>> GetGamesAsync();
        Task<List<Genre>> GetGenresAsync();
        Task<bool> CreateGameAsync(GameCreateDTO createGame);
    }

}
