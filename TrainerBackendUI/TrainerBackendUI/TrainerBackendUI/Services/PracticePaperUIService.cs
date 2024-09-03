using Azure;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using QuestionBankDll.Trainer.Models;
using System.Net.Http.Json;


namespace QuestionBankUI.Trainer.Services
{
    public class PracticePaperUIService
    {
        private readonly HttpClient _httpClient;

        public PracticePaperUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private  string _pdfFolderPath;
        

        public async Task AddPracticePaperAsync( string file)
        {

            var response = await _httpClient.PostAsJsonAsync("AddPracticePaper", file);
            //response.EnsureSuccessStatusCode();
        }

        public async Task DeletePracticePaperAsync(string practicePaper)
        {
            var response = await _httpClient.DeleteAsync($"DeletePracticePaper/{practicePaper}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<byte[]> DownloadPracticePaperAsync(string paperName)
        {
            var pdf = await _httpClient.GetByteArrayAsync($"DownloadPracticePaper/{paperName}");
            
            return pdf;

        }

        public async Task<List<string>> GetAllPracticePapersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("GetPracticePapers");
        }
    }
}
