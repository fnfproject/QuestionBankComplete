
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Interfaces
{
    public interface ITestService
    {
        Task<List<Question>> GetQuestionsByTestIdAsync(int testId);

        Task<Test> GetTestAsync(int testId);

        void AddQuestionsToTest(int testId, List<int> questionIds);

        Task<TestWithQuestionsDto> GetTestWithQuestionsAsync(int testId);

        Task<bool> SubmitTestAnswerAsync(TestSubmissionDto submissionDto);


       // Task<Test> AddTestAsync(TestDto testDto);
       // Task<Test> GetTestAsync(int testId);

        //Task<List<Question>> GetQuestionsByTestIdAsync(int testId);
        //Task<ResultDto> SubmitTestAsync(int testId, Dictionary<int, string> selectedAnswers,int userId);
    }
}
