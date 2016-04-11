
using System;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;
using PaddleBuddy.Core.ViewModels;
using Debug = System.Diagnostics.Debug;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("paddlebuddy.droid.fragments.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback
    {
        private SupportMapFragment _fragment;
        private GoogleMap _map;
        protected override int FragmentId => Resource.Layout.fragment_map;
        private static LatLng BRISBANE = new LatLng(-27.47093, 153.0235);

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _fragment  = new SupportMapFragment();
            _fragment.GetMapAsync(this);
            FragmentTransaction trans = ChildFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.map_container, _fragment).Commit();
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            //_map.AddMarker(new MarkerOptions().SetPosition(BRISBANE).SetTitle("Brisbane"));
            _map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(33.7490, -84.3880), 10.0f));
            DrawLine();
        }

        public async void DrawLine()
        {
            var river = await MapService.GetInstance().GetRiver(1);
            var p0 = river.Points[0];
            var p1 = river.Points[1];
            _map.AddMarker(new MarkerOptions().SetTitle("test").SetPosition(new LatLng(p0.Lat, p0.Lng)));
            _map.AddPolyline(new PolylineOptions().Add(new LatLng(p0.Lat, p0.Lng), new LatLng(p1.Lat, p1.Lng)).InvokeColor(Resource.Color.black).InvokeWidth(4));
            //var a = new LatLng();
        }
    }
}