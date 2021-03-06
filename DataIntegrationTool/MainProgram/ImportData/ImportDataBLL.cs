﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.MainProgram.Dialogs.ExceptionDialog;
using DataIntegrationTool.Resources.Enums;
using FastMember;
using Semio.ClientService.Data.Intelligence.Canvas;
using Semio.ClientService.Data.Intelligence.Investigator;
using Semio.ClientService.Provide;
using DataIntegrationTool.Resources.HelperMethods;

namespace DataIntegrationTool.MainProgram.ImportData
{
    public static class ImportDataBLL
    {
        public static async Task<List<CanvasModelListItem>> ImportClinWeb()
        {
            var clinWebData = new List<CanvasModelListItem>();

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => true;
            try
            {
                clinWebData = await Task.Run(() => ProvideClient.Default.GetDataMineCanvasNameListWithMetadata());

                foreach (var item in clinWebData.Where(item => item.IsLocked == null))
                {
                    item.IsLocked = true;
                }

                return clinWebData;
            }
            catch (Exception ex)
            {
                var ev = new ExceptionDialogView("An unexpected error occurred in the application.", ex);
                ev.Show();
                return clinWebData;
            }
        }

        public static async Task<InvestigationalPerformanceCollection> GetInvestigationalPerformanceCollection(Guid canvasGuid) => await Task.Run(() => ProvideClient.Default.GetFacilityInvestigatorData(canvasGuid.ToString()));

        public static DataView ConvertClinWebDataToDataView(InvestigationalPerformanceCollection clinwebCollection)
        {
            var sourceDataTable = new DataTable();
            var listData = clinwebCollection.Items.Select(item => new ClinWebDataMiningStructure
            {
                ResearchFacilityRegion = item.FacilityEntityLocation.Region.RegionName,
                ResearchFacilityCountry = item.Facility.Location.Address.Country.Name,
                ResearchFacilityState = item.Facility.Location.Address.StateOrProvince,
                ResearchFacilityCity = item.Facility.Location.Address.City,
                ResearchFacilityAddressLine1 = item.Facility.Location.Address.Line1,
                ResearchFacilityAddressLine2 = item.Facility.Location.Address.Line2,
                ResearchFacilityAddressLine3 = item.Facility.Location.Address.Line3,
                ResearchFacilityDepartment = item.Facility.Department,
                ResearchFacilityName = item.Facility.OrganizationName,
                ResearchFacilityID = item.Facility.Id,
                ResearchFacilityCountryISO2 = item.Facility.Location.Address.Country.IsoCode,
                ResearchFacilityCountryISO3 = item.Facility.Location.Address.Country.Iso3Code,
                PIRegion = item.Investigator.Location.Region.RegionName,
                PICountry = item.Investigator.Location.Address.Country.Name,
                PICountryISO2 = item.Investigator.Location.Address.Country.IsoCode,
                PICountryISO3 = item.Investigator.Location.Address.Country.Iso3Code,
                PIState = item.Investigator.Location.Address.StateOrProvince,
                PICity = item.Investigator.Location.Address.City,
                PIAddressLine1 = item.Investigator.Location.Address.Line1,
                PIAddressLine2 = item.Investigator.Location.Address.Line2,
                PIAddressLine3 = item.Investigator.Location.Address.Line3,
                PIFirstName = item.Investigator.FirstName,
                PIMiddleName = item.Investigator.MiddleName,
                PILastName = item.Investigator.LastName,
                PIEmailAddress = item.Investigator.Email,
                PIExternalInvID = item.Id,
                PIPhoneNumber = item.Investigator.PhoneNumber,
                InvestigatorContactStatus = item.InvestigatorContactStatus,
                InvestigatorAllSpecialties = item.CtmsExperience.AllSpecialties,
                InvestigatorIsClinical = item.InvestigatorIsClinical,
                InvestigatorIsResearch = item.InvestigatorIsResearch,
                CenterOfExcellence = item.CtmsExperience.CenterOfExcellence,
                ProjectCountBioPharm = item.ProjectTotalCountBioPharm,
                ProjectTotalCountBioPharm = item.ProjectCountBioPharm,
                ProjectCountRecruitingBioPharm = item.ProjectCountRecruitingBioPharm,
                ProjectCountCompletedBioPharm = item.ProjectCountCompletedBioPharm,
                ResearchFacilityAccountName2 = item.Facility.AdditionalName
            }).ToList();

            var emptyFacilityIDCounter = 1;

            foreach (var item in listData.Where(x => x.ResearchFacilityID == string.Empty))
            {
                item.ResearchFacilityID = $"IPD_Orphan_{emptyFacilityIDCounter}";
                item.ResearchFacilityName = $"IPD_Orphan_{emptyFacilityIDCounter}";
                emptyFacilityIDCounter++;
            }

            // Converts a list to a data table (the structure that's being used in the first step)
            using (var reader = ObjectReader.Create(listData))
            {
                sourceDataTable.Load(reader);
            }

            return sourceDataTable.DefaultView;
        }

        public static async Task<DataTable> GetFileData(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            var fileData = ext == ".csv" ? await Task.Run(() => OpenAndImportFiles.ImportCSV(fileName)) :
                                                await Task.Run(() => OpenAndImportFiles.ImportExcel(fileName));

            foreach (DataColumn dataColumn in fileData.Table.Columns)
            {
                dataColumn.ColumnName = dataColumn.ColumnName.Replace("/", "_");
            }

            return fileData.Table;
        }

        public static List<string> MissingMandatoryColumnList(ImportSource.FileSource fileSource,DataTable fileDataTable)
        {
            var submittedColumnList =
                        (from DataColumn dc in fileDataTable.Columns select dc.ToString()).ToList();
            var mandatoryColumnList = new List<string>();

            switch (fileSource)
            {
                case ImportSource.FileSource.CTMS:
                    mandatoryColumnList = GenerateMandatoryCTMSManualColumnsList();
                    break;
                case ImportSource.FileSource.BioPharm:
                    mandatoryColumnList = GenerateMandatoryBioPharmColumnList();
                    break;
            }

            return mandatoryColumnList.Where(mandatory => submittedColumnList.Count(submitted => submitted == mandatory) != 1).ToList();
        }

        private static List<string> GenerateMandatoryBioPharmColumnList() => new List<string>
            {
                "BP-Name",
                "BP-Address",
                "BP-City",
                "BP-State",
                "BP-Country",
                "BP-Zip",
                "BP-Phone",
                "BP-Email",
                "BP-Contact",
                "BP-# of Studies",
                "BP-# of Studies by Criteria",
                "BP-# of Studies by Criteria Recruiting",
                "BP-# of Studies by Criteria Completed",
                "BP-Principal Investigators at Site",
                "BP-Site ID"
            };

        private static List<string> GenerateMandatoryCTMSManualColumnsList() => new List<string>
            {
                "Contact Last Name",
                "Contact First Name",
                "Contact Status",
                "Contact Email",
                "Primary Account",
                "Account Name",
                "Account Name 2",
                "Account Status",
                "Partner Status",
                "Account Email",
                "Account All Experience",
                "Primary Account Exp Category",
                "Account Clinical",
                "Account Research",
                "Account CoE",
                "QA Status",
                "City",
                "State_Province",
                "Country",
                "Sub-Region",
                "Region",
                "Department Name",
                "All Specialties",
                "Specialty Category",
                "Board Certified",
                "Primary Specialty",
                "Investigator All Experience",
                "Primary Investigator Exp Category",
                "Investigator Clinical",
                "Investigator Research",
                "Investigator CoE",
                "Ethics Type",
                "Ultimate Parent",
                "Relationship Manager",
                "Account Row Id",
                "Contact Row Id"
            };

    }
}
