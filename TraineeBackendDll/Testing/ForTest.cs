using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TraineeBackendDll.Testing
{
    
    public class ForTest
    {
        private static Dictionary<int, List<int>> TestQuestionsMapping = new Dictionary<int, List<int>>();

        public void AddQuestionsToTest(int testId, List<int> questionIds)
        {
            if (TestQuestionsMapping.ContainsKey(testId))
            {
                TestQuestionsMapping[testId].AddRange(questionIds);
            }
            else
            {
                TestQuestionsMapping[testId] = new List<int>(questionIds);
            }
        }
    }
}
