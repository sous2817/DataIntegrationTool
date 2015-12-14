using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIntegrationTool.BaseClasses.DynamicHelper;

namespace DataIntegrationTool.BaseClasses
{
    public class MatchDataItem : DynamicTypeHelper<MatchDataItem>, ICloneable
    {
        private ObservableCollection<MatchDataItem> _matchCollection = new ObservableCollection<MatchDataItem>();

        public ObservableCollection<MatchDataItem> MatchCollection
        {
            get { return _matchCollection; }

            set
            {
                if (_matchCollection == value)
                {
                    return;
                }
                _matchCollection = value;
                RaisePropertyChanged();
            }
        }
        public string MatchKey { get; set; }

        private bool _isMatch;

        public bool IsMatch
        {
            get { return _isMatch; }

            set
            {
                if (_isMatch == value)
                {
                    return;
                }
                _isMatch = value;
                RaisePropertyChanged();
            }
        }
        public decimal MatchScore { get; set; }

        public bool UploadToClinWeb { get; set; } = true;

        public MatchDataItem ShallowCopy()
        {
            return (MatchDataItem)MemberwiseClone();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
