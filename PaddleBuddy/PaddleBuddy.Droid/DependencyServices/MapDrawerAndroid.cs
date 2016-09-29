using System;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Test.Mock;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Services;
using Plugin.CurrentActivity;
using Point = PaddleBuddy.Core.Models.Map.Point;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class MapDrawerAndroid : IMapDrawer
    {
        public GoogleMap Map { get; set; }
        public Marker CurrentMarker { get; set; }
        private MarkerOptions _currentMarkerOptions;

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
            if (current == null)
            {
                current = LocationService.GetInstance().CurrentLocation;
            }
            var position = new LatLng(current.Lat, current.Lng);
            if (CurrentMarker != null)
            {
                CurrentMarker.Position = position;
            }
            else
            {
                CurrentMarker = Map.AddMarker(ReturnCurrentMarkerOptions.SetPosition(position));
            }
        }

        private MarkerOptions ReturnCurrentMarkerOptions
        {
            get
            {
                if (_currentMarkerOptions != null) return _currentMarkerOptions;
                var px = 50;
                var bitmap = Bitmap.CreateBitmap(px, px, Bitmap.Config.Argb8888);
                var canvas = new Canvas(bitmap);
                var shape = CrossCurrentActivity.Current.Activity.Resources.GetDrawable(Resource.Drawable.current_circle);
                shape.SetBounds(0, 0, bitmap.Width, bitmap.Height);
                shape.Draw(canvas);
                var markerOpts = new MarkerOptions().SetIcon(BitmapDescriptorFactory.FromBitmap(bitmap)).Anchor(.5f, .5f);
                _currentMarkerOptions = markerOpts;
                return _currentMarkerOptions;
            }
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