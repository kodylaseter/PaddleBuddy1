using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Fragment  = new SupportMapFragment();
            Fragment.GetMapAsync(this);
            FragmentTransaction trans = ChildFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.map_container, Fragment).Commit();
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            ((MapDrawerAndroid) Mvx.Resolve<IMapDrawer>()).Map = googleMap;
            ViewModel.MapReady = true;
            ViewModel.Setup();
        }
    }
}