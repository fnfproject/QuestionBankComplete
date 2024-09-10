using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraineeBackendDll.Data;
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Interface;

namespace TraineeBackendDll.Service
{
    public class TestService : ITestService
    {
        private readonly QuestionBankDbContext _context;

        public TestService(QuestionBankDbContext context)
        {
            _context = context;
        }

        public async Task<TestDetailsDto> GetTestDetailsAsync(int testId)
        {
            var testDetails = await _context.Tests
                .FromSqlRaw("SELECT * FROM Test WHERE TestId = {0}", testId)
                .Select(t => new TestDetailsDto
                {
                    TestId = t.TestId,
                    TestName = t.TestName,
                    ModelTestQuestions = _context.Questions
                        .FromSqlRaw("SELECT * FROM Question WHERE TestId = {0}", testId)
                        .Select(q => new QuestionDto
                        {
                            QuestionId = q.QuestionId,
                            QuestionText = q.QuestionText,
                            OptionA = q.OptionA,
                            OptionB = q.OptionB,
                            OptionC = q.OptionC,
                            OptionD = q.OptionD,
                            CorrectAnswer = q.CorrectAnswer,
                            Explaination = q.Explaination
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (testDetails == null)
            {
                throw new Exception("Test not found");
            }

            return testDetails;
        }


    }
}
