﻿using System;
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

        public void Setup()
        {
            MapDrawer = Mvx.Resolve<IMapDrawer>();
            if (CurrentLocation == null)
            {
                //using this simulate, plan from tretret to qwertyuiop
                CurrentLocation = new Point
                {
                    Lat = 34.065676,
                    Lng = -84.272612
                };
            }
        }

        public async Task SetupAsync()
        {
            IsLoading = true;
            await Task.Run(() => LetDBSetup());
            switch (InitMode)
            {
                case MapInitModes.Init:
                    await SetupInit();
                    break;
                case MapInitModes.TripStart:
                    SetupTripStart();
                    break;
                default: throw new ArgumentOutOfRangeException();
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
                var current = LocationService.GetInstance().CurrentLocation;
                MapDrawer.DrawMarker(StartPoint);
                MapDrawer.DrawMarker(EndPoint);
                MapDrawer.MoveCamera(current);
                MapDrawer.AnimateCameraBounds( new[] { StartPoint, EndPoint, current });
                MapDrawer.DrawLine(StartPoint, EndPoint);
            }
        }

        public async Task SetupInit()
        {
            if (!await LetLocationStart())
            {
                MessengerService.Toast(this, "Unable to get current location", true);
            }
            MapDrawer.MoveCameraZoom(CurrentLocation, 8);
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

        private async Task<bool> LetLocationStart()
        {
            var times = 0;
            var max = 10;
            while (times < max && LocationService.GetInstance().CurrentLocation == null)
            {
                await Task.Delay(100);
                times++;
            }
            return times != max;
        }

        public Point CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                //MapDrawer.DrawCurrent(_currentLocation);
                AdjustForLocation(_currentLocation);
            }
        }



        public void AdjustForLocation(Point point)
        {
            switch (InitMode)
            {
                case MapInitModes.Init:
                    break;
                case MapInitModes.TripStart:
                    var dist = PBUtilities.DistanceInMeters(point, StartPoint);
                    if (dist > 50)
                    {
                        SubBarText = "Proceed to start point";
                    }
                    else
                    {
                        SubBarText = "At start";
                    }
                    break;
                default:
                    MessengerService.Toast(this, "Map init mode not set", true);
                    break;
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
