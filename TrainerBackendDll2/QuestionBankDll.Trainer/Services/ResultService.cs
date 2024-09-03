using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuestionBankDll.Trainer.Dtos;
using QuestionBankDll.Trainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankDll.Trainer.Services
{
    public interface IResultService
    {
        List<TraineeTestResultDto> GetResultByTraineeName(string traineeName);
        List<TestTraineeResultAPIDto> GetResultByTestName(string testName);
        List<TestDtos> GetTests();
    }
    public class ResultService : IResultService
    {
        private readonly QuestionDbContext _context;
        public ResultService(QuestionDbContext context)
        {
            _context = context;
        }
        public List<TestTraineeResultAPIDto> GetResultByTestName(string testName)
        {

            var results = new List<TestTraineeResultAPIDto>();

            //var command = _context.Database.GetDbConnection().CreateCommand();
            using (var connection = _context.Database.GetDbConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "GetResultsByTestName"; // Your stored procedure name
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var testNameParam = new SqlParameter("@TestName", testName ?? (object)DBNull.Value);
                command.Parameters.Add(testNameParam);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new TestTraineeResultAPIDto
                        {
                            TraineeName = reader["TraineeName"].ToString(),
                            Score = Convert.ToDecimal(reader["Score"])
                        });
                    }
                }
            }


            return results;
        }

        public List<TraineeTestResultDto> GetResultByTraineeName(string traineeName)
        {

            var results = new List<TraineeTestResultDto>();

            //var command = _context.Database.GetDbConnection().CreateCommand();
            using (var connection = _context.Database.GetDbConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "GetResultsByTraineeName"; // Your stored procedure name
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var traineeNameParam = new SqlParameter("@TraineeName", traineeName ?? (object)DBNull.Value);
                command.Parameters.Add(traineeNameParam);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new TraineeTestResultDto
                        {
                            TestName = reader["TestName"].ToString(),
                            Score = Convert.ToDecimal(reader["Score"])
                        });
                    }
                }
            }

            return results;
        }

        public List<TestDtos> GetTests()
        {
            try
            {
                var tests = _context.Tests.Select(t => new TestDtos
                {
                    TestName = t.TestName,
                    StartTime = t.StartTime,
                    ExpiryTime = t.ExpiryTime,
                }).ToList();
                return tests;
            }
            catch (Exception ex)
            {

                throw ex.InnerException ?? ex;

            }
        }
    }
}
