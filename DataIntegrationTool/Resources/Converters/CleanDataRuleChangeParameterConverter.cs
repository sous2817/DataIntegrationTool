using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.Resources.BindingParameters;

namespace DataIntegrationTool.Resources.Converters
{
    internal class CleanDataRuleChangeParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            var cleanDataSetRuleParameter = new CleanDataSetRuleParameter
            {
                SelectedCleanDataRule = (CleanDataRule) values[0],
                RuleName = values[1].ToString()
            };

            return cleanDataSetRuleParameter;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
