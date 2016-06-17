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
            var filtered = StartSearchService.Filter(_startText);
            if (_selectedEnd != null)
            {
                filtered.ToList().RemoveAll(x => ((Point) x.Item).RiverId != ((Point) _selectedEnd.Item).RiverId);
            }
            FilteredStart = filtered;
        }

        public void EndSearch()
        {
            var filtered = StartSearchService.Filter(_endText);
            if (_selectedStart != null)
            {
                //todo: convert thESE to linq??
                //todo: fix thisssssss
                filtered.ToList().RemoveAll(x => ((Point)x.Item).RiverId != ((Point)_selectedStart.Item).RiverId);
            }
            FilteredEnd = filtered;
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

        public void Estimate()
        {
            if (_startPoint != null && _endPoint != null)
            {
                if (_startPoint.RiverId == _endPoint.RiverId)
                {
                    var trip = PlanService.GetInstance().EstimateTrip(_startPoint.Id, _endPoint.Id, _startPoint.RiverId);
                    if (trip != null)
                    {
                        if (Trips == null) Trips = new ObservableCollection<TripEstimate>();
                        Trips.Add(trip);
                        RaisePropertyChanged(() => Trips);
                    }
                }
                else
                {
                    MessengerService.Toast(this, "River Ids don't match", true);
                }
                
            }
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
