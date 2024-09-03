using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TraineeBackend.Data;
using TraineeBackendDll.Data;
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Models;

namespace TraineeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionBankDbContext _context;

        public QuestionController(QuestionBankDbContext context)
        {
            _context = context;
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // POST: api/Questions/AddQuestions
        [HttpPost("AddQuestions")]
        public async Task<ActionResult<QuestionDto>> AddQuestion(QuestionDto questionDto)
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
                CreatedAt = DateTime.UtcNow
            };

            // Add question to the database
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            // Add question to the dictionary
            var questionDictionary = _context.GetService<IDictionary<int, QuestionDto>>();
            questionDictionary[question.QuestionId] = questionDto;

            return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, questionDto);
        }

        // GET: api/Questions/ByTestId/5
        [HttpGet("ByTestId/{testId}")]
        public async Task<IActionResult> GetQuestionsByTestId(int testId)
        {
            if (!TestQuestionMapping.TestToQuestions.ContainsKey(testId))
            {
                return NotFound("Test ID not found");
            }

            var questionIds = TestQuestionMapping.TestToQuestions[testId];
            var questions = await _context.Questions
                                          .Where(q => questionIds.Contains(q.QuestionId))
                                          .ToListAsync();

            return Ok(questions);
        }
    }
}
