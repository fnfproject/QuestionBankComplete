using QuestionBankDll.Trainer.Dtos;
using System.Net.Http.Json;

namespace QuestionBankUI.Trainer.Services
{
    public class ResultUIService
    {
        private readonly HttpClient _httpClient;

        public ResultUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TestTraineeResultAPIDto>> GetResultByTestNameAsync(string testName)
        {
            return await _httpClient.GetFromJsonAsync<List<TestTraineeResultAPIDto>>($"ResultsByTestName/{testName}");
        }

        public async Task<List<TraineeTestResultDto>> GetResultByTraineeNameAsync(string traineeName)
        {
            var results= await _httpClient.GetFromJsonAsync<List<TraineeTestResultDto>>($"ResultsByTraineeName/{traineeName}");
            return results;
        }

        public async Task<List<TestDtos>> GetAllTestsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TestDtos>>("GetAllTests");
        }
    }
}

