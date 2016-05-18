using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Java.Lang;
using Java.Util;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class MapDrawerAndroid : IMapDrawer
    {

        public GoogleMap Map { get; set; }

        public void DrawLine(Point[] points)
        {
            if (IsMapNull) return;
            var polyOpts = new PolylineOptions()
                        .InvokeColor(Resource.Color.black)
                        .InvokeWidth(9)
                        .InvokeZIndex(1);
            foreach (var p in points)
            {
                polyOpts.Add(new LatLng(p.Lat, p.Lng));
            }
        }

        public void DrawMarker(Point p)
        {
            if (IsMapNull) return;
            Map.AddMarker(new MarkerOptions().SetPosition(new LatLng(p.Lat, p.Lng)));
        }

        public void DrawCurrent(Point current = null)
        {
            if (IsMapNull) return;
            if (current == null) current = LocationService.GetInstance().GetCurrentLocation();
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(current.Lat, current.Lng));
            var icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.current_circle);
        }

        public void MoveCamera(Point p)
        {
            if (IsMapNull) return;
            Map.MoveCamera(CameraUpdateFactory.NewLatLng(new LatLng(p.Lat, p.Lng)));
        }

        public void MoveCameraZoom(Point p, int zoom)
        {
            if (IsMapNull) return;
            Map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(p.Lat, p.Lng), zoom));
        }

        public void AnimateCameraBounds(Point[] points)
        {
            if (IsMapNull) return;
            var builder = new LatLngBounds.Builder();
            foreach (var p in points)
            {
                builder.Include(new LatLng(p.Lat, p.Lng));
            }
            var bounds = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewLatLngBounds(bounds, 80);
            Map.AnimateCamera(cameraUpdate);
        }

        public bool IsMapNull
        {
            get
            {
                if (Map != null) return false;
                MessengerService.Toast(this, "Map is null!", true);
                return true;
            }
        }
    }
}