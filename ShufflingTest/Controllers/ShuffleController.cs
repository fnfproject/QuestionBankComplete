using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ShufflingTest.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

using TraineeBackendDll.Interfaces;
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Services;
using TraineeBackendDll.Models;
using TraineeBackendDll.Data;
using ShufflingTest.Models;
using System.Text.Json;


namespace ShufflingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class ShuffleController : ControllerBase
    {
        private readonly List<Question> _questions;

        private readonly QuestionBankDbContext _context;

        private readonly IQuestionService _questionService;

        private readonly IResultService _resultService;

        private readonly ITestService _testService;

        public ShuffleController(QuestionBankDbContext context, IQuestionService questionService, IResultService resultService, ITestService testService)
        {
            _context = context;
            _questionService = questionService;
            _resultService = resultService;
            _testService = testService;
            // Sample data, in a real scenario this would come from a database
            //    _questions = new List<Question>
            //    {
            //        new Question
            //        {
            //            Id = 1,
            //            Text = "What is the capital of France?",
            //            Options = new List<Option>
            //            {
            //                new Option { Id = 1, Text = "Paris" },
            //                new Option { Id = 2, Text = "Berlin" },
            //                new Option { Id = 3, Text = "Rome" },
            //                new Option { Id = 4, Text = "Madrid" }
            //            },
            //            CorrectOptionId = 1
            //        },
            //        new Question
            //        {
            //            Id = 2,
            //            Text = "What is 2+2?",
            //            Options = new List<Option>
            //            {
            //                new Option { Id = 1, Text = "3" },
            //                new Option { Id = 2, Text = "4" },
            //                new Option { Id = 3, Text = "5" },
            //                new Option { Id = 4, Text = "6" }
            //            },
            //            CorrectOptionId = 2
            //        }
            //    };
            //}

            //[HttpGet("generate-shuffled-questions")]
            //public ActionResult<List<ShuffledQuestionDto>> GenerateShuffledQuestions()
            //{
            //    // Shuffle the order of questions
            //    var shuffledQuestions = _questions
            //        .OrderBy(q => Guid.NewGuid()) // Shuffle questions
            //        .Select(q => new ShuffledQuestionDto
            //        {
            //            QuestionText = q.Text,
            //            ShuffledOptions = q.Options
            //                .OrderBy(o => Guid.NewGuid()) // Shuffle options within each question
            //                .Select(o => o.Text)
            //                .ToList()
            //        })
            //        .ToList();

            //    return Ok(shuffledQuestions);
        }

        [HttpGet("TestResult/{id}")]
        public async Task<ActionResult<AverageScoreDto>> GetAverageScore(int id)
        {
            var results = await _context.TestTrainees
                .Where(tr => tr.UserId == id && tr.Status == "Completed")
                .ToListAsync();

            if (results == null || results.Count == 0)
            {
                return NotFound("No test results found for this user.");
            }

            var averageScore = results.Average(tr => tr.Score ?? 0);

            var averageScoreDto = new AverageScoreDto
            {
                UserId = id,
                AverageScore = averageScore
            };

            return Ok(averageScoreDto);
        }

        // New endpoint to get detailed test results by userId
        [HttpGet("TestResults/{id}")]
        public async Task<ActionResult<List<TestResultDto>>> GetTestResults(int id)
        {
            var testResults = await _context.TestTrainees
                .Where(tr => tr.UserId == id)
                .Select(tr => new TestResultDto
                {
                    Status = tr.Status,
                    Score = tr.Score,
                    CreatedAt = tr.CreatedAt
                })
                .ToListAsync();

            if (testResults == null || testResults.Count == 0)
            {
                return NotFound("No test results found for this user.");
            }

            return Ok(testResults);
        }

        //[HttpPost("AddQuestion")]
        //public async Task<IActionResult> AddQuestion([FromBody] QuestionDto questionDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _questionService.AddQuestionAsync(questionDto);
        //    return CreatedAtAction(nameof(GetQuestions), new { id = result.QuestionId }, result);
        //}

        //[HttpGet("GetQuestions")]
        //public async Task<IActionResult> GetQuestions()
        //{
        //    var questions = await _questionService.GetQuestionsAsync();
        //    //return Ok(questions);
        //}

        [HttpPost("AddResult")]
        public async Task<IActionResult> AddResult([FromBody] ResultDto resultDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _resultService.AddResultAsync(resultDto);
            return CreatedAtAction(nameof(GetResult), new { id = result.ResultId }, result);
        }

        [HttpGet("GetResult/{id}")]
        public async Task<IActionResult> GetResult(int id)
        {
            var result = await _resultService.GetResultAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var test = await _testService.GetTestAsync(result.TestId);
            return Ok(new { Result = result, Test = test });
        }

        //[HttpPost("AddTest")]
        //public async Task<IActionResult> AddTest([FromBody] TestDto testDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var test = await _testService.AddTestAsync(testDto);
        //    return CreatedAtAction(nameof(GetTest), new { id = test.TestId }, test);
        //}

        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion([FromBody] QuestionDto questionDto)
        {
            if (questionDto == null)
            {
                return BadRequest("Question data must be provided.");
            }

            // Simulate adding the question to the in-memory data store
            InMemoryDataStore.AddQuestion(questionDto);

            return Ok($"Question added with ID {questionDto.QuestionId}.");
        }


        //[HttpGet("GetTest/{id}")]
        //public async Task<IActionResult> GetTest(int id)
        //{
        //    var test = await _testService.GetTestAsync(id);

        //    if (test == null)
        //    {
        //        return NotFound();
        //    }

        //    var questions = await _questionService.GetQuestionsAsync();
        //    return Ok(new { Test = test, Questions = questions });
        //}

        // GET: api/Tests/{testId}/questions
        [HttpGet("{testId}/questions")]
        public IActionResult GetQuestionsByTestId(int testId)
        {
            var questions = InMemoryDataStore.GetQuestionsByTestId(testId);

            if (questions == null || !questions.Any())
            {
                return NotFound($"No questions found for test ID {testId}.");
            }

            return Ok(questions);
        }

        // POST: api/Tests/{testId}/questions
        [HttpPost("{testId}/questions")]
        public IActionResult AddQuestionsToTest(int testId, [FromBody] List<int> questionIds)
        {
            if (questionIds == null || !questionIds.Any())
            {
                return BadRequest("Question IDs must be provided.");
            }

            // Ensure all provided question IDs exist in the dictionary
            foreach (var questionId in questionIds)
            {
                if (!InMemoryDataStore.QuestionExists(questionId))
                {
                    return BadRequest($"Question ID {questionId} does not exist.");
                }
            }

            // Add or update the test with associated questions
            InMemoryDataStore.AddTest(testId, questionIds);

            return Ok($"Questions added to test ID {testId}.");
        }

        // GET: api/Tests/{testId}/details
        [HttpGet("{testId}/details")]
        public IActionResult GetTestWithQuestions(int testId)
        {
            var questions = InMemoryDataStore.GetQuestionsByTestId(testId);

            if (questions == null || !questions.Any())
            {
                return NotFound($"Test with ID {testId} not found.");
            }

            var testWithQuestions = new TestWithQuestionsDto
            {
                TestId = testId,
                Questions = questions
            };

            return Ok(testWithQuestions);
        }

        // POST: api/Tests/{testId}/live
        [HttpPost("{testId}/live")]
        public IActionResult MakeTestLive(int testId)
        {
            if (!InMemoryDataStore._testToQuestions.ContainsKey(testId))
            {
                return NotFound($"Test with ID {testId} not found.");
            }

            InMemoryDataStore.MakeTestLive(testId);
            return Ok($"Test with ID {testId} is now live.");
        }




        [HttpPost("SubmitTest")]
        public async Task<ActionResult> SubmitTest([FromBody] TestSubmissionDto submission)
        {
            var result = await _testService.SubmitTestAnswerAsync(submission);
            if (!result)
            {
                return BadRequest("Failed to submit test results.");
            }

            return Ok("Test submitted successfully.");
        }

        //[HttpGet("GetTestQuestions/{testId}")]
        //public async Task<IActionResult> GetTestQuestions(int testId)
        //{
        //    var questions = await _testService.GetQuestionsByTestIdAsync(testId);
        //    if (questions == null || !questions.Any())
        //    {
        //        return NotFound("No questions found for the given test ID.");
        //    }

        //    var questionDtos = questions.Select(q => new Question
        //    {
        //        QuestionId = q.QuestionId,
        //        Subject = q.Subject,
        //        Topic = q.Topic,
        //        DifficultyLevel = q.DifficultyLevel,
        //        QuestionText = q.QuestionText,
        //        OptionA = q.OptionA,
        //        OptionB = q.OptionB,
        //        OptionC = q.OptionC,
        //        OptionD = q.OptionD,
        //        CorrectAnswer = q.CorrectAnswer,
        //        Explaination = q.Explaination,
        //        CreatedBy = q.CreatedBy,
        //        CreatedAt = q.CreatedAt
        //    }).ToList();

        //    return Ok(questionDtos);
        //}

        //[HttpPost("SubmitTest/{testId}")]
        //public async Task<IActionResult> SubmitTest(int testId, [FromBody] Dictionary<int, string> selectedAnswers)
        //{
        //    var userId = 1; // Get the user ID from the current context/session
        //    var result = await _testService.SubmitTestAsync(testId, selectedAnswers, userId);
        //    if (result == null)
        //    {
        //        return NotFound("No questions found for the given test ID.");
        //    }

        //    return Ok(result);
        //}


        [HttpGet]
        public IActionResult GetQuestions()
        {
            var questions = _context.Questions.ToList();
            return Ok(questions);
        }

        [HttpPost("save-selected")]
        public IActionResult SaveSelectedQuestions([FromBody] List<int> selectedQuestionIds)
        {
            var selectedQuestions = _context.Questions.Where(q => selectedQuestionIds.Contains(q.QuestionId)).ToList();
            var json = JsonSerializer.Serialize(selectedQuestions);
            System.IO.File.WriteAllText("selectedQuestions.json", json);
            return Ok();
        }

        [HttpGet("load-selected")]
        public IActionResult LoadSelectedQuestions()
        {
            if (!System.IO.File.Exists("selectedQuestions.json")) return Ok(new List<Question>());
            var json = System.IO.File.ReadAllText("selectedQuestions.json");
            var selectedQuestions = JsonSerializer.Deserialize<List<Question>>(json);
            return Ok(selectedQuestions);
        }

        //[HttpGet("{testId}")]
        //public async Task<ActionResult<TestDto>> GetTest(int testId)
        //{
        //    var test = await _context.Tests
        //        .Include(t => t.Questions)
        //        .FirstOrDefaultAsync(t => t.TestId == testId);

        //    if (test == null)
        //    {
        //        return NotFound();
        //    }

        //    var testDto = new TestDto
        //    {
        //        TestId = test.TestId,
        //        TestName = test.TestName,
        //        Questions = test.Questions.Select(q => new QuestionDto
        //        {
        //            QuestionId = q.QuestionId,
        //            QuestionText = q.QuestionText,
        //            // Map other properties as needed
        //        }).ToList()
        //    };

        //    return Ok(testDto);
        //}
    }
}


