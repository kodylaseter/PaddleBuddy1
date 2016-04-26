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
        Label = "Register",
        Theme = "@style/AppTheme.Login",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize,
        Name = "paddlebuddy.droid.activities.RegisterActivity"
    )]			
    public class RegisterActivity : MvxAppCompatActivity<RegisterViewModel>
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);
            Messenger = Mvx.Resolve<IMvxMessenger>();
            Messenger.Subscribe<ToastMessage>(DisplayToast);
            SetContentView ( Resource.Layout.activity_register );
        }

        public void DisplayToast(ToastMessage message)
        {
            Toast.MakeText(this, message.Text, message.IsShort ? ToastLength.Short : ToastLength.Long).Show();
        }

        protected IMvxMessenger Messenger { get; private set; }
    }
}

