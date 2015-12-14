using System;
using System.Collections.Generic;
using DataIntegrationTool.MainProgram.CleanData;
using DataIntegrationTool.MainProgram.ImportData;
using DataIntegrationTool.MessengerPackages;
using DataIntegrationTool.Resources.Enums;
using DataIntegrationToolTests.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataIntegrationToolTests
{
    [TestClass]
    public class CleanDataTests
    {
        [TestMethod]
        public void ProcessDataTest()
        {
            var allDataStructuresUsed = ImportData.BuildImportDataPackages();
            var cleanDataViewModel = new CleanDataViewModel();

            var obj = new PrivateObject(cleanDataViewModel);
            obj.Invoke("ProcessIncomingData", allDataStructuresUsed);

            var missingCTMSDataStructure = ImportData.BuildImportDataPackages(includeCTMS: false);
            obj.Invoke("ProcessIncomingData", missingCTMSDataStructure);
        }
    }
}
