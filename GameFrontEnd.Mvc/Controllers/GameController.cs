using GameFrontEnd.Mvc.Service;
using GameWeb.API.Entities;
using GameWebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GameFrontEnd.Mvc.Controllers
{
    public class GameController : Controller
    {
        private  List<Genre> allGenre;

        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var games = await _gameService.GetGamesAsync();
                return View(games);
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> CreateGame()
        {
            try
            {
                var genres = await _gameService.GetGenresAsync();
                ViewBag.Genre = genres;
                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewGame(GameCreateDTO createGame)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return View();
            }

            if (string.IsNullOrEmpty(createGame?.GameName))
            {
                ModelState.AddModelError("GameName", "Game name can't be null");
                return View();
            }

            var success = await _gameService.CreateGameAsync(createGame);
            return success ? RedirectToAction("Index") : View("Error");
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
