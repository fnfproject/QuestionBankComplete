using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeBackendDll.Data;
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Interface;
using TraineeBackendDll.Models;


namespace TraineeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]


    public class QuestionController : ControllerBase
    {
        private readonly QuestionBankDbContext _context;
        private readonly ITestService _testService;

        public QuestionController(QuestionBankDbContext context,ITestService testService)
        {
            _context = context;
            _testService = testService;
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

        [HttpGet("GetLeaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            try
            {
                var leaderboard = await _context.Results
                    .GroupBy(r => r.UserId)
                    .Select(g => new
                    {
                        UserId = g.Key,
                        TotalScore = g.Sum(r => r.Score),
                        Name = _context.Users.Where(u => u.Id == g.Key).Select(u => u.Username).FirstOrDefault()
                    })
                    .OrderByDescending(r => r.TotalScore)
                    .ToListAsync();

                return Ok(leaderboard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetTestProgress/{Id}")]
        public async Task<IActionResult> GetTestProgress(int id)
        {
            try
            {
                var testProgress = await _context.Results
                    .Where(r => r.UserId == id)
                    .Select(r => new
                    {
                        r.TestId,
                        TestName = _context.Tests.Where(t => t.TestId == r.TestId).Select(t => t.TestName).FirstOrDefault(),
                        r.Score,
                        TestDate = _context.Tests.Where(t => t.TestId == r.TestId).Select(t => t.StartTime).FirstOrDefault() // Assuming TestDate exists
                    })
                    .OrderBy(r => r.TestDate) // Order by the test date
                    .ToListAsync();

                return Ok(testProgress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("LiveTests")]
        public async Task<IActionResult> GetLiveTests()
        {
            var liveTests = await _context.Tests
                .Where(t => t.StartTime > DateTime.Now && t.StartTime <= DateTime.Now.AddDays(1))
                .ToListAsync();

            return Ok(liveTests);
        }

        [HttpGet("ExpiredTests")]
        public async Task<IActionResult> GetExpiredTests()
        {
            var expiredTests = await _context.Tests
                .Where(t => t.ExpiryTime < DateTime.Now)
                .ToListAsync();

            return Ok(expiredTests);
        }

        [HttpGet("GetPreviousResults/{traineeId}")]
        public async Task<IActionResult> GetPreviousResults(int traineeId)
        {
            try
            {
                var previousResults = await _context.Results
                    .Where(r => r.UserId == traineeId)
                    .Select(r => new
                    {
                        r.TestId,
                        TestName = _context.Tests.Where(t => t.TestId == r.TestId).Select(t => t.TestName).FirstOrDefault(),
                        r.Score,
                        r.Percentage,
                        CreatedAt = _context.Tests.Where(t => t.TestId == r.TestId).Select(t => t.ExpiryTime).FirstOrDefault() // Assuming EndTime exists
                    })
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                if (!previousResults.Any())
                {
                    return NotFound("No previous results found.");
                }

                return Ok(previousResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

//.Where(t => t.StartTime <= currentDateTime && t.ExpiryTime >= currentDateTime)
