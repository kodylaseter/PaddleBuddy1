using Android.Gms.Maps.Model;
using Java.Lang;
using Java.Util;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Droid.Utilities
{
    public static class Converter
    {
        public static IIterable PathToLatLngs(Path path)
        {
            ArrayList ret = new ArrayList();
            foreach (var a in path.Points)
            {
                ret.Add(new LatLng(a.Lat, a.Lng));
            }
            return ret;
        } 
    }
}