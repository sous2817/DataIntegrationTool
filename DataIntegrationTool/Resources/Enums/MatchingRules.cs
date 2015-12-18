using System.ComponentModel;

namespace DataIntegrationTool.Resources.Enums
{
    public class MatchingRules
    {
        public enum Name
        {
            [Description("This rule applies a hierarchical order")]
            List,
            [Description("This rule applies numeric operations")]
            Numeric,
            [Description("This rule concatenates strings")]
            String,
            [Description("This rule allows grouping in 3 buckets")]
            Trinary
        }
    }
}
