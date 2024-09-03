using System.Net.Http.Json;
using ShuffleTestBlazor.Dtos;

namespace ShuffleTestBlazor.Services
{
    public class ShuffleService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5109/api/Shuffle";

        public ShuffleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ShuffledQuestionDto>> GetShuffledQuestionsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ShuffledQuestionDto>>($"{BaseUrl}/generate-shuffled-questions");
            return response ?? new List<ShuffledQuestionDto>();
        }
    }
}
