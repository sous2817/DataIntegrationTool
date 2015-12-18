using System;
using System.Collections.Generic;
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
                FileSource = ImportSource.FileSource.ClinWeb,
                GroupBoxName = GroupBoxToPopulate.Name.BaseData,
                ShortDescription = "Infosario Planning",
                ImportedData = clinWebDataTable
            };

            var ctmsDataPackage = new ImportDataPackage
            {
                FileSource = ImportSource.FileSource.CTMS,
                GroupBoxName = GroupBoxToPopulate.Name.ComparerData1,
                ShortDescription = "CTMS",
                ImportedData = ctmsDataTable.DefaultView
            };

            var bioPharmDataPackage = new ImportDataPackage
            {
                FileSource = ImportSource.FileSource.BioPharm,
                GroupBoxName = GroupBoxToPopulate.Name.ComparerData2,
                ShortDescription = "BioPharm",
                ImportedData = biopharmDataTable.DefaultView
            };

            var otherFileDataPackage = new ImportDataPackage
            {
                FileSource = ImportSource.FileSource.Other,
                GroupBoxName = GroupBoxToPopulate.Name.ComparerData3,
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
