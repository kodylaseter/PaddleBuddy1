using Android.Runtime;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("paddlebuddy.droid.fragments.PreferencesFragment")]
    public class PreferencesFragment : BaseFragment<PreferencesViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_preferences;
    }
}