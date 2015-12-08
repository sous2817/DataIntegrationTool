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
    }
}
