using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataIntegrationTool.Resources.Converters
{
    class RuleStringToEnumIndexConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)Enum.Parse(typeof(Enums.MatchingRules.Name), value.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ((Enums.MatchingRules.Name)(int)value).ToString();
    }
}
