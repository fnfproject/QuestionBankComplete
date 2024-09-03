using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using QuestionBackDll.Trainer.Dtos;
using QuestionBankDll.Trainer.Dtos;
using QuestionBankDll.Trainer.Models;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankDll.Trainer.Services
{
    public interface ITestService
    {
        Task<Test> GenerateTestAsync(GenerateTestRequest request);
        Task<ModelTest> GetTestByHyperlinkAsync(string hyperlink);
        public  Task SaveTestResultsAsync(int testId, int userId, List<UserAnswerDto> userAnswers);

        public  Task SubmitAnswersAsync(int testId, int userId, List<UserAnswer> userAnswers);
    }

    public class TestService : ITestService
    {
        private readonly QuestionDbContext _context;

        public TestService(QuestionDbContext context)
        {
            _context = context;
        }
        public async Task SubmitAnswersAsync(int testId, int userId, List<UserAnswer> userAnswers)
        {
            foreach (var userAnswerDto in userAnswers)
            {
                var correctAnswer = await _context.Questions
                    .Where(q => q.QuestionId == userAnswerDto.QuestionId)
                    .Select(q => q.CorrectAnswer)
                    .FirstOrDefaultAsync();

                var userAnswer = new UserAnswer
                {
                    TestId = testId,
                    QuestionId = userAnswerDto.QuestionId,
                    UserId = userId,
                    SelectedOption = userAnswerDto.SelectedOption,
                    IsCorrect = userAnswerDto.SelectedOption == correctAnswer,
                    AnsweredAt = DateTime.UtcNow
                };

                _context.UserAnswers.Add(userAnswer);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<Test> GenerateTestAsync(GenerateTestRequest request)
        {
            if (request.QuestionIds == null || !request.QuestionIds.Any())
                throw new ArgumentException("No questions selected.");

            var test = new Test
            {
                TestName = request.TestName,
                TestMaxMarks = request.QuestionIds.Count,
                TestNoOfQuestions = request.QuestionIds.Count,
                CreatedBy = request.CreatedBy,
                StartTime = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddMinutes(30),
                CreatedAt = DateTime.UtcNow,
                Hyperlinks = Guid.NewGuid().ToString()
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            try
            {

                foreach (var questionId in request.QuestionIds)
                {
                    var testQuestion = new TestQuestion
                    {
                        TestId = test.TestId,
                        QuestionId = questionId
                    };
                    _context.TestQuestions.Add(testQuestion);
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }


            return test;
        }

        public async Task<ModelTest> GetTestByHyperlinkAsync(string hyperlink)
        {
            var test = await _context.Tests
                .Include(t => t.TestQuestions)
                .ThenInclude(tq => tq.Question)
                .FirstOrDefaultAsync(t => t.Hyperlinks == hyperlink);

            if (test == null)
                throw new KeyNotFoundException("Test not found.");
            var testQuestionList = test.TestQuestions.ToList();

            ShuffleQuestionsAndOptions(testQuestionList);

            var testDto = new ModelTest
            {
                TestId = test.TestId,
                TestName = test.TestName,
                TestMaxMarks = test.TestMaxMarks,
                TestNoOfQuestions = test.TestNoOfQuestions,
                Hyperlinks = test.Hyperlinks,
                ModelTestQuestions = test.TestQuestions.Select(tq => new ModelTestQuestion
                {
                    QuestionId = tq.QuestionId,
                    QuestionText = tq.Question.QuestionText,
                    OptionA = tq.Question.OptionA,
                    OptionB = tq.Question.OptionB,
                    OptionC = tq.Question.OptionC,
                    OptionD = tq.Question.OptionD
                }).ToList()
            };

            return testDto;
        }

        private void ShuffleQuestionsAndOptions(List<TestQuestion> testQuestions)
        {
            var random = new Random();

            foreach (var tq in testQuestions)
            {
                var question = tq.Question;

                // Shuffle options
                var options = new[] { question.OptionA, question.OptionB, question.OptionC, question.OptionD };
                options = options.OrderBy(o => random.Next()).ToArray();

                question.OptionA = options[0];
                question.OptionB = options[1];
                question.OptionC = options[2];
                question.OptionD = options[3];

                // Optionally shuffle the order of questions
            }
        }

        public async Task SaveTestResultsAsync(int testId, int userId, List<UserAnswerDto> userAnswers)
        {
            int totalCorrectAnswers = 0;

            foreach (var userAnswerDto in userAnswers)
            {
                var correctAnswer = await _context.Questions
                    .Where(q => q.QuestionId == userAnswerDto.QuestionId)
                    .Select(q => q.CorrectAnswer)
                    .FirstOrDefaultAsync();

                var isCorrect = userAnswerDto.SelectedOption == correctAnswer;
                if (isCorrect) totalCorrectAnswers++;

                var userAnswer = new UserAnswer
                {
                    TestId = testId,
                    QuestionId = userAnswerDto.QuestionId,
                    UserId = userId,
                    SelectedOption = userAnswerDto.SelectedOption,
                    IsCorrect = isCorrect,
                    AnsweredAt = DateTime.UtcNow
                };

                _context.UserAnswers.Add(userAnswer);
            }

            var score = CalculateScore(totalCorrectAnswers, userAnswers.Count);

            var testResult = new TestResult
            {
                TestId = testId,
                UserId = userId,
                Score = score,
                CompletedAt = DateTime.UtcNow
            };

            _context.TestResults.Add(testResult);
            await _context.SaveChangesAsync();
        }

        private decimal CalculateScore(int correctAnswers, int totalQuestions)
        {
            return (decimal)correctAnswers / totalQuestions * 100;
        }


    }
}
