using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeBackendDll.Dtos;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Interfaces
{
    public interface IQuestionService
    {
        Task<Question> AddQuestionAsync(QuestionDto questionDto);
        Task<IEnumerable<Question>> GetQuestionsAsync();
    }
}
