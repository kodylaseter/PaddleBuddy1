using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private List<SearchItem> _data;
        private string _searchString;
        private bool _showSpacer;

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
                FilteredData?.Clear();
                _searchString = value;
                if (!string.IsNullOrWhiteSpace(_searchString))
                {
                    FilteredData =
                        new ObservableCollection<SearchItem>(Data?.Where(w => w.SearchString.Contains(SearchString)));
                }
                RaisePropertyChanged(() => FilteredData);
                RaisePropertyChanged(() => SpacerText);
                RaisePropertyChanged(() => IsShown);
            }
        }


        public bool ShowSpacer
        {
            get { return _showSpacer; }
            set
            {
                _showSpacer = value;
                RaisePropertyChanged(() => ShowSpacer);
                RaisePropertyChanged(() => SpacerText);
            }
        }

        public string SpacerText
        {
            get
            {
                var text = FilteredData == null || FilteredData.Count < 1 ? "No results" : "";
                return text;
            }
        }



        public bool IsShown => !string.IsNullOrEmpty(_searchString);
    }
}
