
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("paddlebuddy.droid.fragments.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_map;

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    var ignored = base.OnCreateView(inflater, container, savedInstanceState);

        //    var view = this.BindingInflate(Resource.Layout.fragment_map, null);

        //    if (view != null)
        //    {
        //        ViewGroup parent = (ViewGroup)view.getParent();
        //        if (parent != null)
        //            parent.removeView(view);
        //    }
        //    try
        //    {
        //        view = inflater.inflate(R.layout.map, container, false);
        //    }
        //    catch (InflateException e)
        //    {
        //        /* map is already there, just return view as it is */
        //    }

        //    return View;
        //}
    }
}