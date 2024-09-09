using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Dtos
{
    public class TraineeDto
    {
        public int Id { get; set; } // Unique identifier for the trainee
        public string Username { get; set; } // Trainee's full name
        public decimal Score { get; set; } // Trainee's score in the leaderboard
    }
}
