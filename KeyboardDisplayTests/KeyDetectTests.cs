using KeyboardDisplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyboardDisplay.Tests
{
    [TestClass()]
    public class LabelTests
    {
        [TestMethod()]
        public void typeLabelTextTest_Caps()
        {
            string type = "CapsLock";
            string expected = "Caps Lock";

            string actual = Functions.typeLabelText(type);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void typeLabelTextTest_Num()
        {
            string type = "NumLock";
            string expected = "Num Lock";

            string actual = Functions.typeLabelText(type);

            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass()]
    public class LockStorageTests
    {
        [TestMethod()]
        public void lockStorageTests_Caps_True()
        {
            string type = "CapsLock";
            bool previousLock = true;
            try
            {
                Assert.AreEqual(CapsLock.curstate, true);
            }
            catch (AssertFailedException)
            {
                return;
            }
            if (CapsLock.prevstate != CapsLock.curstate)
            {
                Assert.IsTrue(Functions.changeStoredLock(type, previousLock));
            } else
            {
                Assert.IsFalse(Functions.changeStoredLock(type, previousLock));
            }
        }

        [TestMethod()]
        public void lockStorageTests_Caps_False()
        {
            string type = "CapsLock";
            bool previousLock = true;
            try
            {
                Assert.AreEqual(CapsLock.curstate, false);
            }
            catch (AssertFailedException)
            {
                return;
            }
            if (CapsLock.prevstate != CapsLock.curstate)
            {
                Assert.IsTrue(Functions.changeStoredLock(type, previousLock));
            }
            else
            {
                Assert.IsFalse(Functions.changeStoredLock(type, previousLock));
            }
        }
    }
}