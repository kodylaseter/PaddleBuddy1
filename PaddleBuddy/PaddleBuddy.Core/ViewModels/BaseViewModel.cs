using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private List<SearchItem> _data;
        private string _searchString;
        private bool _showSpacer;
        public SearchService SearchService { get; set; }

        public override void Start()
        {
            base.Start();
            SearchService = new SearchService();
            SearchService.SetData(SearchService.ArrayToSearchSource(DatabaseService.GetInstance().Rivers.ToArray()));
        }

        public ObservableCollection<SearchItem> FilteredData { get; set; }


        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                FilteredData = SearchService.Filter(_searchString);
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
