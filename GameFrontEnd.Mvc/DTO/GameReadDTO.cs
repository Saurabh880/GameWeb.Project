using GameWebAPI.Entites;
using System.ComponentModel.DataAnnotations;

namespace GameWebAPI.DTO
{
    //A DTO is a contract btw client and server
    public class GameReadDTO
    {
        public int GameId { get; set; }
        public required string GameName { get; set; }
        [Range(1, 100)]
        public double GamePrice { get; set; }
        public DateOnly ReleaseDate { get; set; }
        [Url]
        [StringLength(200)]
        public required string ImageUri { get; set; }
        [StringLength(50)]
        public string? GenreName { get; set; }

        
    }

    public static class GameExtension
    {
        public static GameReadDTO ToGameRead(this Game game)
        {
            return new GameReadDTO()
            {
                GameId = game.GameId,
                GameName = game.GameName,
                GamePrice = game.GamePrice,
                ReleaseDate = game.ReleaseDate,
                ImageUri = game.ImageUri,
                GenreName = game.Genre?.Name
            };
        }
    }
}