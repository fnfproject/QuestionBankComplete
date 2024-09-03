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
    public class TestService : ITestService
    {
        private readonly QuestionBankDbContext _context;

        public TestService(QuestionBankDbContext context)
        {
            _context = context;
        }

        private static Dictionary<int, List<int>> TestQuestionsMapping = new Dictionary<int, List<int>>();

        public async Task<List<Question>> GetQuestionsByTestIdAsync(int testId)
        {
            if (TestQuestionsMapping.ContainsKey(testId))
            {
                List<int> questionIds = TestQuestionsMapping[testId];

                // Use the injected _context to fetch questions
                var questions = await _context.Questions
                    .Where(q => questionIds.Contains(q.QuestionId))
                    .ToListAsync();

                return questions;
            }

            return new List<Question>();
        }

        //public async Task<Test> GetTestAsync(int testId)
        //{
        //    // Use the injected _context to fetch the test
        //    var test = await _context.Tests.FindAsync(testId);

        //    if (test != null)
        //    {
        //        // Fetch questions separately if needed, without assigning to a non-existent property
        //        var questions = await GetQuestionsByTestIdAsync(testId);

        //        // Handle questions independently or return them along with the test if needed
        //        // Example: return a tuple or a custom DTO with both Test and Questions
        //    }

        //    return test;


        //}

        void ITestService.AddQuestionsToTest(int testId, List<int> questionIds)
        {
            if (TestQuestionsMapping.ContainsKey(testId))
            {
                TestQuestionsMapping[testId].AddRange(questionIds);
            }
            else
            {
                TestQuestionsMapping[testId] = new List<int>(questionIds);
            }

        }

        //public async Task<TestWithQuestionsDto> GetTestWithQuestionsAsync(int testId)
        //{
        //    var test = await _context.Tests.FindAsync(testId);

        //    if (test != null)
        //    {
        //        var questions = await GetQuestionsByTestIdAsync(testId);

        //        return new TestWithQuestionsDto
        //        {
        //            Test = test,
        //            Questions = questions
        //        };
        //    }

        //    return null; // Or handle it as needed
        //}

        public Task<bool> SubmitTestAnswerAsync(TestSubmissionDto submissionDto)
        {
            throw new NotImplementedException();
        }

        public Task<TestWithQuestionsDto> GetTestWithQuestionsAsync(int testId)
        {
            throw new NotImplementedException();
        }

        Task<Models.Test> ITestService.GetTestAsync(int testId)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> SubmitTestAnswersAsync(TestSubmissionDto submission)
        //{
        //    // Example: Validate and save answers to the database
        //    try
        //    {
        //        // Validate submission (ensure all questions are answered)
        //        var questions = await _context.Questions
        //                                      .Where(q => q.TestId == submission.TestId)
        //                                      .ToListAsync();

        //        if (questions.Count != submission.SelectedAnswers.Count)
        //        {
        //            return false; // Not all questions are answered
        //        }

        //        // Save each answer to the database
        //        foreach (var answer in submission.SelectedAnswers)
        //        {
        //            var testAnswer = new TestAnswer
        //            {
        //                TestId = submission.TestId,
        //                QuestionId = answer.Key,
        //                SelectedOptionId = answer.Value
        //            };
        //            _context.testAnswers.Add(testAnswer);
        //        }

        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false; // Handle exceptions properly in a real application
        //    }
        //}
    }

    //public Task<Test> AddTestAsync(TestDto testDto)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<Test> GetTestAsync(int testId)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<List<Question>> GetQuestionsByTestIdAsync(int testId)
    //{
    //    // Fetch questions for the given TestId
    //    return await _context.Questions
    //        .Where(q => q.TestId == testId)
    //        .ToListAsync();
    //}


    // return await _context.Tests.FindAsync(testId);
    //public async Task<Test> GetTestAsync(int testId)
    //    {
    //        return await _context.Tests
    //                             .Include(t => t.Questions) // If Test has a collection of Questions
    //                             .FirstOrDefaultAsync(t => t.TestId == testId);
    //    }


    //public async Task<ResultDto> SubmitTestAsync(int testId, Dictionary<int, string> selectedAnswers, int userId)
    //{
    //    // Fetch questions for the given TestId
    //    var questions = await _context.Questions
    //        .Where(q => q.TestId == testId)
    //        .ToListAsync();

    //    if (questions == null || !questions.Any())
    //    {
    //        return null; // No questions found
    //    }

    //    int correctAnswersCount = 0;
    //    int wrongAnswersCount = 0;

    //    // Evaluate answers
    //    foreach (var question in questions)
    //    {
    //        if (selectedAnswers.TryGetValue(question.QuestionId, out string selectedOption))
    //        {
    //            // Check if the selected option matches the correct answer
    //            if (selectedOption.Equals(question.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
    //            {
    //                correctAnswersCount++;
    //            }
    //            else
    //            {
    //                wrongAnswersCount++;
    //            }
    //        }
    //    }

    //    // Calculate score and percentage
    //    int score = correctAnswersCount; // Score can be the count of correct answers
    //    double percentage = ((double)correctAnswersCount / questions.Count) * 100;

    //    // Create the ResultDto
    //    var resultDto = new ResultDto
    //    {
    //        TestId = testId,
    //        UserId = userId,
    //        Score = score,
    //        NoRightAnswers = correctAnswersCount,
    //        NoOfWrongAnswers = wrongAnswersCount,
    //        Percentage = percentage
    //    };

    //    // Update results in the database
    //    var testResult = new Result
    //    {
    //        TestId = testId,
    //        UserId = userId,
    //        Score = score,
    //        NoRightAnswers = correctAnswersCount,
    //        NoOfWrongAnswers = wrongAnswersCount,
    //        Percentage = percentage,
    //        CreatedAt = DateTime.UtcNow
    //    };

    //    _context.Results.Add(testResult);
    //    await _context.SaveChangesAsync();

    //    return resultDto;
    //}


}
