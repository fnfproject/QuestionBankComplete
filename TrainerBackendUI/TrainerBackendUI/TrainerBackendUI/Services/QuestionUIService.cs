using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionBankDll.Trainer.Models;
using System.Net.Http.Json;

namespace QuestionBankUI.Trainer.Services
{
    
    public class QuestionUIService
    {
        private readonly HttpClient _httpClient;
        public QuestionUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task AddQuestionAsync(Question question)
        {
            try
            {
                _httpClient.PostAsJsonAsync<Question>("AddSingleQuestion", question);

            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
            //response.EnsureSuccessStatusCode();
        }

        //public async Task AddQuestionInBulkAsync(IFormFile file)
        //{
        //    var content = new MultipartFormDataContent();
        //    content.Add(new StreamContent(file.OpenReadStream()));
        //     _httpClient.PostAsync("AddBulkQuestions", content);
        //    //response.EnsureSuccessStatusCode();
        //}

        public async Task DeleteQuestionAsync(int questionId)
        {
            var response =  await _httpClient.DeleteAsync($"DeleteQuestion/{questionId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Question>> FindQuestionByTextAsync(string text)
        {
            return await _httpClient.GetFromJsonAsync<List<Question>>($"GetByName/{text}");
        }

        public async Task<byte[]> GeneratePdfAsync(List<int> questionIds)
        {
            var response = await _httpClient.PostAsJsonAsync("generate-pdf", questionIds);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Question>>("GetAllQuestions");
        }
        public async Task<Question> GetQuestionById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Question>($"Find/{id}");
        }

        public Task UpdateQuestionAsync(Question question)
        {
            try
            {
                //question.CreatedBy = 1;
                var response = _httpClient.PutAsJsonAsync($"Update/{question.QuestionId}", question);
                return response;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


    }
}
