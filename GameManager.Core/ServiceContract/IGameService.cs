using GameManager.Core.Domain.Entities;
using GameManager.Core.DTO;


namespace GameManager.Core.ServiceContract
{
    public interface IGameService
    {
        Task<List<GameReadDTO>> GetGamesAsync();
        Task<List<Genre>> GetGenresAsync();
        Task<bool> CreateGameAsync(GameCreateDTO createGame);

        Task<GameReadDTO> GetGameDetailsAsync(int Id);
        Task<GameReadDTO> UpdateGame(int gameId,GameUpdateDTO updateGame);
        Task<bool> CreateGenreAsync(Genre genre);
    }

}
