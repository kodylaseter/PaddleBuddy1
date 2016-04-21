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

        public ObservableCollection<River> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
            }
        }


        public BaseViewModel()
        {
            Messenger = Mvx.Resolve<IMvxMessenger>();
            Data = SearchService.GetInstance().Data;
        }

        protected IMvxMessenger Messenger { get; private set; }
    }
}
