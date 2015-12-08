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
using DataIntegrationTool.MainProgram.Welcome;
using GalaSoft.MvvmLight;
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
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ImportDataViewModel ImportData
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ImportDataViewModel>();
            }
        }

        public OpenFileDialogViewModel OpenFileDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OpenFileDialogViewModel>();
            }
        }

        public WelcomeViewModel Welcome
        {
            get 
            { return ServiceLocator.Current.GetInstance<WelcomeViewModel>(); }
        }
        public ImportDialogViewModel ImportDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ImportDialogViewModel>();
            }
        }

        public CleanDataViewModel CleanData
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CleanDataViewModel>();
            }
        }

        public EvaluateMatchesViewModel EvaluateMatches
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EvaluateMatchesViewModel>();
            }
        }

        public ExportDataViewModel ExportData
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExportDataViewModel>();
            }
        }

        

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}