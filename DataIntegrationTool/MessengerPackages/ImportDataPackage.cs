using System;
using System.Data;
using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight;

namespace DataIntegrationTool.MessengerPackages
{
    public class ImportDataPackage : ViewModelBase
    {
        public ImportSource.FileSource FileSource { get; set; }
        public string FileName { get; set; }
        public string DataGridToPopulate { get; set; }
        public Guid ClinWebCanvasGuid { get; set; }

        private DataView _importedData;

        public DataView ImportedData
        {
            get { return _importedData; }

            set
            {
                if (_importedData == value)
                {
                    return;
                }
                _importedData = value;
                RaisePropertyChanged();
            }
        }

        private string _shortDescription;

        public string ShortDescription
        {
            get { return _shortDescription; }

            set
            {
                if (_shortDescription == value)
                {
                    return;
                }
                _shortDescription = value;
                RaisePropertyChanged();
            }
        }
    }
}
