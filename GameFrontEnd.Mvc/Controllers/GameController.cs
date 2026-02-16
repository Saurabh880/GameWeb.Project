
using GameManager.Core.Domain.Entities;
using GameManager.Core.DTO;
using GameManager.Core.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GameFrontEnd.Mvc.Controllers
{
    [Route("[controller]")]
    public class GameController : Controller
    {
        private  List<Genre> allGenre;

        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [AllowAnonymous]
        [Route("[action]")]
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
        [Route("[action]")]
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
        
        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        
        [HttpGet("Details/{GameId}")]
        public async Task<ActionResult<GameReadDTO>> Details(int GameId)
        {
            try
            {
                var game = await _gameService.GetGameDetailsAsync(GameId);
                if (game == null) return RedirectToAction("Index");

                var genres = await _gameService.GetGenresAsync();
                ViewBag.Genre = genres;

                return View(game); // model passed to view

            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        [HttpGet("Edit/{GameId}")]
        public async Task<ActionResult<GameReadDTO>> Edit(int GameId)
        {
            try
            {
                var (game, genres) = await LoadGameAndGenres(GameId);
                if (game == null) return RedirectToAction("Index");

                ViewBag.Genre = genres;
                return View("Edit", game);
            }
            catch (Exception ex)
            {               
                return View("Error");
            }

        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateGame(int GameId, GameUpdateDTO updateGame)
        {
            if (!ModelState.IsValid)
            {
                // Collect validation errors and pass them to the view
                ViewBag.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return View(updateGame); // return the same view with errors
            }

            if (string.IsNullOrEmpty(updateGame?.GameName))
            {
                ModelState.AddModelError("GameName", "Game name can't be null");
                return View(updateGame);
            }

            // Call your service layer
            var updatedGame = await _gameService.UpdateGame(GameId, updateGame);

            if (updatedGame != null)
            {
                return RedirectToAction("Index"); // redirect to list page
            }

            return View("Error"); // show error view if update fails
        }
        public IActionResult Error()
        {
            return View();
        }
        private async Task<(GameReadDTO game, List<Genre> genres)> LoadGameAndGenres(int id)
        {
            var game = await _gameService.GetGameDetailsAsync(id);
            var genres = await _gameService.GetGenresAsync();
            return (game, genres);
        }

    }
}
