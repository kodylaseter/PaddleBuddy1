using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Activities
{
    [Activity(
        Label = "Login",
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
            SetContentView ( Resource.Layout.activity_register );
        }
    }
}

