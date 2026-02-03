using GameWebAPI.Entites;
using System.ComponentModel.DataAnnotations;

namespace GameWebAPI.DTO
{
    public class GameUpdateDTO
    {
        [Required]
        public int GameId { get; set; }

        [StringLength(50)]
        public required string GameName { get; set; }

        // Use GenreId directly instead of GenreName
        [Required]
        public int GenreId { get; set; }

        [Range(1, 100)]
        public double GamePrice { get; set; }

        public DateOnly ReleaseDate { get; set; }

        [Url]
        [StringLength(200)]
        public required string ImageUri { get; set; }

        // Now ToGame doesn’t need a parameter
        public Game ToGame()
        {
            return new Game
            {
                GameId = GameId,
                GameName = GameName,
                GenreId = GenreId,
                GamePrice = GamePrice,
                ReleaseDate = ReleaseDate,
                ImageUri = ImageUri ?? string.Empty
            };
        }
    }
}
