using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TraineeBackendDll.Data;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Services
{
    public class UpdateService
    {

        private readonly QuestionBankDbContext _context;
        private readonly string _jsonFilePath = "selectedQuestions.json";

        public UpdateService(QuestionBankDbContext context)
        {
            _context = context;
        }

        public List<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }

        public void SaveSelectedQuestions(List<int> selectedQuestionIds)
        {
            var selectedQuestions = _context.Questions.Where(q => selectedQuestionIds.Contains(q.QuestionId)).ToList();
            var json = JsonSerializer.Serialize(selectedQuestions);
            File.WriteAllText(_jsonFilePath, json);
        }

        public List<Question> LoadSelectedQuestions()
        {
            if (!File.Exists(_jsonFilePath)) return new List<Question>();
            var json = File.ReadAllText(_jsonFilePath);
            return JsonSerializer.Deserialize<List<Question>>(json);
        }
    }
}
