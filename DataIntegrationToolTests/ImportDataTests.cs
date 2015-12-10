using System;
using DataIntegrationTool.MainProgram.ImportData;
using DataIntegrationTool.Resources.Enums;
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
        public void ConvertClinWebDataTest()
        {
            var investigatorCollection = ImportDataBLL.GetInvestigationalPerformanceCollection(new Guid("d422355b-6877-48b3-9bf4-83dc7cd18e18")).Result;
            var clinWebDataTable = ImportDataBLL.ConvertClinWebDataToDataView(investigatorCollection);
            Assert.IsTrue(clinWebDataTable.Count >0);
        }

        [TestMethod]
        public void GetCTMSFileTest()
        {
            var ctmsDataTable = 
                ImportDataBLL.GetFileData(
                    @"..\..\TestDataFiles\CTMSTestFile.xlsx").Result;
            Assert.IsTrue(ctmsDataTable.DefaultView.Count >0);
        }

        [TestMethod]
        public void GetBioPharmFileTest()
        {
            var biopharmDataTable = ImportDataBLL.GetFileData(@"..\..\TestDataFiles\BioPharmTestFile.xlsx").Result;
            Assert.IsTrue(biopharmDataTable.DefaultView.Count >0);
        }

        [TestMethod]
        public void GetOtherFileTest()
        {
            var otherFileDataTable = ImportDataBLL.GetFileData(@"..\..\TestDataFiles\ClinWebTestFile.xlsx").Result;
            Assert.IsTrue(otherFileDataTable.DefaultView.Count >0);
        }

        [TestMethod]
        public void MissingCTMSMandatoryColumns()
        {
            var ctmsDataTable =
                ImportDataBLL.GetFileData(
                    @"..\..\TestDataFiles\CTMSTestFile.xlsx").Result;
            var noMissingColumns = ImportDataBLL.MissingMandatoryColumnList(ImportSource.FileSource.CTMS, ctmsDataTable);

            Assert.IsTrue(noMissingColumns.Count == 0);

            var missingColumns = ImportDataBLL.MissingMandatoryColumnList(ImportSource.FileSource.BioPharm,
                ctmsDataTable);

            Assert.IsTrue(missingColumns.Count >0);
        }

        [TestMethod]
        public void MissingBioPharmMandatoryColumns()
        {
            var bioPharmDataTable =
                ImportDataBLL.GetFileData(
                    @"..\..\TestDataFiles\BioPharmTestFile.xlsx").Result;
            var noMissingColumns = ImportDataBLL.MissingMandatoryColumnList(ImportSource.FileSource.BioPharm, bioPharmDataTable);

            Assert.IsTrue(noMissingColumns.Count == 0);

            var missingColumns = ImportDataBLL.MissingMandatoryColumnList(ImportSource.FileSource.CTMS,
                bioPharmDataTable);

            Assert.IsTrue(missingColumns.Count > 0);
        }

    }
}
