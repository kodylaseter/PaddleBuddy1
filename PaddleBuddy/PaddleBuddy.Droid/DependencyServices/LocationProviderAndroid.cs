using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class LocationProviderAndroid : ILocationProvider
    {
        //private readonly IGeolocator _locator;
        //public LocationProviderAndroid()
        //{
        //    _locator = CrossGeolocator.Current;
        //    _locator.DesiredAccuracy = 20;
        //    _locator.AllowsBackgroundUpdates = true;
        //    //var a = CurrentLocation;
        //}
        
        //private readonly LocationManager _locationManager;
        //private readonly Criteria _criteria;

        //public LocationProviderAndroid()
        //{
        //    _locationManager = Application.Context.GetSystemService(Context.LocationService) as LocationManager;
        //    if (_locationManager != null)
        //    {
        //        _criteria = new Criteria
        //        {
        //            Accuracy = Accuracy.Fine,
        //            PowerRequirement = Power.NoRequirement
        //        };
        //    }
        //    else
        //    {
        //        MessengerService.Toast(this, "could not create location manager", false);
        //    }
        //}

        //public bool InitLocationProvider()
        //{
        //    if (!PermissionService.CheckOrRequestPermission(Permission.Location).Result) return false;
        //    try
        //    {
        //        //_locationManager.RequestSingleUpdate(_locationManager.GetBestProvider(_criteria, true), this, Looper.MainLooper);
        //        _locationManager.RequestLocationUpdates(_locationManager.GetBestProvider(_criteria, true), 500, 1, this);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("PBUDDY", "Unable to initialize location provider: " + ex);
        //    }
        //    return true;
        //}

        public Point CurrentLocation { get; set; }


        //public void OnLocationChanged(Location location)
        //{
        //    CurrentLocation = new Point
        //    {
        //        Lat = location.Latitude,
        //        Lng = location.Longitude
        //    };
        //}

        //public void OnProviderDisabled(string provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnProviderEnabled(string provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnStatusChanged(string provider, Availability status, Bundle extras)
        //{
        //    //throw new NotImplementedException();
        //}

        //private class Listener : Java.Lang.Object, ILocationListener
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