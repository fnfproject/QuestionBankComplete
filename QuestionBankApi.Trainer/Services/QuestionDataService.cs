using QuestionBankDll.Trainer.Dtos;
using QuestionBankDll.Trainer.Models;
using QuestionBankDll.Trainer.Services;

namespace QuestionBankAPI.Trainer.Services
{
    public interface IQuestionDataService
    {
        Task AddQuestion(Question question);
        Task AddQuestionsInBulk(IFormFile file);
        Task DeleteQuestion(int questionId);
        Task<List<Question>> FindQuestionByText(string text);
        Task<byte[]> GeneratePdf(List<int> questionIds);
        Task<List<Question>> GetAllQuestions();
        Task UpdateQuestion(QuestionDtos questionDtos);
    }

    public class QuestionDataService : IQuestionDataService
    {

        private readonly IQuestionService _questionService;

        public QuestionDataService(IQuestionService service)
        {
            _questionService = service;
        }
        public Task AddQuestion(Question question)
        {
            return Task.Run(() => _questionService.AddQuestion(question));
        }

        public Task AddQuestionsInBulk(IFormFile file)
        {
            try
            {
                return Task.Run(() => _questionService.AddQuestionsInBulk(file));
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public Task DeleteQuestion(int questionId)
        {
            try
            {
                return Task.Run(() => _questionService.DeleteQuestion(questionId));
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public Task<List<Question>> FindQuestionByText(string text)
        {
            try
            {
                return Task.Run(() => _questionService.FindQuestionByText(text));
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public Task<byte[]> GeneratePdf(List<int> questionIds)
        {
            try
            {
                return Task.Run(() => _questionService.GeneratePdf(questionIds));
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public Task<List<Question>> GetAllQuestions()
        {
            try
            {
                return Task.Run(() => _questionService.GetAllQuestions());
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public Task UpdateQuestion(QuestionDtos questionDto)
        {
            try
            {
                return Task.Run(() => _questionService.UpdateQuestionAsync(questionDto));
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

    }
}
