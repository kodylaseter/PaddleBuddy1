using System;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Core.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public IMapDrawer MapDrawer { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public bool MapReady { get; set; }
        public MapInitModes InitMode { get; set; }

        
        public void Init(MapInitModes initMode = MapInitModes.Init, int start = int.MaxValue, int end = int.MaxValue)
        {
            InitMode = initMode;
            StartPoint = new Point {Id = start};
            EndPoint = new Point {Id = end};
        }

        public void Setup()
        {
            MapDrawer = Mvx.Resolve<IMapDrawer>();
            switch (InitMode)
            {
                case MapInitModes.Init:
                    SetCamera();
                    break;
                case MapInitModes.Plan:
                    PlanSetup();
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public async void PlanSetup()
        {
            if (StartPoint.Id == int.MaxValue || EndPoint.Id == int.MaxValue)
            {
                MessengerService.Toast(this, "Start or end value not valid", true);
            }
            else
            {
                StartPoint = await MapService.GetInstance().GetPoint(StartPoint.Id);
                EndPoint = await MapService.GetInstance().GetPoint(EndPoint.Id);
                var current = LocationService.GetInstance().GetCurrentLocation();
                MapDrawer.DrawMarker(StartPoint);
                MapDrawer.DrawMarker(EndPoint);
                MapDrawer.MoveCamera(current);
                MapDrawer.AnimateCameraBounds( new[] { StartPoint, EndPoint });
            }
        }

        public async void SetCamera()
        {
            MapDrawer.MoveCameraZoom(LocationService.GetInstance().GetCurrentLocation(), 11);
            try
            {
                var river = await MapService.GetInstance().GetClosestRiver();
                if (river.Points != null)
                {
                    MapDrawer.DrawLine(river.Points.ToArray());
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
