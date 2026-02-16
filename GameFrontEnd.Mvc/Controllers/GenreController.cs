using GameFrontEnd.Mvc.Service;
using GameManager.Core.Domain.Entities;
using GameManager.Core.DTO;
using GameManager.Core.ServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace GameFrontEnd.Mvc.Controllers
{
    [Route("[controller]")]
    public class GenreController : Controller
    {
        private List<Genre> allGenre;
        private readonly IGameService _gameService;

        public GenreController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [Route("[action]")]
        public async Task<IActionResult> GenreList()
        {
            var genres = await _gameService.GetGenresAsync();
            if (genres == null || !genres.Any())
            {
                return NotFound("No genres found.");
            }
            return View(genres);
        }
        [Route("[action]")]
        public IActionResult CreateGenre()
        {
            return View();          
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewGenre(Genre createGenre)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return View();
            }

            if (string.IsNullOrEmpty(createGenre?.Name))
            {
                ModelState.AddModelError("GenreName", "Genre name can't be null");
                return View();
            }

            var success = await _gameService.CreateGenreAsync(createGenre);
            return success ? RedirectToAction("GenreList") : View("Error");
        }
    }
}
