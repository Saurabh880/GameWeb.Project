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

        public async Task<GameReadDTO> GetGameDetailsAsync(int Id)
        {
            var response = await _httpClient.GetAsync($"api/v1/Game/details/{Id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GameReadDTO>(result);
        }

        public async Task<GameReadDTO> UpdateGame(int gameId, GameUpdateDTO gameUpdate)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/v1/Game/{gameId}", gameUpdate);

            if (response.IsSuccessStatusCode)
            {
                var updatedGame = await response.Content.ReadFromJsonAsync<GameReadDTO>();
                return updatedGame!;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Failed to update game {gameId}. Status: {response.StatusCode}, Details: {errorContent}");
        }

        public async Task<bool> CreateGenreAsync(Genre genre)
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/Genre", genre);
            return response.IsSuccessStatusCode;
        }
    }
}
