using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using TraineeBackendDll.Models;

namespace ShufflingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class TestController : ControllerBase
    {
        private readonly string connectionString = "Data Source=FNFIDVPRE20522\\SQLSERVER;Initial Catalog=QuestionBankDatabase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";

        [HttpPost("store-hyperlink")]
        public IActionResult StoreHyperlink([FromBody] Test test)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Test (TestName, Hyperlinks, TestMaxMarks, TestNoOfQuestions, CreatedBy, StartTime, ExpiryTime) VALUES (@TestName, @Hyperlinks, @TestMaxMarks, @TestNoOfQuestions, @CreatedBy, @StartTime, @ExpiryTime)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TestName", test.TestName);
                cmd.Parameters.AddWithValue("@Hyperlinks", test.Hyperlinks);
                cmd.Parameters.AddWithValue("@TestMaxMarks", test.TestMaxMarks);
                cmd.Parameters.AddWithValue("@TestNoOfQuestions", test.TestNoOfQuestions);
                cmd.Parameters.AddWithValue("@CreatedBy", test.CreatedBy);
                cmd.Parameters.AddWithValue("@StartTime", test.StartTime);
                cmd.Parameters.AddWithValue("@ExpiryTime", test.ExpiryTime);
                cmd.ExecuteNonQuery();
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetTestById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Test WHERE TestId = @TestId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TestId", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var test = new Test
                    {
                        TestName = reader["TestName"].ToString(),
                        Hyperlinks = reader["Hyperlinks"].ToString(),
                        TestMaxMarks = Convert.ToInt32(reader["TestMaxMarks"]),
                        TestNoOfQuestions = Convert.ToInt32(reader["TestNoOfQuestions"]),
                        CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                        StartTime = Convert.ToDateTime(reader["StartTime"]),
                        ExpiryTime = Convert.ToDateTime(reader["ExpiryTime"])
                    };
                    return Ok(test);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost("generate-hyperlink")]
        public IActionResult GenerateHyperlink(int testId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Question WHERE TestId = @TestId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TestId", testId);
                SqlDataReader reader = cmd.ExecuteReader();

                var questions = new List<Question>();
                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        QuestionText = reader["QuestionText"].ToString(),
                        OptionA = reader["OptionA"].ToString(),
                        OptionB = reader["OptionB"].ToString(),
                        OptionC = reader["OptionC"].ToString(),
                        OptionD = reader["OptionD"].ToString(),
                        CorrectAnswer = reader["CorrectAnswer"].ToString()
                    });
                }

                // Serialize questions to JSON
                var questionsJson = JsonConvert.SerializeObject(questions);
                var encodedQuestions = WebUtility.UrlEncode(questionsJson);

                // Generate hyperlink
                var hyperlink = $"http://localhost:5011/question?data={encodedQuestions}";

                // Store hyperlink in TestTable
                string updateQuery = "UPDATE Test SET Hyperlinks = @Hyperlinks WHERE TestId = @TestId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@Hyperlinks", hyperlink);
                updateCmd.Parameters.AddWithValue("@TestId", testId);
                updateCmd.ExecuteNonQuery();

                return Ok(hyperlink);
            }
        }
    }

    public class Test
    {
        public string TestName { get; set; }
        public string Hyperlinks { get; set; }
        public int TestMaxMarks { get; set; }
        public int TestNoOfQuestions { get; set; }
        public int CreatedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryTime { get; set; }
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
    }


}