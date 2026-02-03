using GameWebAPI.Entites;
using System.ComponentModel.DataAnnotations;

namespace GameWebAPI.DTO
{
    public class GameCreateDTO
    {

        [StringLength(50)]
        public required string GameName { get; set; }
        public required int GenreId { get; set; }
        [Range(1, 100)]
        public double GamePrice { get; set; }
        public DateOnly ReleaseDate { get; set; }
        [Url]
        [StringLength(200)]
        public required string ImageUri { get; set; }

        public Game ToGame()
        {
            return new Game
            {
                GameName =GameName,
                GenreId =GenreId,
                GamePrice = GamePrice,
                ReleaseDate = ReleaseDate,
                // Game.ImageUri is required on the entity; ensure non-null value
                ImageUri = ImageUri ?? string.Empty
            };
        }

    }
}
