using System.ComponentModel.DataAnnotations.Schema;

namespace ShufflingTest.Models
{
    [Table("TestTrainee")]
    public class TestTrainee
    {
        public int TestTraineeId { get; set; }
        public int? TestId { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }
        public decimal? Score { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);

        // Method to get CreatedAt in the desired format
        public string GetFormattedCreatedAt()
        {
            return CreatedAt.ToString("M/d/yyyy h:mm:ss tt");
        }

        // Navigation properties
       
        // Add navigation property for Test if needed
    }
}
