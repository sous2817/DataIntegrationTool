using System;
using DataIntegrationTool.MainProgram.MainWindow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataIntegrationToolTests
{
    [TestClass]
    public class MainWindowBLLTests
    {
        [TestMethod]
        public void TestWizardCount()
        {
            var wizList = MainWindowBLL.GetWizardSteps();

            Assert.IsTrue(wizList.Count==5);
        }

        [TestMethod]
        public void TestEnvironemntName()
        {
            var environmentName = MainWindowBLL.GetCurrentEnvironment();
            Assert.AreEqual(environmentName, "Development");
        }

        [TestMethod]
        public void TestVersionNumber()
        {
            var versionNumber = MainWindowBLL.GetCurrentVerision();
            Assert.AreEqual(versionNumber, "Debugging");
        }
    }
}
