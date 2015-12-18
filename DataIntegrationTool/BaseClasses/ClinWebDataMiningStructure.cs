namespace DataIntegrationTool.BaseClasses
{
    public class ClinWebDataMiningStructure
    {
        //Not sure what these attributes are doing, so taking them out for now.
        //[Core.IPDColumn]
        public string ResearchFacilityName { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityRegion { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityCountry { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityState { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityCity { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityAddressLine1 { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityAddressLine2 { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityAddressLine3 { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityDepartment { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityID { get; set; }
        //[Core.IPDColumn]
        public string PIRegion { get; set; }
        //[Core.IPDColumn]
        public string PICountry { get; set; }
        //[Core.IPDColumn]
        public string PIState { get; set; }
        //[Core.IPDColumn]
        public string PICity { get; set; }
        //[Core.IPDColumn]
        public string PIAddressLine1 { get; set; }
        //[Core.IPDColumn]
        public string PIAddressLine2 { get; set; }
        //[Core.IPDColumn]
        public string PIAddressLine3 { get; set; }
        //[Core.IPDColumn]
        public string PIExternalInvID { get; set; }
        //[Core.IPDColumn]
        public string PIPhoneNumber { get; set; }
        //[Core.IPDColumn]
        public string PIFirstName { get; set; }
        //[Core.IPDColumn]
        public string PIMiddleName { get; set; }
        //[Core.IPDColumn]
        public string PILastName { get; set; }
        //[Core.IPDColumn]
        public string PIEmailAddress { get; set; }
        //[Core.IPDColumn]
        public string PICountryISO2 { get; set; }
        //[Core.IPDColumn]
        public string PICountryISO3 { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityCountryISO2 { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityCountryISO3 { get; set; }
        //[Core.IPDColumn]
        public string ResearchFacilityAccountName2 { get; set; }

        public string InvestigatorContactStatus { get; set; }

        public string InvestigatorAllSpecialties { get; set; }

        public bool? InvestigatorIsClinical { get; set; }

        public bool? InvestigatorIsResearch { get; set; }

        public string CenterOfExcellence { get; set; }

        public int? ProjectCountBioPharm { get; set; }

        public int? ProjectTotalCountBioPharm { get; set; }

        public int? ProjectCountRecruitingBioPharm { get; set; }

        public int? ProjectCountCompletedBioPharm { get; set; }


        //public string PIPartnerStatus { get; set; }
        //public string PIQualityAssuranceStatus { get; set; }
        //public string PIBoardCertified { get; set; }
        //public string PIPrimarySpecialty { get; set; }
        //public string PIExperience { get; set; }
        //public string PIExperienceCategory { get; set; }
        //public string PIKeyOpinionLeader { get; set; }
        //public string ResearchFacilityIRBType { get; set; }
        //public string ResearchFacilityUltimateParentName { get; set; }
        //public string ResearchFacilityRelationshipManagerName { get; set; }
    }
}
