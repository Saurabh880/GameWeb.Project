using GameWebAPI.DTO;
using GameWebAPI.Entites;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GameFrontEnd.Mvc.Controllers
{
    public class GameController : Controller
    {
        private string baseUrl = "http://localhost:5140/";
        private  List<Genre> allGenre;

        public async Task<IActionResult> Index()
        {
            List<GameReadDTO> game = new List<GameReadDTO>();

            //API call
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseUrl+"api/v1/Game");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (
                        new MediaTypeWithQualityHeaderValue("application/json")
                    );

                //Get all games
                HttpResponseMessage getData = await _httpclient.GetAsync("");
                if(getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    game = JsonConvert.DeserializeObject<List<GameReadDTO>>(result);
                }
                else
                {
                    return View("Error");
                }

            }
            return View(game);
        }
        public async Task<IActionResult> CreateGame()
        {
            allGenre = new List<Genre>();
            using (var _httpclient = new HttpClient())
            {
                
                _httpclient.BaseAddress = new Uri(baseUrl + "api/v1/Genre");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (
                        new MediaTypeWithQualityHeaderValue("applicatiion/json")
                    );

                //Get all games
                HttpResponseMessage getData = await _httpclient.GetAsync("");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    allGenre = JsonConvert.DeserializeObject<List<Genre>>(result);
                    ViewBag.Genre = allGenre;
                }
                else
                {
                    return View("Error");
                }
                return View();
            }
        }
        public async Task<IActionResult> CreateNewGame(GameCreateDTO createGame)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
            if(createGame == null) throw new ArgumentNullException(nameof(createGame));

            //Validate the Person Name
            if (string.IsNullOrEmpty(createGame.GameName))
            {
                throw new ArgumentException("Game name can't be null");
            }
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseUrl + "api/v1/Game");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (
                        new MediaTypeWithQualityHeaderValue("applicatiion/json")
                    );

                //Get all games
                HttpResponseMessage postData = await _httpclient.PostAsJsonAsync("", createGame);
                if (postData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }

            }

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
