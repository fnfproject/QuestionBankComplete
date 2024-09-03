using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeBackendDll.Data;
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Interfaces;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly QuestionBankDbContext _context;

        public QuestionService(QuestionBankDbContext context)
        {
            _context = context;
        }

        public async Task<Question> AddQuestionAsync(QuestionDto questionDto)
        {
            var question = new Question
            {
                Subject = questionDto.Subject,
                Topic = questionDto.Topic,
                DifficultyLevel = questionDto.DifficultyLevel,
                QuestionText = questionDto.QuestionText,
                OptionA = questionDto.OptionA,
                OptionB = questionDto.OptionB,
                OptionC = questionDto.OptionC,
                OptionD = questionDto.OptionD,
                CorrectAnswer = questionDto.CorrectAnswer,
                Explaination = questionDto.Explaination,
                CreatedBy = questionDto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return question;
        }

       

       

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }
    }
}
