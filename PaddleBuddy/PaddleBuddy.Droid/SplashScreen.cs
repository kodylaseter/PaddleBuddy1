using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;
using PaddleBuddy.Droid.Services;
using Plugin.CurrentActivity;

namespace PaddleBuddy.Droid
{
    [Activity(
        Label = "PaddleBuddy"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/AppTheme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.splash_screen)
        {

        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        private async void SetupLocation()
        {
            Core.Services.LocationService.GetInstance().StartListening();
            await Core.Services.LocationService.GetInstance().GetLocationAsync();
        }
    }
}