
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
            var resp = MapService.GetInstance().GetRiver(1);
            Log.Info("pb", resp.ToString());
        }
    }
}