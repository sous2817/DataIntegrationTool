using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using DataIntegrationTool.BaseClasses;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;

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
            WizardSteps = MainWindowBLL.GetWizardSteps();
            CurrentViewModel = WizardSteps[WizardIndex];
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
        public RelayCommand ExitProgramCommand
        {
            get
            {
                return _exitProgramCommand
                       ?? (_exitProgramCommand = new RelayCommand(ExitProgram));
            }
        }

        private RelayCommand<string> _changeThemeCommand;

        public RelayCommand<string> ChangeThemeCommand
        {
            get
            {
                return _changeThemeCommand
                       ?? (_changeThemeCommand = new RelayCommand<string>(ChangeTheme));
            }
        }

        private RelayCommand _moveForwardCommand;

        public RelayCommand MoveForwardCommand
        {
            get
            {
                return _moveForwardCommand
                       ?? (_moveForwardCommand = new RelayCommand(MoveForward, CanExecuteMoveForward));
            }
        }

        private RelayCommand _moveBackCommand;

        public RelayCommand MoveBackCommand
        {
            get
            {
                return _moveBackCommand
                       ?? (_moveBackCommand = new RelayCommand(MoveBack, CanExecuteMoveBack));
            }
        }

        private RelayCommand<string> _changeAccentCommand;

        public RelayCommand<string> ChangeAccentCommand
        {
            get
            {
                return _changeAccentCommand
                       ?? (_changeAccentCommand = new RelayCommand<string>(ChangeAccent));
            }
        }
        
        #endregion

        #region Methods
        private async void ExitProgram()
        {

            var foo = await _dialogCoordinator.ShowMessageAsync(this, "Exit Program", "Are you sure you want to close the Data Integration Tool?", MessageDialogStyle.AffirmativeAndNegative);
            if (foo == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }
        }

        private bool CanExecuteMoveForward()
        {
            return WizardIndex != WizardSteps.Count - 1;
        }


        private void MoveForward()
        {
            WizardIndex += 1;
            CurrentViewModel = WizardSteps[WizardIndex];
        }

        private bool CanExecuteMoveBack()
        {
            return WizardIndex > 0;
        }

        private void MoveBack()
        {
            WizardIndex -= 1;
            CurrentViewModel = WizardSteps[WizardIndex];
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
        private static ReadOnlyCollection<ViewModelBase> WizardSteps { get; set; }
        private readonly IDialogCoordinator _dialogCoordinator;

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
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