using System.Reflection.Metadata.Ecma335;
using Moq;
using SampleDllCode;

namespace SampleDllCodeTest
{
    [TestClass]
    public class MockMathComponentTest
    {
        private MathComponent component = null;

        [TestInitialize]

        public void SetUp()

        {
           var mock = new Mock<MathComponent>();
            mock.Setup(m => m.addFunc(3, 2)).Returns(5);
            mock.Setup(m => m.subFunc(3, 2)).Returns(1);
            component = mock.Object;
            Console.WriteLine("Added Successfully");


        }

        [TestMethod]
        public void TestFor_AddFunc()
        {
            //var mock = new Mock<MathComponent>();
           // mock.Setup(m => m.addFunc(3,2)).Returns(5);
           // var component = mock.Object;

           // var component = new mathComponent();

            var actual = component.addFunc(3,2);

            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void TestFor_SubFunc()
        {
           // var component = new mathComponent();

            var actual = component.subFunc(3,2);

            Assert.AreEqual(1, actual);
        }

        
    }
}