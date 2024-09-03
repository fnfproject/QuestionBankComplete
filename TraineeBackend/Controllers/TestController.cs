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
    public class TestController : ControllerBase
    {
        private readonly QuestionBankDbContext _context;

        public TestController(QuestionBankDbContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //public async Task<ActionResult<Test>> AddTest(TestDto testDto)
        //{
        //    var test = new Test
        //    {
        //        TestName = testDto.TestName,
        //        TestMaxMarks = testDto.TestMaxMarks,
        //        TestNoOfQuestions = testDto.TestNoOfQuestions,
        //        StartTime = testDto.StartTime,
        //        ExpiryTime = testDto.ExpiryTime,
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    _context.Tests.Add(test);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetTestById), new { id = test.TestId }, test);
        //}

        //[HttpGet("{testId}")]
        //public async Task<ActionResult<Test>> GetTestById(int testId)
        //{
        //    var test = await _context.Tests.Include(t => t.Questions)
        //                                   .FirstOrDefaultAsync(t => t.TestId == testId);

        //    if (test == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(test);
        //}
    }
}