using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankDll.Trainer.Dtos
{
    public class GenerateTestRequest
    {
        public string TestName { get; set; }
        public int CreatedBy { get; set; }
        public List<int> QuestionIds { get; set; }
    }
}
