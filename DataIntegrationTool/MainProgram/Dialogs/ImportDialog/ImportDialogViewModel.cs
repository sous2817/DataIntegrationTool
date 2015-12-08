using System;
using System.Collections.Generic;
using System.Linq;
using DataIntegrationTool.MessengerPackages;
using DataIntegrationTool.Resources.Enums;
using Excel.Log;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Semio.ClientService.Data.Intelligence.Canvas;
using Semio.ClientService.Provide;

namespace DataIntegrationTool.MainProgram.ImportDialog
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ImportDialogViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the ImportDialogViewModel class.
        /// </summary>
        public ImportDialogViewModel()
        {
            Messenger.Default.Register<List<CanvasModelListItem>>(this, ProcessClinWebData);
        }

        private void ProcessClinWebData(List<CanvasModelListItem> clinWebData )
        {
            ClinWebData = clinWebData;
        }

        private RelayCommand _sendCancelMessageCommand;

        public RelayCommand SendCancelMessageCommand
        {
            get
            {
                return _sendCancelMessageCommand
                       ?? (_sendCancelMessageCommand = new RelayCommand(SendCancelMessage));
            }
        }

        private void SendCancelMessage()
        {
            Messenger.Default.Send("ClinWebDialog", "Cancel");
        }


        private RelayCommand _sendGuidMessageCommand;

        public RelayCommand SendGuidMessageCommand
        {
            get
            {
                return _sendGuidMessageCommand
                       ?? (_sendGuidMessageCommand = new RelayCommand(SendMessage));
            }
        }

        private void SendMessage()
        {
            var importDataPackage = new ImportDataPackage
            {
                ClinWebCanvasGuid = SelectedCanvasItem.CanvasModelGuid,
                FileSource = ImportSource.FileSource.ClinWeb,
                DataGridToPopulate = "Base",
                ShortDescription="Infosario Planning"
            };

            Messenger.Default.Send(importDataPackage);
        }


        private CanvasModelListItem _selectedCanvasItem = new CanvasModelListItem();

        public CanvasModelListItem SelectedCanvasItem
        {
            get { return _selectedCanvasItem; }

            set
            {
                if (_selectedCanvasItem == value)
                {
                    return;
                }
                _selectedCanvasItem = value;
                RaisePropertyChanged();
            }
        }

        private List<CanvasModelListItem> _clinWebData;

        public List<CanvasModelListItem> ClinWebData
        {
            get { return _clinWebData; }

            set
            {
                if (_clinWebData == value)
                {
                    return;
                }
                _clinWebData = value;
                RaisePropertyChanged();
            }
        }
    }
}