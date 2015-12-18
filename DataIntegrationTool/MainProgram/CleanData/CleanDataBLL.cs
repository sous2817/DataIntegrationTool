using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.Resources.Enums;

namespace DataIntegrationTool.MainProgram.CleanData
{
    public static class CleanDataBLL
    {
        public static ObservableCollection<CleanDataRule> BuildCleanDataRulesStructure(DataView data)
        {
            var cleanDataCollection = new ObservableCollection<CleanDataRule>();

            foreach (var dataColumn in data.Table.Columns)
            {
                cleanDataCollection.Add(new CleanDataRule
                {
                    ColumnName = dataColumn.ToString(),
                    IncludeColumn = true
                });
            }

            return cleanDataCollection;
        }

        public static List<string> GetRulesList()
        {
            var listRules = new List<string>();
            foreach (var rule in Enum.GetValues(typeof (MatchingRules.Name)))
            {
                listRules.Add(rule.ToString());
            }

            return listRules;

        } 
    }
}
