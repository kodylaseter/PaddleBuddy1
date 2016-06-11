using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Services;
using System.Threading.Tasks;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.ViewModels
{
    public class PlanViewModel : BaseViewModel
    {
        private Point _startPoint;
        private Point _endPoint;
        private TripEstimate _trip;
        private bool _isLoading;
        private string _riverId;

        public PlanViewModel(Point start)
        {
            StartPoint = start;
        }

        public PlanViewModel()
        {

        }

        public TripEstimate Trip
        {
            get { return _trip; }
            set { _trip = value; }
        }

        public Point StartPoint
        {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                Estimate();
            }
        }

        public Point EndPoint
        {
            get { return _endPoint; }
            set
            {

                _endPoint = value;
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
            ShowViewModel<MapViewModel>(new {initMode = MapInitModes.Plan, start = int.Parse(_start), end = int.Parse(_end) });
        }

        public async void Estimate()
        {
            Trip = null;
            IsLoading = true;
            if (_start != null && _end != null && _riverId != null)
            {
                int startId = int.Parse(_start);
                int endId = int.Parse(_end);
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
