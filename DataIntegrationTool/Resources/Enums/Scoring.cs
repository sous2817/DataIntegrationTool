using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegrationTool.Resources.Enums
{
    public static class Scoring
    {
        public enum ClinWeb
        {
            Ascending,
            Descending,
            Visualization
        }

        public enum NumericRules
        {
            Count,
            Max,
            Mean,
            Median,
            Min,
            Sum
        }
    }
}
