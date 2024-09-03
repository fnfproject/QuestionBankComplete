using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeBackendDll.Data;
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Interfaces;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Services
{
    public class ResultService : IResultService
    {
        private readonly QuestionBankDbContext _context;

        public ResultService(QuestionBankDbContext context)
        {
            _context = context;
        }

        public async Task<Result> AddResultAsync(ResultDto resultDto)
        {
            var result = new Result
            {
                TestId = resultDto.TestId,
                UserId = resultDto.UserId,
                Score = resultDto.Score,
                NoRightAnswers = resultDto.NoRightAnswers,
                NoOfWrongAnswers = resultDto.NoOfWrongAnswers,
                Percentage = resultDto.Percentage,
                CreatedAt = DateTime.UtcNow
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Result> GetResultAsync(int resultId)
        {
            return await _context.Results.FindAsync(resultId);
        }
    }
}
