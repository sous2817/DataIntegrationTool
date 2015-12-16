using System;
using System.Collections.Generic;
using DataIntegrationTool.MessengerPackages;
using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Semio.ClientService.Data.Intelligence.Canvas;

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

        public RelayCommand SendCancelMessageCommand => _sendCancelMessageCommand
                                                        ?? (_sendCancelMessageCommand = new RelayCommand(SendCancelMessage));

        private void SendCancelMessage()
        {
            Messenger.Default.Send("ClinWebDialog", "Cancel");
        }

        private RelayCommand _sendMessageCommand;

        public RelayCommand SendMessageCommand => _sendMessageCommand
                                                  ?? (_sendMessageCommand = new RelayCommand(SendMessage, CanExecuteSendMessage));

        private bool CanExecuteSendMessage() => SelectedCanvasItem.CanvasModelGuid != Guid.Empty;

        private void SendMessage()
        {
            var importDataPackage = new ImportDataPackage
            {
                ClinWebCanvasGuid = SelectedCanvasItem.CanvasModelGuid,
                FileSource = ImportSource.FileSource.ClinWeb,
                GroupBoxName = GroupBoxToPopulate.Name.BaseData,
                ShortDescription="Infosario Planning"
            };

            Messenger.Default.Send(importDataPackage);
        }


        private CanvasModelListItem _selectedCanvasItem;

        public CanvasModelListItem SelectedCanvasItem
        {
            get { return _selectedCanvasItem ?? (_selectedCanvasItem = new CanvasModelListItem()); }

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