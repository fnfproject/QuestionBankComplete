using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Dtos
{
    public class TestDto

    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        //public string HyperLinks { get; set; }
        public int TestMaxMarks { get; set; }
        public int TestNoOfQuestions { get; set; }
        //public int CreatedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryTime { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}
