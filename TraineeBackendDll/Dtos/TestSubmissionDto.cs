using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Dtos
{
    public class TestSubmissionDto
    {
        public int TestId { get; set; }

        public Dictionary<int ,int> SelectedAnswers { get; set; }
     }
}
