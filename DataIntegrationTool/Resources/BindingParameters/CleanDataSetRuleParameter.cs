using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIntegrationTool.BaseClasses;

namespace DataIntegrationTool.Resources.BindingParameters
{
    public class CleanDataSetRuleParameter
    {
        public CleanDataRule SelectedCleanDataRule { get; set; }
        public string RuleName { get; set; }
    }
}
