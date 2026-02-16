using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameWeb.API.Migrations
{
    /// <inheritdoc />
    public partial class ImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    GamePrice = table.Column<double>(type: "float", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Name" },
                values: new object[,]
                {
                    { 1, "Fighting" },
                    { 2, "Roleplaying" },
                    { 3, "Sports" },
                    { 4, "Racing" },
                    { 5, "Kids and Family" },
                    { 6, "Action RPG" },
                    { 7, "Action Adventure" },
                    { 8, "Multiplayer" },
                    { 9, "Open World" },
                    { 10, "FPS" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "GameName", "GamePrice", "GenreId", "ImageContentType", "ImageData", "ImageUri", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, "Elden Ring", 3999.0, 6, null, null, "https://example.com/images/elden-ring.jpg", new DateOnly(2022, 2, 25) },
                    { 2, "Minecraft", 1999.0, 5, null, null, "https://example.com/images/minecraft.jpg", new DateOnly(2011, 11, 18) },
                    { 3, "GTA V", 2999.0, 7, null, null, "https://example.com/images/gta-v.jpg", new DateOnly(2013, 9, 17) },
                    { 4, "Red Dead Redemption 2", 3499.0, 9, null, null, "https://example.com/images/rdr2.jpg", new DateOnly(2018, 10, 26) },
                    { 5, "God of War Ragnarök", 4999.0, 7, null, null, "https://example.com/images/gow-ragnarok.jpg", new DateOnly(2022, 11, 9) },
                    { 6, "The Witcher 3", 2499.0, 6, null, null, "https://example.com/images/witcher-3.jpg", new DateOnly(2015, 5, 19) },
                    { 7, "Cyberpunk 2077", 2999.0, 6, null, null, "https://example.com/images/cyberpunk.jpg", new DateOnly(2020, 12, 10) },
                    { 8, "Forza Horizon 5", 3499.0, 4, null, null, "https://example.com/images/forza-horizon-5.jpg", new DateOnly(2021, 11, 9) },
                    { 9, "Call of Duty Modern Warfare", 3999.0, 10, null, null, "https://example.com/images/cod-mw.jpg", new DateOnly(2019, 10, 25) },
                    { 10, "FIFA 23", 4599.0, 3, null, null, "https://example.com/images/fifa-23.jpg", new DateOnly(2022, 9, 30) },
                    { 11, "Assassin’s Creed Valhalla", 3999.0, 6, null, null, "https://example.com/images/ac-valhalla.jpg", new DateOnly(2020, 11, 10) },
                    { 12, "Horizon Forbidden West", 4999.0, 6, null, null, "https://example.com/images/horizon-fw.jpg", new DateOnly(2022, 2, 18) },
                    { 13, "PUBG", 1499.0, 10, null, null, "https://example.com/images/pubg.jpg", new DateOnly(2017, 12, 20) },
                    { 14, "Skyrim", 1999.0, 2, null, null, "https://example.com/images/skyrim.jpg", new DateOnly(2011, 11, 11) },
                    { 15, "Among Us", 499.0, 8, null, null, "https://example.com/images/among-us.jpg", new DateOnly(2018, 6, 15) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GenreId",
                table: "Games",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
