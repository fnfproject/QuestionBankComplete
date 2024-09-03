using TraineeBackendDll.Dtos;

namespace ShufflingTest.Models
{
    public static class InMemoryDataStore
    {
        private static readonly Dictionary<int, QuestionDto> _questions = new Dictionary<int, QuestionDto>();
        public static readonly Dictionary<int, List<int>> _testToQuestions = new Dictionary<int, List<int>>();
        public static readonly HashSet<int> _liveTests = new HashSet<int>();
        public static void AddQuestion(QuestionDto question)
        {
            // Ensure the question has a unique ID
            if (_questions.ContainsKey(question.QuestionId))
            {
                _questions[question.QuestionId] = question; // Update existing question
            }
            else
            {
                _questions.Add(question.QuestionId, question); // Add new question
            }
        }

        public static bool QuestionExists(int questionId)
        {
            return _questions.ContainsKey(questionId);
        }

        public static List<QuestionDto> GetQuestionsByTestId(int testId)
        {
            if (_testToQuestions.TryGetValue(testId, out var questionIds))
            {
                return questionIds.Select(id => _questions.GetValueOrDefault(id)).Where(q => q != null).ToList();
            }
            return new List<QuestionDto>();
        }

        public static void AddTest(int testId, List<int> questionIds)
        {
            _testToQuestions[testId] = questionIds;
        }

        public static void MakeTestLive(int testId)
        {
            if (_testToQuestions.ContainsKey(testId))
            {
                _liveTests.Add(testId);
            }
        }

        public static bool IsTestLive(int testId)
        {
            return _liveTests.Contains(testId);
        }

       
    }
}
