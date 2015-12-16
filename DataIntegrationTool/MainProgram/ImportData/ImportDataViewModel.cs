using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DataIntegrationTool.MainProgram.ImportDialog;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using Semio.ClientService.Data.Intelligence.Canvas;
using System.Threading.Tasks;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.BindingParameters;
using DataIntegrationTool.MainProgram.Dialogs.ExceptionDialog;
using DataIntegrationTool.MainProgram.Dialogs.OpenFile;
using DataIntegrationTool.MessengerPackages;
using DataIntegrationTool.Resources.Enums;


namespace DataIntegrationTool.MainProgram.ImportData
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ImportDataViewModel : DataIntegrationViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the ImportDataViewModel class.
        /// </summary>
        public ImportDataViewModel(IDialogCoordinator dialogCoordinator)
        {
            BaseImportOptions=new List<string>{"Infosario Planning...", "CTMS...", "BioPharm...", "File..." };
            ComparerImportOptions = new List<string> {"CTMS...", "BioPharm...", "File..."};
            _dialogCoordinator = dialogCoordinator;
            Messenger.Default.Register<ImportDataPackage>(this, ProcessDataImport);
            Messenger.Default.Register<string>(this,"Cancel",CancelDialog);
        }
        #region Commands

        private RelayCommand<ImportButtonParameters> _importDataCommand;

        public RelayCommand<ImportButtonParameters> ImportDataCommand => _importDataCommand
                                                                         ?? (_importDataCommand = new RelayCommand<ImportButtonParameters>(ImportData));

        private RelayCommand<string> _clearDataCommand;

        public RelayCommand<string> ClearDataCommand => _clearDataCommand
                                                        ?? (_clearDataCommand = new RelayCommand<string>(ClearData, CanExecuteClearData));

        #endregion

        #region Methods 

        private bool CanExecuteClearData(string groupBoxName)
        {
            switch (groupBoxName)
            {
                case "BaseData":
                    return BaseData.ImportedData!= null;
                case "ComparerData1":
                    return ComparerData1.ImportedData != null;
                case "ComparerData2":
                    return ComparerData2.ImportedData != null;
                case "ComparerData3":
                    return ComparerData3.ImportedData != null;
                default:
                    return true;
            }
        }

        private void ClearData(string groupBoxName)
        {
            switch (groupBoxName)
            {
                case "BaseData":
                    BaseData.ShortDescription = "BASE DATA";
                    BaseData.ImportedData = null;
                    break;
                case "ComparerData1":
                    ComparerData1.ShortDescription = "COMPARER DATA 1";
                    ComparerData1.ImportedData = null;
                    break;
                case "ComparerData2":
                    ComparerData2.ShortDescription = "COMPARER DATA 2";
                    ComparerData2.ImportedData = null;
                    break;
                case "ComparerData3":
                    ComparerData3.ShortDescription = "COMPARER DATA 3";
                    ComparerData3.ImportedData = null;
                    break;
            }
        }
        private async void CancelDialog(string closingSource)
        {
            switch (closingSource)
            {
                case "ClinWebDialog":
                    await _dialogCoordinator.HideMetroDialogAsync(this, ImportDialog);
                    break;
                case "FileDialog":
                    await _dialogCoordinator.HideMetroDialogAsync(this, OpenFileDialog);
                    break;
            }
        }

        private async void ProcessDataImport(ImportDataPackage importedData)
        {
            var progressDialog = await _dialogCoordinator.ShowProgressAsync(this, "Fetching Data...", string.Empty);
            progressDialog.SetIndeterminate();
            try
            {
                if (importedData.ClinWebCanvasGuid != Guid.Empty)
                {
                    _canvasGuid = importedData.ClinWebCanvasGuid;
                    await _dialogCoordinator.HideMetroDialogAsync(this, ImportDialog);

                    var facilityInvListDataForACanvas =
                        await ImportDataBLL.GetInvestigationalPerformanceCollection(_canvasGuid);

                    BaseData = importedData;
                    BaseData.ImportedData =
                        await Task.Run(() => ImportDataBLL.ConvertClinWebDataToDataView(facilityInvListDataForACanvas));
                    await progressDialog.CloseAsync();
                }
                else
                {
                    await _dialogCoordinator.HideMetroDialogAsync(this, OpenFileDialog);

                    var importedFileDataTable = await Task.Run(() => ImportDataBLL.GetFileData(importedData.FileName));

                    var missingMandatoryColumns =
                        await
                            Task.Run(
                                () =>
                                    ImportDataBLL.MissingMandatoryColumnList(importedData.FileSource,
                                        importedFileDataTable));

                    if (missingMandatoryColumns.Any())
                    {
                        var errorMessage =
                            $"There are certain columns that are expected to be in the uploaded file.  The following columns are missing: {Environment.NewLine}{string.Join(Environment.NewLine, missingMandatoryColumns)}";
                        await progressDialog.CloseAsync();
                        await _dialogCoordinator.ShowMessageAsync(this, "Error in file", errorMessage);
                        return;
                    }

                    switch (importedData.GroupBoxName)
                    {
                        case GroupBoxToPopulate.Name.BaseData:
                            BaseData = importedData;
                            BaseData.ImportedData = importedFileDataTable.DefaultView;
                            break;
                        case GroupBoxToPopulate.Name.ComparerData1:
                            ComparerData1 = importedData;
                            ComparerData1.ImportedData = importedFileDataTable.DefaultView;
                            break;
                        case GroupBoxToPopulate.Name.ComparerData2:
                            ComparerData2 = importedData;
                            ComparerData2.ImportedData = importedFileDataTable.DefaultView;
                            break;
                        case GroupBoxToPopulate.Name.ComparerData3:
                            ComparerData3 = importedData;
                            ComparerData3.ImportedData = importedFileDataTable.DefaultView;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    await progressDialog.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                await _dialogCoordinator.HideMetroDialogAsync(this, ImportDialog);
                var ev = new ExceptionDialogView("An unexpected error occurred in the application.", ex);
                ev.ShowDialog();
                await progressDialog.CloseAsync();
            }
        }

        private async void ImportData(ImportButtonParameters importSource)
        {
            switch (importSource.SelectedImportOption)
            {
                case ImportSource.FileSource.ClinWeb:
                    var progressDialog = await _dialogCoordinator.ShowProgressAsync(this, "Fetching ClinWeb Canvas Information...", string.Empty);
                    progressDialog.SetIndeterminate();

                    ClinWebData = await ImportDataBLL.ImportClinWeb();

                    await _dialogCoordinator.ShowMetroDialogAsync(this, ImportDialog);

                    Messenger.Default.Send(ClinWebData);

                    await progressDialog.CloseAsync();

                    break;
                case ImportSource.FileSource.BioPharm:
                case ImportSource.FileSource.CTMS:
                case ImportSource.FileSource.Other:
                    ImportFile();
                    Messenger.Default.Send(importSource, "ImportSource");
                    break;
                default:
                    ImportFile();
                    Messenger.Default.Send(importSource, "ImportSource");
                    break;
            }
        }

        private async void ImportFile()
        {
            OpenFileDialog.DataContext = new OpenFileDialogViewModel();
            await _dialogCoordinator.ShowMetroDialogAsync(this, OpenFileDialog);
        }

        #endregion

        #region Properties

        private ImportDialogView _importDialog;

        private ImportDialogView ImportDialog => _importDialog ?? (_importDialog = new ImportDialogView());

        private OpenFileDialogView _openFileDialog;
        
        private OpenFileDialogView OpenFileDialog => _openFileDialog ?? (_openFileDialog = new OpenFileDialogView());

        private readonly IDialogCoordinator _dialogCoordinator;
        private Guid _canvasGuid;
              
        public List<string> BaseImportOptions { get; set; }
        public List<string> ComparerImportOptions { get; set; }

        private ImportDataPackage _baseData = new ImportDataPackage { ShortDescription = "BASE DATA" };

        public ImportDataPackage BaseData
        {
            get { return _baseData; }

            set
            {
                if (_baseData == value)
                {
                    return;
                }
                _baseData = value;
                RaisePropertyChanged();
            }
        }

        private ImportDataPackage _comparerData1 = new ImportDataPackage { ShortDescription = "COMPARER DATA 1" };

        public ImportDataPackage ComparerData1
        {
            get { return _comparerData1; }

            set
            {
                if (_comparerData1 == value)
                {
                    return;
                }
                _comparerData1 = value;
                RaisePropertyChanged();
            }
        }

        private ImportDataPackage _comparerData2 = new ImportDataPackage { ShortDescription = "COMPARER DATA 2" };

        public ImportDataPackage ComparerData2
        {
            get { return _comparerData2; }

            set
            {
                if (_comparerData2 == value)
                {
                    return;
                }
                _comparerData2 = value;
                RaisePropertyChanged();
            }
        }

        private ImportDataPackage _comparerData3 = new ImportDataPackage { ShortDescription = "COMPARER DATA 3"};

        public ImportDataPackage ComparerData3
        {
            get { return _comparerData3; }

            set
            {
                if (_comparerData3 == value)
                {
                    return;
                }
                _comparerData3 = value;
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

        public override WizardSteps.LocatorNames LocatorName => WizardSteps.LocatorNames.ImportData;

        #endregion
    }
}