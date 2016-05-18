using System;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Services;
using PaddleBuddy.Core.ViewModels;
using PaddleBuddy.Droid.DependencyServices;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("paddlebuddy.droid.fragments.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback
    {
        public SupportMapFragment Fragment { get; set; }

        protected override int FragmentId => Resource.Layout.fragment_map;

        public EventHandler<GoogleMap.MyLocationChangeEventArgs> Handler;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            Fragment  = new SupportMapFragment();
            Fragment.GetMapAsync(this);
            var trans = ChildFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.map_container, Fragment).Commit();
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.MyLocationEnabled = true;
            googleMap.MyLocationChange += LocationChanged;
            ((MapDrawerAndroid) Mvx.Resolve<IMapDrawer>()).Map = googleMap;
            ViewModel.MapReady = true;
            ViewModel.Setup();
        }

        public void LocationChanged(object sender, GoogleMap.MyLocationChangeEventArgs eventArgs)
        {
            ViewModel.LocationChanged(new Point
            {
                Lat = eventArgs.Location.Latitude,
                Lng = eventArgs.Location.Longitude
            });
        }
    }
}