using System;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight.CommandWpf;

namespace DataIntegrationTool.MainProgram.ExportData
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ExportDataViewModel : DataIntegrationViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the ExportDataViewModel class.
        /// </summary>
        public ExportDataViewModel()
        {
        }

        private string _buttonValue;

        public string ButtonValue
        {
            get { return _buttonValue; }

            set
            {
                if (_buttonValue == value)
                {
                    return;
                }
                _buttonValue = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _buttonPushedCommand;

        public RelayCommand ButtonPushedCommand => _buttonPushedCommand
                                                   ?? (_buttonPushedCommand = new RelayCommand(ButtonPushed));

        private void ButtonPushed()
        {
            ButtonValue = $"{"Foo"}: {DateTime.Now.ToString("h:mm:ss")}";
        }

        public override WizardSteps.LocatorNames LocatorName => WizardSteps.LocatorNames.ExportDate;

    }
}