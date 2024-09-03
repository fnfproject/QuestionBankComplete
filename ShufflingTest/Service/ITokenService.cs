using ShufflingTest.Dtos;

namespace ShufflingTest.Service
{
    public interface ITokenService
    {
        string GenerateUniqueToken(int testId, string userId);
        bool ValidateToken(string token);
    }

    public class TokenService : ITokenService
    {
        private readonly Dictionary<string, (int TestId, string UserId)> _tokens = new();

        public string GenerateUniqueToken(int testId, string userId)
        {
            string token = Guid.NewGuid().ToString();
            _tokens[token] = (testId, userId);
            return token;
        }

        public bool ValidateToken(string token)
        {
            return _tokens.ContainsKey(token);
        }
    }

    // IQuestionService.cs
    public interface IQuestionService
    {
        List<ShuffledQuestionDto> GetShuffledQuestionsByToken(string token);
    }

    // QuestionService.cs
    //public class QuestionService : IQuestionService
    //{
    //    public List<ShuffledQuestionDto> GetShuffledQuestionsByToken(string token)
    //    {
    //        // Implement the logic to fetch shuffled questions based on the token
    //        // For now, return a mocked list of questions
    //        return new List<ShuffledQuestionDto>
    //    {
    //        new ShuffledQuestionDto { QuestionText = "Sample Question 1", ShuffledOptions = new List<string> { "Option A", "Option B", "Option C" } },
    //        new ShuffledQuestionDto { QuestionText = "Sample Question 2", ShuffledOptions = new List<string> { "Option D", "Option E", "Option F" } }
    //    };
    //    }
    //}
}
