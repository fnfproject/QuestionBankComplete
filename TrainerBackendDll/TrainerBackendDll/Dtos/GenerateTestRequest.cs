using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainerBackendDll.Dtos
{
    public class GenerateTestRequest
    {
        public string TestName { get; set; }
        public int CreatedBy { get; set; }
        public List<int> QuestionIds { get; set; }
        public string Hyperlinks { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime ExpiryTime { get; set; }
    }
}
