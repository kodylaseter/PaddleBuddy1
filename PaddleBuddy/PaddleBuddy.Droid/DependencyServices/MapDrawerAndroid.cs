using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Java.Lang;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Models.Messages;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class MapDrawerAndroid : IMapDrawer
    {
        public MapDrawerAndroid(object map)
        {
            Map = map as GoogleMap;
            if (Map == null)
            {
                Mvx.Resolve<IMvxMessenger>().Publish(new ToastMessage(this, "Map is null!", false));
            }
        }

        public GoogleMap Map { get; set; }

        public void DrawLine(object _points, int width = 7, int color = Resource.Color.dark_gray,
            int zindex = 1)
        {
            Map.AddPolyline(
                    new PolylineOptions().AddAll(_points as IIterable)
                        .InvokeColor(color)
                        .InvokeWidth(width)
                        .InvokeColor(color)
                        .InvokeZIndex(zindex));
        }

        public void DrawMarker(Point p)
        {
            Map.AddMarker(new MarkerOptions().SetPosition(new LatLng(p.Lat, p.Lng)).SetTitle("test"));
        }

        public void MoveCamera(Point p)
        {
            Map.AnimateCamera(CameraUpdateFactory.NewLatLng(new LatLng(p.Lat, p.Lng)));
        }

        public void MoveCameraZoom(Point p, int zoom)
        {
            Map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(p.Lat, p.Lng), zoom));
        }
    }
}