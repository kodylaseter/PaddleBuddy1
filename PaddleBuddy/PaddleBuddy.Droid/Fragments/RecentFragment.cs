using Android.Runtime;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("paddlebuddy.droid.fragments.RecentFragment")]
    public class RecentFragment : BaseFragment<RecentViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_recent;
    }
}