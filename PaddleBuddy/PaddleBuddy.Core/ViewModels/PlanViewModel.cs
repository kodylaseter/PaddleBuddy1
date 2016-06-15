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
        private ObservableCollection<SearchItem> _filteredEnd;
        private string _startText;
        private string _endText;
        private SearchItem _selectedStart;
        private SearchItem _selectedEnd;

        public SearchService StartSearchService { get; set; }
        public SearchService EndSearchService { get; set; }

        public void Init(Point startPoint = null)
        {
            if (startPoint?.Lat != 0)
            {
                StartPoint = startPoint;
            }
        }

        public PlanViewModel()
        {
            StartSearchService = new SearchService();
            StartSearchService.SetData(SearchService.ArrayToSearchSource((from p in DatabaseService.GetInstance().Points where p.IsLaunchSite select p).ToArray()));
            EndSearchService = new SearchService();
            EndSearchService.SetData(SearchService.ArrayToSearchSource((from p in DatabaseService.GetInstance().Points where p.IsLaunchSite select p).ToArray()));
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

        public SearchItem SelectedStart
        {
            get { return _selectedStart; }
            set
            {
                _selectedStart = value;
                _startText = _selectedStart.SearchString;
                RaisePropertyChanged(() => StartText);
                FilteredStart.Clear();
                RaisePropertyChanged(() => FilteredStart);
            }
        }

        public SearchItem SelectedEnd
        {
            get { return _selectedEnd; }
            set
            {
                _selectedEnd = value;
                _endText = _selectedEnd.SearchString;
                RaisePropertyChanged(() => EndText);
                FilteredEnd.Clear();
                RaisePropertyChanged(() => FilteredEnd);
            }
        }

        public string StartText
        {
            get { return _startText; }
            set
            {
                _startText = value;
                StartSearch();
            }
        }

        public string EndText
        {
            get { return _endText; }
            set
            {
                _endText = value;
                EndSearch();
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

        public ObservableCollection<SearchItem> FilteredEnd
        {
            get { return _filteredEnd; }
            set
            {
                _filteredEnd = value;
                RaisePropertyChanged(() => FilteredEnd);
            }
        }

        public void StartSearch()
        {
            FilteredStart = StartSearchService.Filter(_startText);
        }

        public void EndSearch()
        {
            FilteredEnd = StartSearchService.Filter(_endText);
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

        public void StartChanged(SearchItem searchItem)
        {
            SelectedStart = searchItem;
        }

        public void EndChanged(SearchItem searchItem)
        {
            SelectedEnd = searchItem;
        }

        public ICommand StartChangedCommand
        {
            get
            {
                return new MvxCommand<SearchItem>(StartChanged);
            }
        }

        public ICommand EndChangedCommand
        {
            get { return new MvxCommand<SearchItem>(EndChanged); }
        }
    }
}
