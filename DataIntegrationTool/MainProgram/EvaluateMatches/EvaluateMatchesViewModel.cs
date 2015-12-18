using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.Resources.Enums;

namespace DataIntegrationTool.MainProgram.EvaluateMatches
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class EvaluateMatchesViewModel : DataIntegrationViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the EvaluateMatchesViewModel class.
        /// </summary>
        public EvaluateMatchesViewModel()
        {
        }

        #region Properties
        public override WizardSteps.LocatorNames LocatorName => WizardSteps.LocatorNames.EvaluateMatches;
        #endregion
    }
}