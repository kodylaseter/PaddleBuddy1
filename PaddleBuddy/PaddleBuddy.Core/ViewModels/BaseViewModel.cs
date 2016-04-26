using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private ObservableCollection<River> _data;
        private string _searchString;

        public ObservableCollection<River> Data
        {
            get { return _data ?? (_data = SearchService.GetInstance().Data); }
            set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
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
