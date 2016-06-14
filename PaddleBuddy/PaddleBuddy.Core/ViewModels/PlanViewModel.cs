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
        private int _riverId;
        private string _startText;
        private string _endText;

        public PlanViewModel(Point start = null, Point end = null)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public PlanViewModel()
        {
            StartPoint = new Point {Id = 48};
            EndPoint = new Point {Id = 52};
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

        public string StartText
        {
            get { return _startText; }
            set { _startText = value; }
        }


        public string EndText
        {
            get { return _endText; }
            set { _endText = value; }
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
            ShowViewModel<MapViewModel>(new {initMode = MapInitModes.Plan, start = StartPoint, end = EndPoint});
        }

        public async void Estimate()
        {
            Trip = null;
            IsLoading = true;
            if (StartPoint != null && EndPoint != null && _riverId != null)
            {
                await Task.Run(() =>
                    Trip = PlanService.GetInstance().EstimateTrip(StartPoint.Id, EndPoint.Id, _riverId));
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
