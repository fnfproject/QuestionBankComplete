using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Dtos
{
    public class TestWithQuestionsDto
    {
        public int TestId { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
