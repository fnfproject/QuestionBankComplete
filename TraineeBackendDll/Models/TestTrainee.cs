using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Models
{
    [Table("TestTrainee")]
    public class TestTrainee
    {
        public int TestTraineeId { get; set; }

        [ForeignKey("TestId")]
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

