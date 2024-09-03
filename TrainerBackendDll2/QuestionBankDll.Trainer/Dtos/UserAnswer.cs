using QuestionBankDll.Trainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankDll.Trainer.Dtos
{
    public class UserAnswer
    {
        // Unique identifier for the user answer record
        public int UserAnswerId { get; set; }

        // The ID of the test this answer is associated with
        public int TestId { get; set; }

        // The ID of the question being answered
        public int QuestionId { get; set; }

        // The ID of the user who submitted the answer
        public int UserId { get; set; }

        // The option selected by the user (e.g., "A", "B", "C", "D")
        public string SelectedOption { get; set; }

        // Indicates whether the selected answer was correct
        public bool IsCorrect { get; set; }

        // Timestamp of when the answer was submitted
        public DateTime AnsweredAt { get; set; }

        // Navigation property for the related Test entity
        public Test Test { get; set; }

        // Navigation property for the related Question entity
        public Question Question { get; set; }

        // Navigation property for the related User entity
        public UserTbl User { get; set; }
    }

}
