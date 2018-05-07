using KeyboardDisplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyboardDisplay.Tests
{
    [TestClass()]
    public class KeyDetectTests
    {
        [TestMethod()]
        public void typeLabelTextTest()
        {
            string type = "CapsLock";
            string expected = "Caps Lock";

            string actual = Functions.typeLabelText(type);

            Assert.AreEqual(expected, actual);
        }
    }
}

namespace KeyboardDisplayTests
{
    [TestClass]
    public class KeyDetectTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            
        }
    }
}
