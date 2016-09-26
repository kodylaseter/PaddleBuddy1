using System;
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
            _currentLocation = point;
            return point;
        }

        public void StartListening()
        {
            Geolocator.StartListeningAsync(1, 1, true);
            Geolocator.PositionChanged += GeolocatorOnPositionChanged;
            var b = Geolocator.IsListening;

        }

        private void GeolocatorOnPositionChanged(object sender, PositionEventArgs positionEventArgs)
        {
            if (positionEventArgs?.Position != null)
            {
                CurrentLocation = new Point
                {
                    Lat = positionEventArgs.Position.Latitude,
                    Lng = positionEventArgs.Position.Longitude
                };
            }
        }
    }
}
