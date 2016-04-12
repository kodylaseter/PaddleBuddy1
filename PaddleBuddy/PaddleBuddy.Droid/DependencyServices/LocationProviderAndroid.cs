using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class LocationProviderAndroid : ILocationProvider
    {
        //public SimpleLocationManager LocationManager { get; set; }

        //public LocationProviderAndroid()
        //{
        //    LocationManager = new SimpleLocationManager();
        //}


        //public Point GetCurrentLocation()
        //{
        //    return new Point
        //    {
        //        Lat = LocationManager.LastLocation.Latitude,
        //        Lng = LocationManager.LastLocation.Longitude
        //    };
        //}
    }
}