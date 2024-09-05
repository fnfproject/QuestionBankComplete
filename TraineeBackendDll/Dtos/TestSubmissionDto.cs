using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeBackendDll.Dtos
{
    public class TestSubmissionDto
    {
        public int TestId { get; set; }  // ID of the test being submitted
        public int UserId { get; set; }  // ID of the trainee submitting the test
        public List<QuestionSubmissionDto> SelectedAnswers { get; set; }  // List of question submissions

        public TestSubmissionDto()
        {
            SelectedAnswers = new List<QuestionSubmissionDto>();
        }
    }

    public class QuestionSubmissionDto
    {
        public int QuestionId { get; set; }      // ID of the question
        public string SelectedAnswer { get; set; }  // The answer selected by the trainee
    }

}