using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        [ForeignKey("TestId")]
        public int TestId { get; set; }

        [ForeignKey("Id")]
        public int UserId { get; set; }
        public int Score { get; set; }
        public int NoRightAnswers { get; set; }
        public int NoOfWrongAnswers { get; set; }
        public double Percentage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);

        // Method to get CreatedAt in the desired format
        public string GetFormattedCreatedAt()
        {
            return CreatedAt.ToString("M/d/yyyy h:mm:ss tt");
        }
    }
}
