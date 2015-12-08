using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight;

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
        }

        #region Properites
        public override WizardSteps.LocatorNames LocatorName => WizardSteps.LocatorNames.CleanData;
        #endregion
    }
}