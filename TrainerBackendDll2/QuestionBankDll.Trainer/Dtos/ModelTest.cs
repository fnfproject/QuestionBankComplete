using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankDll.Trainer.Dtos
{
    public class ModelTest
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int TestMaxMarks { get; set; }
        public int TestNoOfQuestions { get; set; }
        public string Hyperlinks { get; set; }
        public List<ModelTestQuestion> ModelTestQuestions { get; set; }
    }
}
