using System.Collections.Generic;
using Android.Gms.Maps.Model;
using Java.Lang;
using Java.Util;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Droid.Utilities
{
    public static class Converter
    {
        public static IIterable RiverToLatLngs(River river)
        {
            ArrayList ret = new ArrayList();
            foreach (var a in river.Points)
            {
                ret.Add(new LatLng(a.Lat, a.Lng));
            }
            return ret;
        } 
    }
}