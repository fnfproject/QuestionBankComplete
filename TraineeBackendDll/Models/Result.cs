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
        public int ResultId { get; set; }  // Primary key

        [Required]
        public int TestId { get; set; }  // Foreign key for the Test

        [Required]
        public int UserId { get; set; }  // Foreign key for the Trainee who took the test

        [Required]
        public int NoOfRightAnswers { get; set; }  // Number of correct answers

        [Required]
        public int NoOfWrongAnswers { get; set; }  // Number of incorrect answers

        [Required]
        public decimal Score { get; set; }  // Score based on correct answers

        [Required]
        public decimal Percentage { get; set; }  // Percentage score

       
    }
}
