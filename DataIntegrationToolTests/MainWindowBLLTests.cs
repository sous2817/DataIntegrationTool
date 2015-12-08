using System;
using DataIntegrationTool.MainProgram.MainWindow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataIntegrationToolTests
{
    [TestClass]
    public class MainWindowBLLTests
    {
        [TestMethod]
        public void TestEnvironemntName()
        {
            var environmentName = MainWindowBLL.GetCurrentEnvironment();
            Assert.AreEqual(environmentName, "UnitTesting");
        }

        [TestMethod]
        public void TestVersionNumber()
        {
            var versionNumber = MainWindowBLL.GetCurrentVerision();
            Assert.AreEqual(versionNumber, "Debugging");
        }
    }
}
