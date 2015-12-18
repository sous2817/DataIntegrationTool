using DataIntegrationTool.MainProgram.CleanData;
using DataIntegrationToolTests.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataIntegrationToolTests
{
    [TestClass]
    public class CleanDataTests
    {
        [TestMethod]
        public void ProcessDataAllTest()
        {
            var allDataStructuresUsed = ImportData.BuildImportDataPackages();
            var cleanDataViewModel = new CleanDataViewModel();

            var obj = new PrivateObject(cleanDataViewModel);
            obj.Invoke("ProcessIncomingData", allDataStructuresUsed);
            Assert.IsTrue(cleanDataViewModel.BaseCleanDataPackage.CleanDataRules.Count > 0 &&
                          cleanDataViewModel.Comparer1CleanDataPackage.CleanDataRules.Count > 0 &&
                          cleanDataViewModel.Comparer2CleanDataPackage.CleanDataRules.Count > 0 &&
                          cleanDataViewModel.Comparer3CleanDataPackage.CleanDataRules.Count > 0);

            var missingCTMSDataStructure = ImportData.BuildImportDataPackages(includeCTMS: false);
            obj.Invoke("ProcessIncomingData", missingCTMSDataStructure);
        }

        [TestMethod]
        public void ProcessDataMissingOneTest()
        {
            var cleanDataViewModel = new CleanDataViewModel();

            var obj = new PrivateObject(cleanDataViewModel);
            var missingCTMSDataStructure = ImportData.BuildImportDataPackages(includeCTMS: false);
            obj.Invoke("ProcessIncomingData", missingCTMSDataStructure);
            Assert.IsTrue(cleanDataViewModel.BaseCleanDataPackage.CleanDataRules.Count > 0 &&
                          cleanDataViewModel.Comparer1CleanDataPackage.CleanDataRules.Count == 0 &&
                          cleanDataViewModel.Comparer2CleanDataPackage.CleanDataRules.Count > 0 &&
                          cleanDataViewModel.Comparer3CleanDataPackage.CleanDataRules.Count > 0);
        }

        [TestMethod]
        public void GetRulesListTest()
        {
            var rulesList = CleanDataBLL.GetRulesList();

            Assert.IsTrue(rulesList.Count >0);


        }
    }
}
