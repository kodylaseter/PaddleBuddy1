using System.Security.Authentication.ExtendedProtection;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Services;
using PaddleBuddy.Core.ViewModels;
using PaddleBuddy.Droid.Activities;
using PaddleBuddy.Droid.DependencyServices;
using PaddleBuddy.Droid.Utilities;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("paddlebuddy.droid.fragments.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback
    {
        private SupportMapFragment _fragment;
        private GoogleMap _map;
        protected override int FragmentId => Resource.Layout.fragment_map;

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
            Mvx.RegisterSingleton<IMapDrawer>(new MapDrawerAndroid(googleMap));
            ViewModel.Setup();
        }
    }
}