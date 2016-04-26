using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Java.Lang;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Droid.Utilities
{
    public class MapDrawer
    {
        private GoogleMap _map;
        public MapDrawer(GoogleMap map)
        {
            _map = map;
        }

        public void DrawLine(IIterable points, int width = 7, int color = Resource.Color.dark_gray, int zindex = 1)
        {
            _map.AddPolyline(
                new PolylineOptions().AddAll(points)
                    .InvokeColor(color)
                    .InvokeWidth(width)
                    .InvokeColor(color)
                    .InvokeZIndex(zindex));
        }


    }
}