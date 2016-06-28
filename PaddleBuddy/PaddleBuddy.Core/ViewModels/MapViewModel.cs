using System;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.ViewModels.parameters;
using PaddleBuddy.Core.Utilities;

namespace PaddleBuddy.Core.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public IMapDrawer MapDrawer { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public bool MapReady { get; set; }
        public MapInitModes InitMode { get; set; }
        private bool _isLoading;
        private Point _selectedMarker;
        private Point _currentLocation;

        public void Init(MapParameters mapParameters)
        {
            if (mapParameters == null || !mapParameters.Set) return;
            InitMode = mapParameters.InitMode;
            StartPoint = DatabaseService.GetInstance().GetPoint(mapParameters.StartId);
            EndPoint = DatabaseService.GetInstance().GetPoint(mapParameters.EndId);
        }

        public void StartPlan()
        {
            ShowViewModel<PlanViewModel>(new PlanParameters() { StartId = _selectedMarker.Id, Set = true } );
        }

        public void LocationChanged(Point p)
        {
            CurrentLocation = p;
        }

        public async Task Setup()
        {
            IsLoading = true;
            await Task.Run(() => LetDBSetup());
            MapDrawer = Mvx.Resolve<IMapDrawer>();
            switch (InitMode)
            {
                case MapInitModes.Init:
                    SetupInit();
                    break;
                case MapInitModes.TripStart:
                    SetupTripStart();
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
            if (CurrentLocation == null)
            {
                CurrentLocation = new Point
                {
                    Lat = 34.065676,
                    Lng = -84.272612
                };
            }
            IsLoading = false;
        }

        public async Task LetDBSetup()
        {
            var times = 0;
            while (!DatabaseService.GetInstance().IsReady)
            {
                if (times == 20)
                {
                    MessengerService.Toast(this, "DB not updating. Consider restarting app", false);
                }
                await Task.Delay(TimeSpan.FromMilliseconds(300));
                times++;
            }
        }

        public void SetupTripStart()
        {
            if (StartPoint.Id == int.MaxValue || EndPoint.Id == int.MaxValue)
            {
                MessengerService.Toast(this, "Start or end value not valid", true);
            }
            else
            {
                StartPoint = DatabaseService.GetInstance().GetPoint(StartPoint.Id);
                EndPoint = DatabaseService.GetInstance().GetPoint(EndPoint.Id);
                var current = LocationService.GetInstance().GetCurrentLocation();
                MapDrawer.DrawMarker(StartPoint);
                MapDrawer.DrawMarker(EndPoint);
                MapDrawer.MoveCamera(current);
                MapDrawer.AnimateCameraBounds( new[] { StartPoint, EndPoint, current });
                MapDrawer.DrawLine(StartPoint, EndPoint);
                
            }
        }

        public void SetupInit()
        {
            MapDrawer.MoveCameraZoom(LocationService.GetInstance().GetCurrentLocation(), 8);
            try
            {
                var path = DatabaseService.GetInstance().GetClosestRiver();
                if (path.Points != null)
                {
                    MapDrawer.DrawLine(path.Points.ToArray());
                    var launchSites = from p in DatabaseService.GetInstance().Points where p.RiverId == DatabaseService.GetInstance().ClosestRiverId && p.IsLaunchSite select p;
                    foreach (var site in launchSites)
                    {
                        MapDrawer.DrawMarker(site);
                    }
                }
                else
                {
                    MessengerService.Toast(this, "Failed to get nearest river", true);
                }
            }
            catch (Exception)
            {
                MessengerService.Toast(this, "Failed to get nearest river", true);
            }
        }

        public string StartText
        {
            get
            {
                return "teststart";
            }
        }

        public string EndText
        {
            get
            {
                return "test end";
            }
        }

        public Point CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                MapDrawer.DrawCurrent(CurrentLocation);
                var dist = PBUtilities.DistanceInMeters(_currentLocation, StartPoint);
                switch (InitMode)
                {
                    case MapInitModes.Init:
                        break;
                    case MapInitModes.TripStart:
                        if (dist > 50)
                        {
                            SubBarText = "Navigate to starting point";
                        }
                        break;
                    default: MessengerService.Toast(this, "Map init mode not set", true);
                        break;
                }
            }
        }

        public void AdjustForLocation()
        {
            if (CurrentLocation == null || StartPoint == null) return;
            var dist = PBUtilities.DistanceInMeters(CurrentLocation, StartPoint);
            if (dist < 40)
            {
                MessengerService.Toast(this, dist.ToString(), true);
            }
        }

        public Point SelectedMarker
        {
            get { return _selectedMarker; }
            set
            {
                _selectedMarker = value;
                RaisePropertyChanged(() => SelectedMarker);
            }
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

        public ICommand StartPlanCommand
        {
            get
            {
                return new MvxCommand(StartPlan);
            }
        }
    }

    public enum MapInitModes
    {
        Init,
        TripStart
    }
}
