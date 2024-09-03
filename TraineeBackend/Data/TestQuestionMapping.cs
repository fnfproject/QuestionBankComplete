namespace TraineeBackend.Data
{
    public class TestQuestionMapping
    {
        public static Dictionary<int, List<int>> TestToQuestions = new Dictionary<int, List<int>>();

        public static void Initialize(Dictionary<int, List<int>> mapping)
        {
            TestToQuestions = mapping;
        }
    }
}
