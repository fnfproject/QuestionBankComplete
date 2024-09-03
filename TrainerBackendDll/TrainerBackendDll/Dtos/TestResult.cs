using QuestionBankDll.Trainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainerBackendDll.Dtos
{
    public class TestResult
    {
        public int TestResultId { get; set; }  // Unique identifier for the result entry.
        public int TestId { get; set; }         // The ID of the test that was taken.
        public int UserId { get; set; }         // The ID of the user who took the test.
        public decimal Score { get; set; }     // The score achieved by the user on the test.
        public DateTime CompletedAt { get; set; }  // The date and time when the test was completed.

        // Navigation properties to other entities.
        public Test Test { get; set; }  // Navigation property to the Test entity.
        public UserTbl UserTbl { get; set; }  // Navigation property to the User entity.
    }
}
