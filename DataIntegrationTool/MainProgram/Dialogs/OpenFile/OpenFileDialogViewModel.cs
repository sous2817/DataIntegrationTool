using DataIntegrationTool.BindingParameters;
using DataIntegrationTool.MessengerPackages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using DataIntegrationTool.Resources.HelperMethods;

namespace DataIntegrationTool.MainProgram.Dialogs.OpenFile
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class OpenFileDialogViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the OpenFileDialogViewModel class.
        /// </summary>
        public OpenFileDialogViewModel()
        {
            Messenger.Default.Register<ImportButtonParameters>(this,"ImportSource", SetReturnLocationAndType);
        }

        #region Commands
        private RelayCommand _sendCancelMessageCommand;

        public RelayCommand SendCancelMessageCommand
        {
            get
            {
                return _sendCancelMessageCommand
                       ?? (_sendCancelMessageCommand = new RelayCommand(SendCancelMessage));
            }
        }

        private RelayCommand _openFileDialogCommand;

        public RelayCommand OpenFileDialogCommand
        {
            get
            {
                return _openFileDialogCommand
                       ?? (_openFileDialogCommand = new RelayCommand(OpenFileDialog));
            }
        }

        private RelayCommand _sendFilePathMessageCommand;

        public RelayCommand SendFilePathMessageCommand => _sendFilePathMessageCommand
                                                          ?? (_sendFilePathMessageCommand = new RelayCommand(SendFilePathMessage, CanExecuteSendFilePathMessage));

        #endregion

        #region Methods
        private bool CanExecuteSendFilePathMessage()
        {
            return !string.IsNullOrEmpty(FileName);
        }

        private void SetReturnLocationAndType(ImportButtonParameters callingSource)
        {
            _importDataPackage.DataGridToPopulate = callingSource.CallingGroupBoxName;
            _importDataPackage.FileSource = callingSource.SelectedImportOption;
            ShortName = callingSource.SelectedImportOption.ToString();
        }

        private void SendFilePathMessage()
        {
            _importDataPackage.ShortDescription = string.IsNullOrEmpty(ShortName) ? _importDataPackage.FileSource.ToString() : ShortName;
            Messenger.Default.Send(_importDataPackage);
        }

        private void SendCancelMessage()
        {
            Messenger.Default.Send("FileDialog", "Cancel");
        }

        private void OpenFileDialog()
        {
            FileName = OpenAndImportFiles.GetFile();
            _importDataPackage.FileName = FileName;
        }
        #endregion

        #region Properties
        private readonly ImportDataPackage _importDataPackage = new ImportDataPackage();

        private string _fileName;

        public string FileName
        {
            get { return _fileName; }

            set
            {
                if (_fileName == value)
                {
                    return;
                }
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        private string _shortName;

        public string ShortName
        {
            get { return _shortName; }

            set
            {
                if (_shortName == value)
                {
                    return;
                }
                _shortName = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}