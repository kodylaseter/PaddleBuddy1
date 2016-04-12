using System;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Models.Messages;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class LocationProviderAndroid : ILocationProvider
    {
        private readonly Listener _locationListener;
        public LocationProviderAndroid()
        {
            _locationListener = new Listener();
            var locationManager = Application.Context.GetSystemService(Context.LocationService) as LocationManager;
            if (locationManager != null)
            {
                var criteria = new Criteria();
                criteria.Accuracy = Accuracy.Fine;
                criteria.PowerRequirement = Power.NoRequirement;
                var locationProvider = locationManager.GetBestProvider(criteria, true);
                if (!String.IsNullOrEmpty(locationProvider))
                {
                    locationManager.RequestLocationUpdates(locationProvider, 100, 0, _locationListener);
                }
                else
                {
                    Mvx.Resolve<IMvxMessenger>().Publish(new ToastMessage(this, "Location provider was null!", false));
                }
            }
            else
            {
                Mvx.Resolve<IMvxMessenger>().Publish(new ToastMessage(this, "could not create location manager", false));
            }
        }

        public Point GetCurrentLocation()
        {
            while (_locationListener.CurrentLocation == null)
            {
                var x = 0;
            }
            return new Point
            {
                Lat = _locationListener.CurrentLocation.Latitude,
                Lng = _locationListener.CurrentLocation.Longitude
            };
        }
        class Listener : Java.Lang.Object, ILocationListener
        {

            public Location CurrentLocation { get; set; }

            public void OnLocationChanged(Location location)
            {
                CurrentLocation = location;
            }

            public void OnProviderDisabled(string provider)
            {
                throw new NotImplementedException();
            }

            public void OnProviderEnabled(string provider)
            {
                throw new NotImplementedException();
            }

            public void OnStatusChanged(string provider, Availability status, Bundle extras)
            {
                throw new NotImplementedException();
            }
        }
    }
}