using Newtonsoft.Json;
using System.Net.Http.Headers;
using GameManager.Core.ServiceContract;
using GameManager.Core.DTO;
using GameManager.Core.Domain.Entities;

namespace GameFrontEnd.Mvc.Service
{
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient;

        public GameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<GameReadDTO>> GetGamesAsync()
        {
            var response = await _httpClient.GetAsync("api/v1/Game");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GameReadDTO>>(result);
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            var response = await _httpClient.GetAsync("api/v1/Genre");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Genre>>(result);
        }

        public async Task<bool> CreateGameAsync(GameCreateDTO createGame)
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/Game", createGame);
            return response.IsSuccessStatusCode;
        }

    }
}
