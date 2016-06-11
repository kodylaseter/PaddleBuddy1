using System;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

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

        public void StartPlan()
        {
            ShowViewModel<PlanViewModel>();
        }


        public void Init(MapInitModes initMode = MapInitModes.Init, int start = int.MaxValue, int end = int.MaxValue)
        {
            InitMode = initMode;
            StartPoint = new Point {Id = start};
            EndPoint = new Point {Id = end};
        }

        public void LocationChanged(Point p)
        {
            
        }

        public async Task Setup()
        {
            IsLoading = true;
            await Task.Run(() => LetDBSetup());
            IsLoading = false;
            MapDrawer = Mvx.Resolve<IMapDrawer>();
            switch (InitMode)
            {
                case MapInitModes.Init:
                    SetupInit();
                    break;
                case MapInitModes.Plan:
                    SetupPlan();
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
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

        public void SetupPlan()
        {
            if (StartPoint.Id == int.MaxValue || EndPoint.Id == int.MaxValue)
            {
                MessengerService.Toast(this, "Start or end value not valid", true);
            }
            else
            {
                StartPoint = MapService.GetInstance().GetPoint(StartPoint.Id);
                EndPoint = MapService.GetInstance().GetPoint(EndPoint.Id);
                var current = LocationService.GetInstance().GetCurrentLocation();
                MapDrawer.DrawMarker(StartPoint);
                MapDrawer.DrawMarker(EndPoint);
                MapDrawer.MoveCamera(current);
                MapDrawer.AnimateCameraBounds( new[] { StartPoint, EndPoint, current });
            }
        }

        public void SetupInit()
        {
            MapDrawer.MoveCameraZoom(LocationService.GetInstance().GetCurrentLocation(), 8);
            try
            {
                var path = MapService.GetInstance().GetClosestRiver();
                if (path.Points != null)
                {
                    MapDrawer.DrawLine(path.Points.ToArray());
                    var launchSites = from p in DatabaseService.GetInstance().Points where p.RiverId == MapService.GetInstance().ClosestRiverId && p.IsLaunchSite select p;
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
    }

    public enum MapInitModes
    {
        Init,
        Plan
    }
}
