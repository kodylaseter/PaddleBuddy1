using Android.OS;
using Android.Runtime;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("paddlebuddy.droid.fragments.PlanFragment")]
    public class PlanFragment : BaseFragment<PlanViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_plan;
    }
}