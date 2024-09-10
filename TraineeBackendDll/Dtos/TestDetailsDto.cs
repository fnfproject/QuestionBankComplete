using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeBackendDll.Models;

namespace TraineeBackendDll.Dtos
{
    public class TestDetailsDto
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int TestMaxMarks { get; set; }
        public int TestNoOfQuestions { get; set; }
        public int CreatedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string HyperLinks { get; set; }
        public List<QuestionDto> ModelTestQuestions { get; set; } // Update this property

        
    }
}
