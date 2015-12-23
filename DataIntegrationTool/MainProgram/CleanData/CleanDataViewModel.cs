using System.Collections.Generic;
using System.Linq;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.MainProgram.Rules.StringRule;
using DataIntegrationTool.MessengerPackages;
using DataIntegrationTool.Resources.BindingParameters;
using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight.CommandWpf;
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
            RulesList = CleanDataBLL.GetRulesList();
            Messenger.Default.Register<List<ImportDataPackage>>(this, "CleanData", ProcessIncomingData);
        }

        #region Commands

        private RelayCommand<CleanDataRule> _changeRuleCommand;

        public RelayCommand<CleanDataRule> ChangeRuleCommand => _changeRuleCommand
                                                         ?? (_changeRuleCommand = new RelayCommand<CleanDataRule>(ChangeRule));





        #endregion

        #region Methods

        private void ChangeRule(CleanDataRule cleanData)
        {
            switch (cleanData.Rule)
            {
                case "String":
                    cleanData.RulesViewModel = new StringRuleViewModel();
                    break;
                case "Numeric":
                    //Set rule VM here
                    break;
                case "List":
                    //Set rule VM here
                    break;
                case "Trinary":
                    //Set rule VM here
                    break;
            }
            
        }

        private void ProcessIncomingData(List<ImportDataPackage> importedData)
        {
            foreach (var dataSource in importedData.Where(dataSource => dataSource.ImportedData != null))
            {
                switch (dataSource.GroupBoxName)
                {
                    case GroupBoxToPopulate.Name.BaseData:
                        BaseCleanDataPackage.CleanDataRules = CleanDataBLL.BuildCleanDataRulesStructure(dataSource.ImportedData);
                        BaseCleanDataPackage.ShortDescription = dataSource.ShortDescription;
                        break;
                    case GroupBoxToPopulate.Name.ComparerData1:
                        Comparer1CleanDataPackage.CleanDataRules = CleanDataBLL.BuildCleanDataRulesStructure(dataSource.ImportedData);
                        Comparer1CleanDataPackage.ShortDescription = dataSource.ShortDescription;
                        break;
                    case GroupBoxToPopulate.Name.ComparerData2:
                        Comparer2CleanDataPackage.CleanDataRules = CleanDataBLL.BuildCleanDataRulesStructure(dataSource.ImportedData);
                        Comparer2CleanDataPackage.ShortDescription = dataSource.ShortDescription;
                        break;
                    case GroupBoxToPopulate.Name.ComparerData3:
                        Comparer3CleanDataPackage.CleanDataRules = CleanDataBLL.BuildCleanDataRulesStructure(dataSource.ImportedData);
                        Comparer3CleanDataPackage.ShortDescription = dataSource.ShortDescription;
                        break;
                }
            }
        }

        #endregion


        #region Properites
        public override WizardSteps.LocatorNames LocatorName => WizardSteps.LocatorNames.CleanData;

        public List<string> RulesList { get; set; }

        private CleanDataPackage _baseCleanDataPackage;

        public CleanDataPackage BaseCleanDataPackage
        {
            get { return _baseCleanDataPackage ?? (_baseCleanDataPackage = new CleanDataPackage()); }

            set
            {
                if (_baseCleanDataPackage == value)
                {
                    return;
                }
                _baseCleanDataPackage = value;
                RaisePropertyChanged();
            }
        }

        private CleanDataPackage _comparer1CleanDataPackage;

        public CleanDataPackage Comparer1CleanDataPackage
        {
            get { return _comparer1CleanDataPackage ?? (_comparer1CleanDataPackage = new CleanDataPackage()); }

            set
            {
                if (_comparer1CleanDataPackage == value)
                {
                    return;
                }
                _comparer1CleanDataPackage = value;
                RaisePropertyChanged();
            }
        }

        private CleanDataPackage _comparer2CleanDataPackage;

        public CleanDataPackage Comparer2CleanDataPackage
        {
            get { return _comparer2CleanDataPackage ?? (_comparer2CleanDataPackage = new CleanDataPackage()); }

            set
            {
                if (_comparer2CleanDataPackage == value)
                {
                    return;
                }
                _comparer2CleanDataPackage = value;
                RaisePropertyChanged();
            }
        }

        private CleanDataPackage _comparer3CleanDataPackage;

        public CleanDataPackage Comparer3CleanDataPackage
        {
            get { return _comparer3CleanDataPackage ?? (_comparer3CleanDataPackage = new CleanDataPackage()); }

            set
            {
                if (_comparer3CleanDataPackage == value)
                {
                    return;
                }
                _comparer3CleanDataPackage = value;
                RaisePropertyChanged();
            }
        }

        private CleanDataRule _selectedBaseCleanDataRule;

        public CleanDataRule SelectedBaseCleanDataRule
        {
            get { return _selectedBaseCleanDataRule ?? (_selectedBaseCleanDataRule = new CleanDataRule()); }

            set
            {
                if (_selectedBaseCleanDataRule == value)
                {
                    return;
                }
                _selectedBaseCleanDataRule = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}