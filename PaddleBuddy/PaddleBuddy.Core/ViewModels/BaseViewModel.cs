using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private List<SearchItem> _data;
        private string _searchString;

        public List<SearchItem> Data
        {
            get { return _data ?? (_data = SearchService.GetInstance().Data); }
            set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
            }
        }

        public ObservableCollection<SearchItem> FilteredData { get; set; }


        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                if (_searchString != null)
                {
                    FilteredData?.Clear();
                    FilteredData =
                        new ObservableCollection<SearchItem>(Data?.Where(w => w.SearchString.Contains(SearchString)));
                }
                else
                {
                    FilteredData = new ObservableCollection<SearchItem>(Data);
                }
                RaisePropertyChanged(() => FilteredData);
                RaisePropertyChanged(() => IsShown);
            }
        }

        public bool IsShown => !string.IsNullOrEmpty(_searchString);


        public BaseViewModel()
        {
            Messenger = Mvx.Resolve<IMvxMessenger>();
            
        }

        protected IMvxMessenger Messenger { get; private set; }
    }
}
