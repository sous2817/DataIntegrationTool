using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.MessengerPackages;
using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace DataIntegrationTool.MainProgram.CleanData
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CleanDataViewModel : DataIntegrationViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the CleanDataViewModel class.
        /// </summary>
        public CleanDataViewModel()
        {
            Messenger.Default.Register<List<ImportDataPackage>>(this, "CleanData", ProcessIncomingData);
        }

        #region Commands
        #endregion

        #region Methods

        private void ProcessIncomingData(List<ImportDataPackage> importedData)
        {
            foreach (var dataSource in importedData.Where(dataSource => dataSource.ImportedData != null))
            {
                Debug.Print(dataSource.ShortDescription);
            }
        }

        #endregion


        #region Properites
        public override WizardSteps.LocatorNames LocatorName => WizardSteps.LocatorNames.CleanData;

        #endregion
    }
}