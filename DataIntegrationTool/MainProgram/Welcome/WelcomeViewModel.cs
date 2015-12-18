using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.Resources.Enums;

namespace DataIntegrationTool.MainProgram.Welcome
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WelcomeViewModel : DataIntegrationViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the WelcomeViewModel class.
        /// </summary>
        public WelcomeViewModel()
        {
        }

        public override WizardSteps.LocatorNames LocatorName => WizardSteps.LocatorNames.Welcome;
    }
}