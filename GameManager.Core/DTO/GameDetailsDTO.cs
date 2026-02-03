using GameManager.Core.Domain.Entities;

namespace GameManager.Core.DTO
{
    public class GameDetailsDTO
    {
        public string GameName { get; set; }
        public double GamePrice { get; set; }
        public string ImageUri { get; set; }

    }
    public static class GameSummary
    {
        public static GameDetailsDTO ToGameDetails(this Game game)
        {
            return new GameDetailsDTO()
            {
                GameName = game.GameName,
                GamePrice = game.GamePrice,
                ImageUri = game.ImageUri
            };
        }
    }
}
