using System.ComponentModel.DataAnnotations;

namespace GameManager.Core.Domain.Entities
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [StringLength(50)]
        public required string GameName { get; set; }

        public required int GenreId { get; set; }

        [Range(1, 100)]
        public double GamePrice { get; set; }

        public DateOnly ReleaseDate { get; set; }

        [Url]
        [StringLength(200)]
        public required string ImageUri { get; set; }

        public Genre? Genre { get; set; }
    }
}
