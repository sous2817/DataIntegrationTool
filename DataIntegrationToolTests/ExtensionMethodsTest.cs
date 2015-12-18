using DataIntegrationTool.Resources.Enums;
using DataIntegrationTool.Resources.HelperMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataIntegrationToolTests
{
    [TestClass]
    public class ExtensionMethodsTest
    {
        [TestMethod]
        public void GetDescriptionTest()
        {
            var testRule = MatchingRules.Name.List;

            var testDescription = testRule.GetDescription();

            Assert.IsTrue(testDescription == "This rule applies a hierarchical order");
        }

        [TestMethod]
        public void GetDescriptionNoneDefinedTest()
        {
            var testRule = Scoring.ClinWeb.Visualization;

            var testDescription = testRule.GetDescription();

            Assert.IsTrue(testDescription == "Visualization");
        }
    }
}
