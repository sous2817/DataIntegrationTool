using System.Collections.ObjectModel;
using DataIntegrationTool.MainProgram.Rules;
using DataIntegrationTool.MainProgram.Rules.StringRule;
using GalaSoft.MvvmLight;
using DataIntegrationTool.Resources.Enums;

namespace DataIntegrationTool.BaseClasses
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CleanDataRule : ViewModelBase
    {

        private string _columnName = "Select Column";

        public string ColumnName
        {
            get { return _columnName; }

            set
            {
                if (_columnName == value)
                {
                    return;
                }
                _columnName = value;
                RaisePropertyChanged();
            }
        }

        private string _rule = "String";

        public string Rule
        {
            get { return _rule; }

            set
            {
                if (_rule == value)
                {
                    return;
                }
                _rule = value;
                RaisePropertyChanged();
            }
        }

 //       public TrinaryLogicLists TrinaryLogicRulesList { get; set; }

        private Scoring.ClinWeb _scoringClinWeb = Scoring.ClinWeb.Visualization;

        public Scoring.ClinWeb ScoringClinWeb
        {
            get { return _scoringClinWeb; }

            set
            {
                if (_scoringClinWeb == value)
                {
                    return;
                }
                _scoringClinWeb = value;
                RaisePropertyChanged();
            }
        }
        
        private string _baseEquivalent = "New Column";

        public string BaseEquivalent
        {
            get { return _baseEquivalent; }

            set
            {
                if (_baseEquivalent == value)
                {
                    return;
                }
                _baseEquivalent = value;
                RaisePropertyChanged();
            }
        }

        private Scoring.NumericRules _numericRule;

        public Scoring.NumericRules NumericRule
        {
            get { return _numericRule; }

            set
            {
                if (_numericRule == value)
                {
                    return;
                }
                _numericRule = value;
                RaisePropertyChanged();
            }
        }

        private RulesViewModelBase _rulesViewModel;

        public RulesViewModelBase RulesViewModel
        {
            get { return _rulesViewModel ?? (_rulesViewModel = new StringRuleViewModel()); }

            set
            {
                if (_rulesViewModel == value)
                {
                    return;
                }
                _rulesViewModel = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _hierarchyList = new ObservableCollection<string>();

        public ObservableCollection<string> HierarchyList
        {
            get { return _hierarchyList; }

            set
            {
                if (_hierarchyList == value)
                {
                    return;
                }
                _hierarchyList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _columnData = new ObservableCollection<string>();

        public ObservableCollection<string> ColumnData
        {
            get { return _columnData; }

            set
            {
                if (_columnData == value)
                {
                    return;
                }
                _columnData = value;
                RaisePropertyChanged();
            }
        }

        private bool _includeColumn;

        public bool IncludeColumn
        {
            get { return _includeColumn; }

            set
            {
                if (_includeColumn == value)
                {
                    return;
                }
                _includeColumn = value;
                RaisePropertyChanged();
            }
        }

        private bool _isKey;

        public bool IsKey
        {
            get { return _isKey; }

            set
            {
                if (_isKey == value)
                {
                    return;
                }
                _isKey = value;
                RaisePropertyChanged();
            }
        }

        private bool _newProperty = true;

        public bool NewProperty
        {
            get { return _newProperty; }

            set
            { _newProperty = value; }
        }
    }
}
