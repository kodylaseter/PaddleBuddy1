using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Activities
{
    [Activity(
        Label = "Intro",
        Theme = "@style/AppTheme.Login",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize,
        Name = "paddlebuddy.droid.activities.IntroActivity"
    )]			
    public class IntroActivity : MvxAppCompatActivity<IntroViewModel>
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);
            SetContentView ( Resource.Layout.activity_intro );

            Messenger = Mvx.Resolve<IMvxMessenger>();
            Messenger.Subscribe<ToastMessage>(DisplayToast);
        }

        public void DisplayToast(ToastMessage message)
        {
            Toast.MakeText(this, message.Text, message.IsShort ? ToastLength.Short : ToastLength.Long).Show();
        }

        protected IMvxMessenger Messenger { get; private set; }


    }
}

