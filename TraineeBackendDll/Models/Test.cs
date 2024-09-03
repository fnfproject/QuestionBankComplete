using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Models
{
    
        public class Test
        {
        [Key]
        public int TestId { get; set; }
        public string TestName { get; set; }
            public string Hyperlinks { get; set; }
            public int TestMaxMarks { get; set; }
            public int TestNoOfQuestions { get; set; }
            public int CreatedBy { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime ExpiryTime { get; set; }

        
    }
    }

