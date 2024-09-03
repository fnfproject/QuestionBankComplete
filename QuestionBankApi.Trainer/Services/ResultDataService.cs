using QuestionBankApi.Trainer.Dtos;
using QuestionBankDll.Trainer.Dtos;
using QuestionBankDll.Trainer.Models;
using QuestionBankDll.Trainer.Services;

namespace QuestionBankApi.Trainer.Services
{
    public interface IResultDataService
    {
        Task<List<TestTraineeResultAPIDto>> GetResultByTestName(string testName);
        Task<List<TraineeTestResultDto>> GetResultByTraineeName(string traineeName);
        Task<List<QuestionBankDll.Trainer.Dtos.TestDtos>> GetAllTests();

    }
    public class ResultDataService : IResultDataService
    {
        private readonly IResultService _resultService;
        public ResultDataService(IResultService resultService) 
        { 
            _resultService = resultService;
        }
        public Task<List<TestTraineeResultAPIDto>> GetResultByTestName(string testName)
        {
            try
            {
                return Task.Run(() => _resultService.GetResultByTestName(testName));
            }
            catch (Exception ex)
            {

                throw ex.InnerException ?? ex;

            }
        }

        public Task<List<QuestionBankDll.Trainer.Dtos.TestDtos>> GetAllTests()
        {
            try
            {
                return Task.Run(() => _resultService.GetTests());
            }
            catch (Exception ex)
            {

                throw ex.InnerException ?? ex;

            }
        }

        public Task<List<TraineeTestResultDto>> GetResultByTraineeName(string traineeName)
        {
            try
            {
                return Task.Run(() => _resultService.GetResultByTraineeName(traineeName));
            }
            catch (Exception ex)
            {

                throw ex.InnerException ?? ex;

            }
        }
    }
}
