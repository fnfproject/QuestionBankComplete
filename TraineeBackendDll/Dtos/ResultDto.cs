using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Dtos
{
    public class ResultDto
    {
        public int TestId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int NoRightAnswers { get; set; }
        public int NoOfWrongAnswers { get; set; }
        public double Percentage { get; set; }
    }
}
