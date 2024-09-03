using QuestionBankDll.Trainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainerBackendDll.Dtos
{
    public class TestQuestion
    {
        public int TestId { get; set; }
        public Test Test { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
