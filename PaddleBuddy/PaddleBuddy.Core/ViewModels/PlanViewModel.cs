using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<SearchItem> _filteredStart;
        private string _startText;
        private string _endText;

        public SearchService StartSearchService { get; set; }

        //public PlanViewModel(Point start, Point end)
        //{
        //    StartPoint = start;
        //    EndPoint = end;
        //}

        public PlanViewModel()
        {
            StartPoint = MapService.GetInstance().GetPoint(48);
            EndPoint = MapService.GetInstance().GetPoint(52);
            StartSearchService = new SearchService();
            StartSearchService.SetData(SearchService.ArrayToSearchSource((from p in DatabaseService.GetInstance().Points where p.IsLaunchSite select p).ToArray()));
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
                RaisePropertyChanged(() => StartText);
                Estimate();
            }
        }

        public Point EndPoint
        {
            get { return _endPoint; }
            set
            {

                _endPoint = value;
                RaisePropertyChanged(() => EndText);
                Estimate();
            }
        }

        public string StartText
        {
            get { return _startText; }
            set
            {
                _startText = value;
                Search(_startText);
            }
        }


        public string EndText
        {
            get { return _endText; }
            set
            {
                _endText = value;
                RaisePropertyChanged(() => EndText);
            }
        }

        public int RiverId { get; set; }

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

        public ObservableCollection<SearchItem> FilteredStart
        {
            get { return _filteredStart; }
            set
            {
                _filteredStart = value; 
                RaisePropertyChanged(() => FilteredStart);
            }
        }

        public void Search(string searchText)
        {
            FilteredStart = StartSearchService.Filter(searchText);
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
            if (StartPoint != null && EndPoint != null)
            {
                if (StartPoint.RiverId == EndPoint.RiverId)
                {
                    await Task.Run(() =>
                    Trip = PlanService.GetInstance().EstimateTrip(StartPoint.Id, EndPoint.Id, StartPoint.RiverId));
                    MessengerService.Toast(this, "Time estimate: " + Trip, false);
                } else
                {
                    MessengerService.Toast(this, "River Ids don't match", true);
                }
                
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
