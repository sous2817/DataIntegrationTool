using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIntegrationTool.MainProgram.ImportData;
using DataIntegrationTool.MessengerPackages;
using DataIntegrationTool.Resources.Enums;

namespace DataIntegrationToolTests.DataStructures
{
    public static class ImportData
    {
        public static List<ImportDataPackage> BuildImportDataPackages(bool includeCTMS = true, bool includeBioPharm = true, bool includeClinWeb = true, bool includeOther = true)
        {
            var investigatorCollection =
                ImportDataBLL.GetInvestigationalPerformanceCollection(new Guid("d422355b-6877-48b3-9bf4-83dc7cd18e18"))
                    .Result;
            var clinWebDataTable = ImportDataBLL.ConvertClinWebDataToDataView(investigatorCollection);
            var ctmsDataTable =
                ImportDataBLL.GetFileData(
                    @"..\..\TestDataFiles\CTMSTestFile.xlsx").Result;
            var biopharmDataTable = ImportDataBLL.GetFileData(@"..\..\TestDataFiles\BioPharmTestFile.xlsx").Result;
            var otherFileDataTable = ImportDataBLL.GetFileData(@"..\..\TestDataFiles\ClinWebTestFile.xlsx").Result;


            var clinWebDataPackage = new ImportDataPackage
            {
                ClinWebCanvasGuid = new Guid("d422355b-6877-48b3-9bf4-83dc7cd18e18"),
                FileSource = DataIntegrationTool.Resources.Enums.ImportSource.FileSource.ClinWeb,
                DataGridToPopulate = "Base",
                ShortDescription = "Infosario Planning",
                ImportedData = clinWebDataTable
            };

            var ctmsDataPackage = new ImportDataPackage
            {
                FileSource = ImportSource.FileSource.CTMS,
                DataGridToPopulate = "ComparerData1",
                ShortDescription = "CTMS",
                ImportedData = ctmsDataTable.DefaultView
            };

            var bioPharmDataPackage = new ImportDataPackage
            {
                FileSource = ImportSource.FileSource.BioPharm,
                DataGridToPopulate = "ComparerData2",
                ShortDescription = "BioPharm",
                ImportedData = biopharmDataTable.DefaultView
            };

            var otherFileDataPackage = new ImportDataPackage
            {
                FileSource = ImportSource.FileSource.Other,
                DataGridToPopulate = "ComparerData3",
                ShortDescription = "Other",
                ImportedData = otherFileDataTable.DefaultView
            };

            var dataPackages = new List<ImportDataPackage>();

            if (includeBioPharm)
            {
                dataPackages.Add(bioPharmDataPackage);
            }
            if (includeCTMS)
            {
                dataPackages.Add(ctmsDataPackage);
            }
            if (includeClinWeb)
            {
                dataPackages.Add(clinWebDataPackage);
            }
            if (includeOther)
            {
                dataPackages.Add(otherFileDataPackage);
            }

            return dataPackages;
        }
    }
}
