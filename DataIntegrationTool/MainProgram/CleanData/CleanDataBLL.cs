using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIntegrationTool.BaseClasses;

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
    }
}
