using OfficeOpenXml.Drawing;
using OfficeOpenXml;
using QuestionBankDll.Trainer.Dtos;
using QuestionBankDll.Trainer.Models;
using QuestionBankDll.Trainer.Services;

namespace QuestionBankAPI.Trainer.Services
{
    public interface IQuestionDataService
    {
        Task AddQuestion(QuestionDtos question);
        //Task AddQuestionsInBulk(IFormFile file);
        string ProcessOption(ExcelRange cellstring, string path);
        Task DeleteQuestion(int questionId);
        Task<List<Question>> FindQuestionByText(string text);
        Task<byte[]> GeneratePdf(List<int> questionIds);
        Task<List<Question>> GetAllQuestions();
        Task UpdateQuestion(Question question);
        public Task<Question> FindQuestion(int questionId);
    }

    public class QuestionDataService : IQuestionDataService
    {

        private readonly IQuestionService _questionService;

        public QuestionDataService(IQuestionService service)
        {
            _questionService = service;
        }
        public Task AddQuestion(QuestionDtos question)
        {
            return Task.Run(() => _questionService.AddQuestion(question));
        }
        public string ProcessOption(ExcelRange cell, string path)
        {
            return _questionService.ProcessOption(cell, path);
        }
      

        //public Task AddQuestionsInBulk(IFormFile file)
        //{
        //    try
        //    {
        //        return Task.Run(() => _questionService.AddQuestionsInBulk(file));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException ?? ex;
        //    }
        //}

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
        public Task<Question> FindQuestion(int questionId)
        {
            try
            {
                return Task.Run(() => _questionService.FindQuestion(questionId));
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

        public Task UpdateQuestion(Question question)
        {
            try
            {
                return Task.Run(() => _questionService.UpdateQue(question));
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        
    }
}
