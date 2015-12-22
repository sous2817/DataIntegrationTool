using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataIntegrationTool.Resources.Converters
{
    class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            var propertyInfo = value.GetType().GetProperty("Count");
            if (propertyInfo != null)
            {
                var count = (int)propertyInfo.GetValue(value, null);
                return count > 0;
            }
            if (!(value is bool)) return true;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
