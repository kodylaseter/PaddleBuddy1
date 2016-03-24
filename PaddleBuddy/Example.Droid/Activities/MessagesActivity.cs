using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Activities
{
    [Activity(
       Label = "Messages",
       Theme = "@style/AppTheme",
       LaunchMode = LaunchMode.SingleTop,
       Name = "example.droid.activities.AnotherFragmentHostActivity"
       )]
    public class MessagesActivity : MvxCachingFragmentCompatActivity<MessagesViewModel>
    {
        /*public override IFragmentCacheConfiguration BuildFragmentCacheConfiguration()
        {
            return new MessagesFragmentCacheConfigurationCustomFragmentInfo();
        }*/

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_messages);
        }
    }
}