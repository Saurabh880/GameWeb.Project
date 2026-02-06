using GameManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameWeb.API.DbContextRepo
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
          : base((DbContextOptions)options)
        {
        }

        public GameDbContext()
        {
        }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Genre>().HasData(new object[10]
            {
                (object) new{ GenreId = 1, Name = "Fighting" },
                (object) new{ GenreId = 2, Name = "Roleplaying" },
                (object) new{ GenreId = 3, Name = "Sports" },
                (object) new{ GenreId = 4, Name = "Racing" },
                (object) new{ GenreId = 5, Name = "Kids and Family" },
                (object) new{ GenreId = 6, Name = "Action RPG" },
                (object) new{ GenreId = 7, Name = "Action Adventure" },
                (object) new{ GenreId = 8, Name = "Multiplayer" },
                (object) new{ GenreId = 9, Name = "Open World" },
                (object) new{ GenreId = 10, Name = "FPS" }
            });
            modelBuilder.Entity<Game>().HasData(new Game()
            {
                GameId = 1,
                GameName = "Elden Ring",
                GenreId = 6,
                GamePrice = 3999.0,
                ReleaseDate = new DateOnly(2022, 2, 25),
                ImageUri = "https://example.com/images/elden-ring.jpg"
            }, new Game()
            {
                GameId = 2,
                GameName = "Minecraft",
                GenreId = 5,
                GamePrice = 1999.0,
                ReleaseDate = new DateOnly(2011, 11, 18),
                ImageUri = "https://example.com/images/minecraft.jpg"
            }, new Game()
            {
                GameId = 3,
                GameName = "GTA V",
                GenreId = 7,
                GamePrice = 2999.0,
                ReleaseDate = new DateOnly(2013, 9, 17),
                ImageUri = "https://example.com/images/gta-v.jpg"
            }, new Game()
            {
                GameId = 4,
                GameName = "Red Dead Redemption 2",
                GenreId = 9,
                GamePrice = 3499.0,
                ReleaseDate = new DateOnly(2018, 10, 26),
                ImageUri = "https://example.com/images/rdr2.jpg"
            }, new Game()
            {
                GameId = 5,
                GameName = "God of War Ragnarök",
                GenreId = 7,
                GamePrice = 4999.0,
                ReleaseDate = new DateOnly(2022, 11, 9),
                ImageUri = "https://example.com/images/gow-ragnarok.jpg"
            }, new Game()
            {
                GameId = 6,
                GameName = "The Witcher 3",
                GenreId = 6,
                GamePrice = 2499.0,
                ReleaseDate = new DateOnly(2015, 5, 19),
                ImageUri = "https://example.com/images/witcher-3.jpg"
            }, new Game()
            {
                GameId = 7,
                GameName = "Cyberpunk 2077",
                GenreId = 6,
                GamePrice = 2999.0,
                ReleaseDate = new DateOnly(2020, 12, 10),
                ImageUri = "https://example.com/images/cyberpunk.jpg"
            }, new Game()
            {
                GameId = 8,
                GameName = "Forza Horizon 5",
                GenreId = 4,
                GamePrice = 3499.0,
                ReleaseDate = new DateOnly(2021, 11, 9),
                ImageUri = "https://example.com/images/forza-horizon-5.jpg"
            }, new Game()
            {
                GameId = 9,
                GameName = "Call of Duty Modern Warfare",
                GenreId = 10,
                GamePrice = 3999.0,
                ReleaseDate = new DateOnly(2019, 10, 25),
                ImageUri = "https://example.com/images/cod-mw.jpg"
            }, new Game()
            {
                GameId = 10,
                GameName = "FIFA 23",
                GenreId = 3,
                GamePrice = 4599.0,
                ReleaseDate = new DateOnly(2022, 9, 30),
                ImageUri = "https://example.com/images/fifa-23.jpg"
            }, new Game()
            {
                GameId = 11,
                GameName = "Assassin’s Creed Valhalla",
                GenreId = 6,
                GamePrice = 3999.0,
                ReleaseDate = new DateOnly(2020, 11, 10),
                ImageUri = "https://example.com/images/ac-valhalla.jpg"
            }, new Game()
            {
                GameId = 12,
                GameName = "Horizon Forbidden West",
                GenreId = 6,
                GamePrice = 4999.0,
                ReleaseDate = new DateOnly(2022, 2, 18),
                ImageUri = "https://example.com/images/horizon-fw.jpg"
            }, new Game()
            {
                GameId = 13,
                GameName = "PUBG",
                GenreId = 10,
                GamePrice = 1499.0,
                ReleaseDate = new DateOnly(2017, 12, 20),
                ImageUri = "https://example.com/images/pubg.jpg"
            }, new Game()
            {
                GameId = 14,
                GameName = "Skyrim",
                GenreId = 2,
                GamePrice = 1999.0,
                ReleaseDate = new DateOnly(2011, 11, 11),
                ImageUri = "https://example.com/images/skyrim.jpg"
            }, new Game()
            {
                GameId = 15,
                GameName = "Among Us",
                GenreId = 8,
                GamePrice = 499.0,
                ReleaseDate = new DateOnly(2018, 6, 15),
                ImageUri = "https://example.com/images/among-us.jpg"
            });
        }
    }
}