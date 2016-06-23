using System;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class MapDrawerAndroid : IMapDrawer
    {
        public GoogleMap Map { get; set; }
        public Marker CurrentMarker { get; set; }

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
            Map.AddPolyline(polyOpts);
        }

        public void DrawLine(Point start, Point end)
        {
            var path = DatabaseService.GetInstance().GetPath(start, end);
            DrawLine(path.Points.ToArray());
        }

        public void DrawMarker(Point p)
        {
            if (IsMapNull || p == null) return;
            var marker = new MarkerOptions().SetPosition(new LatLng(p.Lat, p.Lng));
            if (p.IsLaunchSite) marker.SetTitle(p.Label).SetSnippet(p.Id.ToString());
            Map.AddMarker(marker);
        }

        public void DrawCurrent(Point current = null)
        {
            if (IsMapNull) return;
            if (CurrentMarker == null)
            {
                var markerOpts = new MarkerOptions().SetPosition(new LatLng(current.Lat, current.Lng));
                CurrentMarker = Map.AddMarker(markerOpts);
            } else
            {
                CurrentMarker.Remove();
                var markerOpts = new MarkerOptions().SetPosition(new LatLng(current.Lat, current.Lng));
                CurrentMarker = Map.AddMarker(markerOpts);
            }
            //if (current == null) current = LocationService.GetInstance().GetCurrentLocation();
            //var markerOptions = new MarkerOptions();
            //markerOptions.SetPosition(new LatLng(current.Lat, current.Lng));
            //var icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.current_circle);

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