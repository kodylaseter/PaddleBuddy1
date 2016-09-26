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
        private readonly ILocationProvider _locationProvider;
        private Point _currentLocation;
        private readonly IMvxMessenger _mvxMessenger;
        public IGeolocator Geolocator => CrossGeolocator.Current;

        public LocationService()
        {
            _locationProvider = Mvx.Resolve<ILocationProvider>();
            _mvxMessenger = Mvx.Resolve<IMvxMessenger>();
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
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 5;

            var position = await locator.GetPositionAsync(10000);
            return new Point
            {
                Lat = position.Latitude,
                Lng = position.Longitude
            };
        }

        public void StartListening()
        {
            Geolocator.StartListeningAsync(1, 1, true);
            Geolocator.PositionChanged += PositionChanged();
            var b = Geolocator.IsListening;

        }

        private EventHandler<PositionEventArgs> PositionChanged()
        {
            _mvxMessenger.Publish(new LocationChangedMessage(this));
            return null;
        }
    }
}
