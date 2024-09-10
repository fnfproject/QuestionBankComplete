using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Dtos
{
    public class ResultDto
    {
        public int TestId { get; set; }          // ID of the test

        public string TestName { get; set; }

        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }          // ID of the trainee
        public int NoOfRightAnswers { get; set; }  // Number of correct answers
        public int NoOfWrongAnswers { get; set; }  // Number of wrong answers
        public decimal Score { get; set; }        // Calculated score
        public decimal Percentage { get; set; }   // Percentage score
        public List<QuestionResultDto> QuestionResults { get; set; }  // Per-question results with explanations

        public ResultDto()
        {
            QuestionResults = new List<QuestionResultDto>();
        }
    }

    public class QuestionResultDto
    {
        public int QuestionId { get; set; }       // ID of the question
        public string SelectedAnswer { get; set; }  // Answer selected by the trainee
        public string CorrectAnswer { get; set; }   // Correct answer
        public bool IsCorrect { get; set; }        // Whether the answer was correct
        public string Explanation { get; set; }    // Explanation for the correct answer
    }
}