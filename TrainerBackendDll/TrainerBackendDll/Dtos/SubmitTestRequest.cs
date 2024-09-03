using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainerBackendDll.Dtos
{
    public class SubmitTestRequest
    {
        public int TestId { get; set; }
        public int UserId { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }
        public List<UserAnswerDto> UserAnswerDtos { get; set; }
    }
}
