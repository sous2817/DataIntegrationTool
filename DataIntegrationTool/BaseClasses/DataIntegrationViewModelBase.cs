using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight;

namespace DataIntegrationTool.BaseClasses
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public abstract class DataIntegrationViewModelBase : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the DataIntegrationViewModelBase class.
        /// </summary>
        protected DataIntegrationViewModelBase()
        {
        }

        public abstract WizardSteps.LocatorNames LocatorName { get; }
    }
}