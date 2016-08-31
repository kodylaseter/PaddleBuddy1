using System;
using Android.Util;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class LocationProviderAndroid : BaseDependencyServiceAndroid, ILocationProvider
    {
        private readonly IGeolocator _locator;
        public LocationProviderAndroid()
        {
            _locator = CrossGeolocator.Current;
            _locator.DesiredAccuracy = 10;
            //var a = CurrentLocation;
        }
        public Point CurrentLocation
        {
            get
            {
                if (!PermissionService.CheckOrRequestPermission(Permission.Location).Result) return null;
                try
                {
                    var position = _locator.GetPositionAsync(10000).Result;
                    return new Point {
                        Lat = position.Latitude,
                        Lng = position.Longitude
                    };
                }
                catch (Exception ex)
                {
                    Log.Error("PBUDDY", "Unable to get location, may need to increase timeout: " + ex);
                }
                return null;
            }
        }

        //class Listener : Java.Lang.Object, ILocationListener
        //{
        //    public Location CurrentLocation { get; set; }

        //    public void OnLocationChanged(Location location)
        //    {
        //        CurrentLocation = location;
        //    }

        //    public void OnProviderDisabled(string provider)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void OnProviderEnabled(string provider)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void OnStatusChanged(string provider, Availability status, Bundle extras)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}