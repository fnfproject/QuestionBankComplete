using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankDll.Trainer.Dtos
{
    public class QuestionDtos
    {
        public int id { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }
        public string DifficultyLevel { get; set; }
        public string QuestionText { get; set; }
        public IFormFile QuestionTextFile { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public IFormFile OptionAFile { get; set; }
        public IFormFile OptionBFile { get; set; }
        public IFormFile OptionCFile { get; set; }
        public IFormFile OptionDFile { get; set; }
        public string CorrectAnswer { get; set; }
        public IFormFile CorrectAnswerFile { get; set; }
        public string Explanation { get; set; }
        public IFormFile ExplanationFile { get; set; }
    }
}
