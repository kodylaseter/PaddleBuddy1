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
using MvvmCross.Binding.BindingContext;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("paddlebuddy.droid.fragments.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback, GoogleMap.IOnMarkerClickListener
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

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            var mapView = (FrameLayout)View.FindViewById(Resource.Id.map_container);

            var layout = new LinearLayout(View.Context);
            layout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            var button = new Button(View.Context);
            button.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            button.Text = "Test";

            var set = this.CreateBindingSet<MapFragment, MapViewModel>();
            set.Bind(button).For("Click").To(vm => vm.TestCommand);
            set.Apply();
            //layout.AddView(button);
            mapView.AddView(button);
            base.OnViewCreated(view, savedInstanceState);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.MyLocationEnabled = true;
            googleMap.MyLocationChange += LocationChanged;
            //googleMap.SetOnMarkerClickListener(this);
            
            //googleMap.SetInfoWindowAdapter(new CustomMarkerWindowAdapter(GetLayoutInflater(null)));
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

        public bool OnMarkerClick(Marker marker)
        {
            try
            {
                var id = int.Parse(marker.Snippet);
                var p = MapService.GetInstance().GetPoint(id);
                marker.ShowInfoWindow();
                return true;

            } catch (Exception)
            {
                MessengerService.Toast(this, "Failed to get id of marker", true);
            }
            return false;
        }

        public class CustomMarkerWindowAdapter : Java.Lang.Object, GoogleMap.IInfoWindowAdapter
        {
            private LayoutInflater _layoutInflater;

            public CustomMarkerWindowAdapter(LayoutInflater layoutInflater)
            {
                _layoutInflater = layoutInflater;
            }

            public View GetInfoContents(Marker marker)
            {
                return _layoutInflater.Inflate(Resource.Layout.infowindow_custom_marker, null);
            }

            public View GetInfoWindow(Marker marker)
            {
                return null;
            }
        }
    }
}