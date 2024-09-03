using System.ComponentModel.DataAnnotations;
using TrainerBackendDll.Dtos;

namespace QuestionBankDll.Trainer.Models
{
    public class Test
    {
        public int TestId { get; set; }
        [Required]
        public string TestName { get; set; }
        public int TestMaxMarks { get; set; }
        public int TestNoOfQuestions { get; set; }
        public int CreatedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Hyperlinks { get; set; }

        public ICollection<TestQuestion> TestQuestions { get; set; }

    }
}
