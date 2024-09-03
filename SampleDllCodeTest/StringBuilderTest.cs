using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleDllCode;

namespace SampleDllCodeTest
{
    [TestClass]
    internal class StringBuilderTest
    {
        [TestMethod]

        public void UTC_01()
        {
            string input = "banglore";
            StringEncoder encoder = new StringEncoder();

            var expected = "30,20,21,92,20,80,32,31,02";

            var actual = encoder.EncodeString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]

        public void UTC_02()
        {
            string input = "Global Village";

            var expected = "92,80,32";
        }

        [TestMethod]

        public void UTC_03_ArgumentException()
        {
            
        }
    }
}
