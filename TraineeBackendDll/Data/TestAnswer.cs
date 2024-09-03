using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Data
{
   
        public class TestAnswer
        {
            public int Id { get; set; }
            public int TestId { get; set; }
            public int QuestionId { get; set; }
            public int SelectedOptionId { get; set; }

            // Navigation properties
            public Test Test { get; set; }
            public Question Question { get; set; }
           // public Option SelectedOption { get; set; }
        }
    }
