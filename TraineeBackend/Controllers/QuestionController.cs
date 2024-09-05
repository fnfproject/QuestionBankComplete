using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeBackendDll.Data;
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Models;


namespace TraineeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]


    public class QuestionController : ControllerBase
    {
        private readonly QuestionBankDbContext _context;

        public QuestionController(QuestionBankDbContext context)
        {
            _context = context;
        }


        [HttpGet("GetAllTestsForTrainees")]
        public async Task<IActionResult> GetAllTestsForTrainees()
        {
            var currentDateTime = DateTime.Now;
            var tests = await _context.Tests.ToListAsync();

            if (tests == null || !tests.Any())
            {
                return NotFound("No active tests available.");
            }

            return Ok(tests);
        }

        [HttpPost("SubmitTest")]
        public IActionResult SubmitTest([FromBody] TestSubmissionDto testSubmission)
        {
            // Fetch test details and questions from the database
            var questions = _context.Questions
                                    .Where(q => testSubmission.SelectedAnswers
                                    .Select(sa => sa.QuestionId).Contains(q.QuestionId))
                                    .ToList();

            // Initialize counters for correct and wrong answers
            int rightAnswers = 0;
            int wrongAnswers = 0;
            decimal score = 0;

            List<QuestionResultDto> questionResults = new List<QuestionResultDto>();

            foreach (var submittedAnswer in testSubmission.SelectedAnswers)
            {
                var question = questions.FirstOrDefault(q => q.QuestionId == submittedAnswer.QuestionId);
                if (question != null)
                {
                    bool isCorrect = question.CorrectAnswer == submittedAnswer.SelectedAnswer;
                    if (isCorrect)
                    {
                        rightAnswers++;
                        // You can assign some scoring logic here
                        score += 1; // Example: 1 point per correct answer
                    }
                    else
                    {
                        wrongAnswers++;
                    }

                    questionResults.Add(new QuestionResultDto
                    {
                        QuestionId = question.QuestionId,
                        SelectedAnswer = submittedAnswer.SelectedAnswer,
                        CorrectAnswer = question.CorrectAnswer,
                        IsCorrect = isCorrect,
                        Explanation = question.Explaination
                    });
                }
            }

            // Calculate percentage
            decimal percentage = (decimal)rightAnswers / questions.Count * 100;

            // Store the result in the database
            var result = new Result
            {
                TestId = testSubmission.TestId,
                UserId = testSubmission.UserId,
                Score = score,
                NoOfRightAnswers = rightAnswers,
                NoOfWrongAnswers = wrongAnswers,
                Percentage = percentage
            };

            _context.Results.Add(result);
            _context.SaveChanges();

            // Return the result to the frontend
            var resultDto = new ResultDto
            {
                TestId = testSubmission.TestId,
                UserId = testSubmission.UserId,
                NoOfRightAnswers = rightAnswers,
                NoOfWrongAnswers = wrongAnswers,
                Score = score,
                Percentage = percentage,
                QuestionResults = questionResults
            };

            return Ok(resultDto);
        }


    }
}

//.Where(t => t.StartTime <= currentDateTime && t.ExpiryTime >= currentDateTime)
