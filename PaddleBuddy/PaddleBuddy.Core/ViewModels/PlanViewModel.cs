using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class PlanViewModel : BaseViewModel
    {
        private string _startID;
        private string _endID;

        public PlanViewModel()
        {
            StartID = 48.ToString();
            EndID = 52.ToString();
        }

        public string StartID
        {
            get { return _startID; }
            set { _startID = value; }
        }

        public string EndID
        {
            get { return _endID; }
            set { _endID = value; }
        }

        public bool IsLoading { get; set; }

        public async void Estimate()
        {
            IsLoading = true;
            RaisePropertyChanged(() => IsLoading);
            var resp = await PlanService.GetInstance().EstimateTime(int.Parse(_startID), int.Parse(_endID), 17);
            if (resp.Success)
            {
                Messenger.Publish(new ToastMessage(this, "Estimated time: " + ((TripEstimate)resp.Data).Time, true));
            }
            IsLoading = false;
            RaisePropertyChanged(() => IsLoading);
        }


        public ICommand EstimateCommand
        {
            get { return new MvxCommand(Estimate); }
        }
    }
}
