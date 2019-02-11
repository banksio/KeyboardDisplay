using KeyboardDisplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace KeyboardDisplay.Tests
{
    [TestClass()]
    public class LabelTests
    {
        [TestMethod()]
        public void typeLabelTextTest_Caps()
        {
            Keys type = Keys.CapsLock;
            string expected = "Caps Lock";

            string actual = Functions.TypeLabelText(type);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void typeLabelTextTest_Num()
        {
            Keys type = Keys.NumLock;
            string expected = "Num Lock";

            string actual = Functions.TypeLabelText(type);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void typeLabelTextTest_Scroll()
        {
            Keys type = Keys.Scroll;
            string expected = "Scroll Lock";

            string actual = Functions.TypeLabelText(type);

            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass()]
    public class LockStorageTests
    {
        [TestMethod()]
        public void lockStorageTests_Caps_True()
        {
            Keys type = Keys.CapsLock;
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
                Assert.IsTrue(Functions.ChangeStoredLock(type, previousLock));
            } else
            {
                Assert.IsFalse(Functions.ChangeStoredLock(type, previousLock));
            }
        }

        [TestMethod()]
        public void lockStorageTests_Caps_False()
        {
            Keys type = Keys.CapsLock;
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
                Assert.IsTrue(Functions.ChangeStoredLock(type, previousLock));
            }
            else
            {
                Assert.IsFalse(Functions.ChangeStoredLock(type, previousLock));
            }
        }

        [TestMethod()]
        public void lockStorageTests_Num_True()
        {
            Keys type = Keys.NumLock;
            bool previousLock = true;
            try
            {
                Assert.AreEqual(NumLock.curstate, true);
            }
            catch (AssertFailedException)
            {
                return;
            }
            if (NumLock.prevstate != NumLock.curstate)
            {
                Assert.IsTrue(Functions.ChangeStoredLock(type, previousLock));
            }
            else
            {
                Assert.IsFalse(Functions.ChangeStoredLock(type, previousLock));
            }
        }

        [TestMethod()]
        public void lockStorageTests_Num_False()
        {
            Keys type = Keys.NumLock;
            bool previousLock = true;
            try
            {
                Assert.AreEqual(NumLock.curstate, false);
            }
            catch (AssertFailedException)
            {
                return;
            }
            if (NumLock.prevstate != NumLock.curstate)
            {
                Assert.IsTrue(Functions.ChangeStoredLock(type, previousLock));
            }
            else
            {
                Assert.IsFalse(Functions.ChangeStoredLock(type, previousLock));
            }
        }

        [TestMethod()]
        public void lockStorageTests_Scroll_True()
        {
            Keys type = Keys.Scroll;
            bool previousLock = true;
            try
            {
                Assert.AreEqual(ScrLock.curstate, true);
            }
            catch (AssertFailedException)
            {
                return;
            }
            if (ScrLock.prevstate != ScrLock.curstate)
            {
                Assert.IsTrue(Functions.ChangeStoredLock(type, previousLock));
            }
            else
            {
                Assert.IsFalse(Functions.ChangeStoredLock(type, previousLock));
            }
        }

        [TestMethod()]
        public void lockStorageTests_Scroll_False()
        {
            Keys type = Keys.Scroll;
            bool previousLock = true;
            try
            {
                Assert.AreEqual(ScrLock.curstate, false);
            }
            catch (AssertFailedException)
            {
                return;
            }
            if (ScrLock.prevstate != ScrLock.curstate)
            {
                Assert.IsTrue(Functions.ChangeStoredLock(type, previousLock));
            }
            else
            {
                Assert.IsFalse(Functions.ChangeStoredLock(type, previousLock));
            }
        }
    }


}