using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Services;
using System.Threading.Tasks;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.ViewModels.parameters;

namespace PaddleBuddy.Core.ViewModels
{
    public class PlanViewModel : BaseViewModel
    {
        private Point _startPoint;
        private Point _endPoint;
        private bool _isLoading;
        private ObservableCollection<SearchItem> _filteredStart;
        private ObservableCollection<SearchItem> _filteredEnd;
        private string _startText;
        private string _endText;
        private SearchItem _selectedStart;
        private SearchItem _selectedEnd;

        public SearchService StartSearchService { get; set; }
        public SearchService EndSearchService { get; set; }
        public ObservableCollection<TripEstimate> Trips { get; set; }
        public TripEstimate Trip { get; set; }

        public void Init(PlanParameters planParams)
        {
            if (planParams != null && planParams.Set)
            {
                StartPoint = DatabaseService.GetInstance().GetPoint(planParams.StartId);
                _startText = StartPoint.Label;
            }
        }

        public PlanViewModel()
        {
            StartSearchService = new SearchService();
            StartSearchService.SetData(SearchService.ArrayToSearchSource((from p in DatabaseService.GetInstance().Points where p.IsLaunchSite select p).ToArray()));
            EndSearchService = new SearchService();
            EndSearchService.SetData(SearchService.ArrayToSearchSource((from p in DatabaseService.GetInstance().Points where p.IsLaunchSite select p).ToArray()));
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
                StartPoint = _selectedStart.Item as Point;
                _startText = _selectedStart.SearchString;
                RaisePropertyChanged(() => StartText);
                FilteredStart.Clear();
                RaisePropertyChanged(() => FilteredStart);
                Estimate();
            }
        }

        public SearchItem SelectedEnd
        {
            get { return _selectedEnd; }
            set
            {
                _selectedEnd = value;
                EndPoint = _selectedEnd.Item as Point;
                _endText = _selectedEnd.SearchString;
                RaisePropertyChanged(() => EndText);
                FilteredEnd.Clear();
                RaisePropertyChanged(() => FilteredEnd);
                Estimate();
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

        public bool CanStart
        {
            get { return Trip != null; }
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
            ShowViewModel<MapViewModel>(new MapParameters(){ InitMode = MapInitModes.Plan, StartId = Trip.StartId, EndId = Trip.EndId, Set = true });
        }

        public async void Estimate()
        {
            IsLoading = true;
            if (_startPoint != null && _endPoint != null)
            {
                if (_startPoint.RiverId == _endPoint.RiverId)
                {
                    await Task.Run(() =>
                    Trips.Add(PlanService.GetInstance().EstimateTrip(_startPoint.Id, _endPoint.Id, _startPoint.RiverId)));
                    RaisePropertyChanged(() => Trips);
                }
                else
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

        public void TripSelected(TripEstimate trip)
        {
            Trip = trip;
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

        public ICommand TripSelectedCommand
        {
            get
            {
                return new MvxCommand<TripEstimate>(TripSelected);
            }
        }
    }
}
