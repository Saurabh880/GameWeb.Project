using GameWeb.API.Entities;

namespace GameWeb.API.Repository
{
    public interface GameRepository
    {
        IEnumerable<Game> GetAll();

        Game? GetById(int id);

        void Create(Game game);

        void Update(Game game);

        void Delete(int gameId);
    }
}
