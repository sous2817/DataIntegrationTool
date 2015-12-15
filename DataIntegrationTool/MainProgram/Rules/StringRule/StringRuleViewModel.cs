using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegrationTool.MainProgram.Rules.StringRule
{
    public class StringRuleViewModel : RulesViewModelBase
    {
        public StringRuleViewModel(string incomingRuleName)
        {
            CurrentlySelectedRuleName = incomingRuleName;
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
