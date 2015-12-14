using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DataIntegrationTool.BindingParameters;
using DataIntegrationTool.Resources.Enums;

namespace DataIntegrationTool.Resources.Converters
{
    class ImportButtonParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var importButtonParameters = new ImportButtonParameters
            {
                CallingGroupBoxName = values[0] == null ? string.Empty : values[0].ToString()
            };

            if (values[1] == null)
            {
                values[1] = "Undefined...";
            }

            switch (values[1].ToString())
            {
                case "Infosario Planning...":
                    importButtonParameters.SelectedImportOption = ImportSource.FileSource.ClinWeb;
                    break;
                case "CTMS...":
                     importButtonParameters.SelectedImportOption = ImportSource.FileSource.CTMS;
                    break;
                case "BioPharm...":
                    importButtonParameters.SelectedImportOption = ImportSource.FileSource.BioPharm;
                    break;
                case "File...":
                    importButtonParameters.SelectedImportOption = ImportSource.FileSource.Other;
                    break;
            }
            return importButtonParameters;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
