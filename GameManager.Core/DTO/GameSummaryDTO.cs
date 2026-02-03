using GameManager.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GameManager.Core.DTO
{
    //A DTO is a contract btw client and server
    public class GameSummaryDTO
    {
        public int GameId { get; set; }
        public required string GameName { get; set; }
        public required int GenreId { get; set; }
        [Range(1, 100)]
        public double GamePrice { get; set; }
        public DateOnly ReleaseDate { get; set; }
        [Url]
        [StringLength(200)]
        public required string ImageUri { get; set; }
        [StringLength(50)]
        public string? GenreName { get; set; }
        // ✅ Convert summary DTO directly into an update DTO
        public GameUpdateDTO ToUpdateDTO()
        {
            return new GameUpdateDTO
            {
                GameId = GameId,
                GameName = GameName,
                GenreId = GenreId,       // already available
                GamePrice = GamePrice,
                ReleaseDate = ReleaseDate,
                ImageUri = ImageUri
            };
        }

    }

    public static class GameSuummaryExtension
    {
        public static GameSummaryDTO ToGameSummary(this Game game)
        {
            return new GameSummaryDTO()
            {
                GameId = game.GameId,
                GenreId = game.GenreId,
                GameName = game.GameName,
                GamePrice = game.GamePrice,
                ReleaseDate = game.ReleaseDate,
                ImageUri = game.ImageUri,
                GenreName = game.Genre?.Name
            };
        }
    }
}