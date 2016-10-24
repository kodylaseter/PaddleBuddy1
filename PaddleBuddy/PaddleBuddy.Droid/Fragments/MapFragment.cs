using System;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.ViewModels;
using PaddleBuddy.Droid.DependencyServices;
using PaddleBuddy.Core.Services;
using Android.Widget;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Droid.Services;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("paddlebuddy.droid.fragments.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback, GoogleMap.IOnMarkerClickListener, GoogleMap.IOnMapClickListener, GoogleMap.IInfoWindowAdapter
    {
        public SupportMapFragment Fragment { get; set; }
        public GoogleMap GoogleMap { get; set; }
        protected override int FragmentId => Resource.Layout.fragment_map;
        public EventHandler<GoogleMap.MyLocationChangeEventArgs> Handler;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            Fragment  = new SupportMapFragment();
            Fragment.GetMapAsync(this);
            var trans = ChildFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.map_container, Fragment).Commit();
            Mvx.Resolve<IMvxMessenger>().Subscribe<PermissionMessage>(AfterMapReadyAndPermission);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            GoogleMap = googleMap;
            ((MapDrawerAndroid)Mvx.Resolve<IMapDrawer>()).Map = GoogleMap;
            if (PermissionService.CheckLocation())
            {
                AfterMapReadyAndPermission();
            }
        }

        private void AfterMapReadyAndPermission(PermissionMessage permissionMessage = null)
        {
            GoogleMap.SetOnMapClickListener(this);
            GoogleMap.SetOnMarkerClickListener(this);
            GoogleMap.SetInfoWindowAdapter(this);
            ViewModel.SelectedMarker = null;
            ViewModel.MapReady = true;
            ViewModel.Setup();
            ViewModel.SetupAsync();
        }

        //public void LocationChanged(object sender, GoogleMap.MyLocationChangeEventArgs eventArgs)
        //{
        //    var point = new Point
        //    {
        //        Lat = eventArgs.Location.Latitude,
        //        Lng = eventArgs.Location.Longitude
        //    };
        //    _locationProvider.CurrentLocation = point;
        //    Mvx.Resolve<IMvxMessenger>().Publish(new LocationChangedMessage(this));
        //}

        public bool OnMarkerClick(Marker marker)
        {
            try
            {
                var id = int.Parse(marker.Snippet);
                ViewModel.SelectedMarker = DatabaseService.GetInstance().GetPoint(id);
                marker.ShowInfoWindow();
                return true;

            }
            catch (Exception)
            {
                MessengerService.Toast(this, "Failed to get id of marker", true);
            }
            return false;
        }

        public void OnMapClick(LatLng point)
        {
            ViewModel.SelectedMarker = null;
        }

        public View GetInfoContents(Marker marker)
        {
            var view = GetLayoutInflater(null).Inflate(Resource.Layout.infowindow_custom_marker, null);
            ((TextView)view.FindViewById(Resource.Id.markerTitle)).Text = ViewModel.SelectedMarker.Label;
            return view;
        }

        public View GetInfoWindow(Marker marker)
        {
            return null;
        }

        //public class CustomMarkerWindowAdapter : Java.Lang.Object, GoogleMap.IInfoWindowAdapter
        //{
        //    private LayoutInflater _layoutInflater;

        //    public CustomMarkerWindowAdapter(LayoutInflater layoutInflater)
        //    {
        //        _layoutInflater = layoutInflater;
        //    }

        //    public View GetInfoContents(Marker marker)
        //    {
        //        var view = _layoutInflater.Inflate(Resource.Layout.infowindow_custom_marker, null);
        //        ((TextView)view.FindViewById(Resource.Id.markerTitle)).Text = 
        //        return view;
        //    }

        //    public View GetInfoWindow(Marker marker)
        //    {
        //        return null;
        //    }
        //}
    }
}