using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Services
{
    public class TestServices
    {
        private readonly HttpClient _httpClient;

        public TestServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task StoreHyperlink(Test test)
        {
            await _httpClient.PostAsJsonAsync("api/test/store-hyperlink", test);
        }
    }

    public class Test
    {
        public string TestName { get; set; }
        public string Hyperlinks { get; set; }
        public int TestMaxMarks { get; set; }
        public int TestNoOfQuestions { get; set; }
        public int CreatedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryTime { get; set; }
    }


}