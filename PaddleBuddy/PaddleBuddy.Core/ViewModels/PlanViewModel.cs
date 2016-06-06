using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Services;
using System.Threading.Tasks;

namespace PaddleBuddy.Core.ViewModels
{
    public class PlanViewModel : BaseViewModel
    {
        private string _startId;
        private string _endId;
        private TripEstimate _trip;
        private bool _isLoading;
        private string _riverId;

        public PlanViewModel()
        {
            StartId = 48.ToString();
            EndId = 52.ToString();
            RiverId = 17.ToString();
        }

        public TripEstimate Trip
        {
            get { return _trip; }
            set { _trip = value; }
        }

        public string StartId
        {
            get { return _startId; }
            set
            {
                _startId = value;
                Estimate();
            }
        }

        public string EndId
        {
            get { return _endId; }
            set
            {
                _endId = value;
                Estimate();
            }
        }

        public string RiverId
        {
            get { return _riverId; }
            set
            {
                _riverId = value;
                Estimate();
            }
        }

        public bool CanStart
        {
            get { return _trip != null; }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value; 
                RaisePropertyChanged(() => IsLoading);
            }
        }


        public void StartTrip()
        {
            if (!CanStart)
            {
                MessengerService.Toast(this, "Invalid trip data", true);
                return;
            }
            ShowViewModel<MapViewModel>(new {initMode = MapInitModes.Plan, start = int.Parse(_startId), end = int.Parse(_endId) });
        }

        public async void Estimate()
        {
            Trip = null;
            IsLoading = true;
            if (_startId != null && _endId != null && _riverId != null)
            {
                int startId = int.Parse(_startId);
                int endId = int.Parse(_endId);
                int riverId = int.Parse(_riverId);
                await Task.Run(() =>
                    Trip = PlanService.GetInstance().EstimateTrip(startId, endId, riverId));
                MessengerService.Toast(this, "Time estimate: " + Trip, false);
            }
            IsLoading = false;
            RaisePropertyChanged(() => IsLoading);
        }


        public ICommand StartCommand
        {
            get { return new MvxCommand(StartTrip); }
        }
    }
}
