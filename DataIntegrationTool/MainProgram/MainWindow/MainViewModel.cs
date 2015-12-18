using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.MainProgram.CleanData;
using DataIntegrationTool.MainProgram.EvaluateMatches;
using DataIntegrationTool.MainProgram.ExportData;
using DataIntegrationTool.MainProgram.ImportData;
using DataIntegrationTool.MainProgram.Welcome;
using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;

namespace DataIntegrationTool.MainProgram.MainWindow
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDialogCoordinator dialogCoordinator)
        {
            CurrentViewModel = ServiceLocator.Current.GetInstance<WelcomeViewModel>();
            AccentColors = ThemeManager.Accents
                                            .Select(a => new ThemeAndAccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                            .ToList();

            AppThemes = ThemeManager.AppThemes
                                           .Select(a => new ThemeAndAccentColorMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();

            SelectedThemeIndex = Properties.Settings.Default.themeIndex;
            SelectedAccentIndex = Properties.Settings.Default.accentIndex;
            ChangeTheme(AppThemes[SelectedThemeIndex].Name);
            ChangeAccent(AccentColors[SelectedAccentIndex].Name);

            _dialogCoordinator = dialogCoordinator;

            CurrentVersion = MainWindowBLL.GetCurrentVerision();
            CurrentEnvironment = MainWindowBLL.GetCurrentEnvironment();
        }
        
        #region Commands
        private RelayCommand _exitProgramCommand;
        public RelayCommand ExitProgramCommand => _exitProgramCommand
                                                  ?? (_exitProgramCommand = new RelayCommand(ExitProgram));

        private RelayCommand<string> _changeThemeCommand;

        public RelayCommand<string> ChangeThemeCommand => _changeThemeCommand
                                                          ?? (_changeThemeCommand = new RelayCommand<string>(ChangeTheme));

        private RelayCommand _moveForwardCommand;

        public RelayCommand MoveForwardCommand => _moveForwardCommand
                                                  ?? (_moveForwardCommand = new RelayCommand(MoveForward, CanExecuteMoveForward));

        private RelayCommand _moveBackCommand;

        public RelayCommand MoveBackCommand => _moveBackCommand
                                               ?? (_moveBackCommand = new RelayCommand(MoveBack, CanExecuteMoveBack));

        private RelayCommand<string> _changeAccentCommand;

        public RelayCommand<string> ChangeAccentCommand => _changeAccentCommand
                                                           ?? (_changeAccentCommand = new RelayCommand<string>(ChangeAccent));

        #endregion

        #region Methods
        private async void ExitProgram()
        {

            var confirmExit = await _dialogCoordinator.ShowMessageAsync(this, "Exit Program", "Are you sure you want to close the Data Integration Tool?", MessageDialogStyle.AffirmativeAndNegative);
            if (confirmExit == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }
        }

        private bool CanExecuteMoveForward() => CurrentViewModel.LocatorName != WizardSteps.LocatorNames.ExportDate;

        private void MoveForward()
        {
            

            DataIntegrationViewModelBase nextStep;
            switch (CurrentViewModel.LocatorName)
            {
                case WizardSteps.LocatorNames.Welcome:
                    nextStep = ServiceLocator.Current.GetInstance<ImportDataViewModel>();
                    break;
                case WizardSteps.LocatorNames.ImportData:
                    nextStep = ServiceLocator.Current.GetInstance<CleanDataViewModel>();
                    break;
                case WizardSteps.LocatorNames.CleanData:
                    nextStep = ServiceLocator.Current.GetInstance<EvaluateMatchesViewModel>();
                    break;
                case WizardSteps.LocatorNames.EvaluateMatches:
                    nextStep = ServiceLocator.Current.GetInstance<ExportDataViewModel>();
                    break;
                default:
                    nextStep = ServiceLocator.Current.GetInstance<WelcomeViewModel>();
                    break;
            }
            MainWindowBLL.NextStepPackage(CurrentViewModel);
            WizardIndex = (int)CurrentViewModel.LocatorName +1;
            CurrentViewModel = nextStep; 
        }

        private bool CanExecuteMoveBack() => CurrentViewModel.LocatorName != WizardSteps.LocatorNames.Welcome;

        private void MoveBack()
        {
            DataIntegrationViewModelBase previousStep;

            switch (CurrentViewModel.LocatorName)
            {
                case WizardSteps.LocatorNames.ImportData:
                    previousStep = ServiceLocator.Current.GetInstance<WelcomeViewModel>();
                    break;
                case WizardSteps.LocatorNames.CleanData:
                    previousStep = ServiceLocator.Current.GetInstance<ImportDataViewModel>();
                    break;
                case WizardSteps.LocatorNames.EvaluateMatches:
                    previousStep = ServiceLocator.Current.GetInstance<CleanDataViewModel>();
                    break;
                case WizardSteps.LocatorNames.ExportDate:
                    previousStep = ServiceLocator.Current.GetInstance<EvaluateMatchesViewModel>();
                    break;
                default:
                    previousStep = ServiceLocator.Current.GetInstance<ExportDataViewModel>();
                    break;
            }
            WizardIndex = (int)CurrentViewModel.LocatorName -1;
            CurrentViewModel = previousStep;
        }

        private void ChangeTheme(string themeName)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(themeName);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
            Properties.Settings.Default.themeIndex = AppThemes.FindIndex(n => n.Name == themeName);
            Properties.Settings.Default.Save();
        }

        private void ChangeAccent(string accentColor)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(accentColor);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
            Properties.Settings.Default.accentIndex = AccentColors.FindIndex(n => n.Name == accentColor);
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Properties
        public List<ThemeAndAccentColorMenuData> AccentColors { get; set; }
        public string CurrentEnvironment { get; }
        public string CurrentVersion { get; }
        public List<ThemeAndAccentColorMenuData> AppThemes { get; set; }
        private readonly IDialogCoordinator _dialogCoordinator;

        private DataIntegrationViewModelBase _currentViewModel;

        public DataIntegrationViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }

            set
            {
                if (_currentViewModel == value)
                {
                    return;
                }
                _currentViewModel = value;
                RaisePropertyChanged();
            }
        }

        private int _wizardIndex;

        public int WizardIndex
        {
            get { return _wizardIndex; }

            set
            {
                if (_wizardIndex == value)
                {
                    return;
                }
                _wizardIndex = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedThemeIndex;

        public int SelectedThemeIndex
        {
            get { return _selectedThemeIndex; }

            set
            {
                if (_selectedThemeIndex == value)
                {
                    return;
                }
                _selectedThemeIndex = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedAccentIndex;

        public int SelectedAccentIndex
        {
            get { return _selectedAccentIndex; }

            set
            {
                if (_selectedAccentIndex == value)
                {
                    return;
                }
                _selectedAccentIndex = value;
                RaisePropertyChanged();
            }
        }

        

        #endregion
    }
}