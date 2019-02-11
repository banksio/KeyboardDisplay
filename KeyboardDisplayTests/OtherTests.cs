using System;
using KeyboardDisplay;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace KeyboardDisplay.Tests
{
    [TestClass]
    public class RegistryTests
    {
        [TestMethod]
        public void CreateCurrentUserKeyTest()
        {
            //TODO TEST CREATION OF REG KEYS
        }
    }

    [TestClass()]
    public class UpdaterTests
    {
        [TestMethod()]
        public void NotifyIconTest()
        {
            var expected = typeof(NotifyIcon);

            var testNI = UpdateManager.CreateNotifyIcon();

            Assert.IsInstanceOfType(testNI, expected);
        }

        //[TestMethod()]
        //public void NotifyIconTest()
        //{
        //    var expected = typeof(NotifyIcon);

        //    var testNI = UpdateManager.CreateNotifyIcon();

        //    Assert.IsInstanceOfType(testNI, expected);
        //}
    }
}
