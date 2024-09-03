using System.Net.Http.Json;
using BlazorTrainee.Model;

namespace BlazorTrainee.Services
{
    public class TestService
    {
        private readonly HttpClient _httpClient;

        public TestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task StoreHyperlink(Test test)
        {
            await _httpClient.PostAsJsonAsync("http://localhost:18360/api/test/store-hyperlink", test);
        }
    }



}


