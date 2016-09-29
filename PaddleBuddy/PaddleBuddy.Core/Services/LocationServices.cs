using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Models.Messages;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace PaddleBuddy.Core.Services
{
    public class LocationService
    {
        private static LocationService _locationService;
        private Point _currentLocation;
        private readonly IMvxMessenger _mvxMessenger;
        public IGeolocator Geolocator => CrossGeolocator.Current;

        public LocationService()
        {
            _mvxMessenger = Mvx.Resolve<IMvxMessenger>();
            Geolocator.DesiredAccuracy = 5;
        }


        public static LocationService GetInstance()
        {
            return _locationService ?? (_locationService = new LocationService());
        }

        public Point CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                _mvxMessenger.Publish(new LocationChangedMessage(this));
            }
        }

        public async Task<Point> GetLocationAsync()
        {

            var position = await Geolocator.GetPositionAsync(10000);
            var point = new Point
            {
                Lat = position.Latitude,
                Lng = position.Longitude
            };
            CurrentLocation = point;
            return point;
        }

        public void StartListening()
        {
            Geolocator.StartListeningAsync(1, 1, true);
            Geolocator.PositionChanged += OnPositionChanged;
            var b = Geolocator.IsListening;
        }

        public void StopListening()
        {
            Geolocator.StopListeningAsync();
        }

        private void OnPositionChanged(object sender, object eventArgs)
        {
            var point = new Point();
            if (eventArgs.GetType() == typeof (PositionEventArgs))
            {
                var args = (PositionEventArgs) eventArgs;
                point = new Point
                {
                    Lat = args.Position.Latitude,
                    Lng = args.Position.Longitude
                };
            }
            else if (eventArgs.GetType() == typeof (Point))
            {
                point = (Point) eventArgs;
            }
            else
            {
                MessengerService.Toast(this, "OnPositionChanged error", true);
                var a = "shouldnt get here";
            }
            CurrentLocation = point;
        }

        public void StartSimulating(List<Point> points)
        {
            //this
        }
    }
}
