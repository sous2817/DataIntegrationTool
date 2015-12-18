/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:DataIntegrationTool.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using DataIntegrationTool.MainProgram.CleanData;
using DataIntegrationTool.MainProgram.Dialogs.OpenFile;
using DataIntegrationTool.MainProgram.EvaluateMatches;
using DataIntegrationTool.MainProgram.ExportData;
using DataIntegrationTool.MainProgram.ImportData;
using DataIntegrationTool.MainProgram.ImportDialog;
using DataIntegrationTool.MainProgram.MainWindow;
using DataIntegrationTool.MainProgram.Rules.StringRule;
using DataIntegrationTool.MainProgram.Welcome;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;

namespace DataIntegrationTool.MainProgram
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {

        //public static readonly List<DataIntegrationViewModelBase> WizardStepsList = new List<DataIntegrationViewModelBase>
        //{
        //    new WelcomeViewModel(),
        //    new ImportDataViewModel(new DialogCoordinator()),
        //    new CleanDataViewModel(),
        //    new ExportDataViewModel()
        //};

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDialogCoordinator, DialogCoordinator>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WelcomeViewModel>();
            SimpleIoc.Default.Register<ImportDataViewModel>();
            SimpleIoc.Default.Register<ImportDialogViewModel>();
            SimpleIoc.Default.Register<OpenFileDialogViewModel>();
            SimpleIoc.Default.Register<CleanDataViewModel>();
            SimpleIoc.Default.Register<EvaluateMatchesViewModel>();
            SimpleIoc.Default.Register<ExportDataViewModel>();
            SimpleIoc.Default.Register<StringRuleViewModel>();
        }
    
        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non- member is needed for data binding purposes.")]
        public  MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public StringRuleViewModel StringRule => ServiceLocator.Current.GetInstance<StringRuleViewModel>();

        public ImportDataViewModel ImportData => ServiceLocator.Current.GetInstance<ImportDataViewModel>();

        public OpenFileDialogViewModel OpenFileDialog => ServiceLocator.Current.GetInstance<OpenFileDialogViewModel>();

        public WelcomeViewModel Welcome => ServiceLocator.Current.GetInstance<WelcomeViewModel>();

        public  ImportDialogViewModel ImportDialog => ServiceLocator.Current.GetInstance<ImportDialogViewModel>();

        public CleanDataViewModel CleanData => ServiceLocator.Current.GetInstance<CleanDataViewModel>();

        public  EvaluateMatchesViewModel EvaluateMatches => ServiceLocator.Current.GetInstance<EvaluateMatchesViewModel>();

        public  ExportDataViewModel ExportData => ServiceLocator.Current.GetInstance<ExportDataViewModel>();


        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
         
        }
    }
}