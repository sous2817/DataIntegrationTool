using System;
using DataIntegrationTool.MainProgram.ImportData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataIntegrationToolTests
{
    [TestClass]
    public class ImportDataTests
    {
        [TestMethod]
        public void ImportClinWebTest()
        {
            var clinWebCanvasList = ImportDataBLL.ImportClinWeb().Result;
            Assert.IsTrue(clinWebCanvasList.Count >0);
        }

        [TestMethod]
        public void GetClinWebDataTest()
        {
            var investigatorCollection = ImportDataBLL.GetInvestigationalPerformanceCollection(new Guid("d422355b-6877-48b3-9bf4-83dc7cd18e18")).Result;
            Assert.IsTrue(investigatorCollection.Items.Count >0);
        }
        [TestMethod]
        public void ConvertClinWebData()
        {
            var investigatorCollection = ImportDataBLL.GetInvestigationalPerformanceCollection(new Guid("d422355b-6877-48b3-9bf4-83dc7cd18e18")).Result;
            var clinWebDataTable = ImportDataBLL.ConvertClinWebDataToDataView(investigatorCollection);
            Assert.IsTrue(clinWebDataTable.Count >0);
        }
    }
}
