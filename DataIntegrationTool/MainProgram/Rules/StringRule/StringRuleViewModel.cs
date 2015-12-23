using GalaSoft.MvvmLight.Ioc;

namespace DataIntegrationTool.MainProgram.Rules.StringRule
{
    public class StringRuleViewModel : RulesViewModelBase
    {
        public StringRuleViewModel(string incomingRuleName)
        {
            CurrentlySelectedRuleName = incomingRuleName;
        }

        [PreferredConstructor]
        public StringRuleViewModel()
        {
        }

        private string _currentlySelectedRuleName = "String Rules";
        public string CurrentlySelectedRuleName
        {
            get { return _currentlySelectedRuleName; }

            set
            {
                if (_currentlySelectedRuleName == value)
                {
                    return;
                }
                _currentlySelectedRuleName = value;
                RaisePropertyChanged();
            }
        }
    }
}
