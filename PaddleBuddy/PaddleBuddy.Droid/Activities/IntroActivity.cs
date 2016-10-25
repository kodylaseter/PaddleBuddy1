using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.ViewModels;
using PaddleBuddy.Droid.Services;

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
            PermissionService.SetupLocation(this);
        }

        public void DisplayToast(ToastMessage message)
        {
            Toast.MakeText(this, message.Text, message.IsShort ? ToastLength.Short : ToastLength.Long).Show();
        }

        protected IMvxMessenger Messenger { get; private set; }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch (requestCode)
            {
                case PermissionCodes.LOCATION:
                {
                    if (grantResults == null || grantResults.Length < 1 || grantResults[0] != Permission.Granted)
                    {
                        var alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Permission Required");
                        alert.SetMessage("Location services are required. Please approve the request.");
                        alert.SetPositiveButton("Ok", (sendAlert, args) =>
                        {
                            PermissionService.SetupLocation(this);
                        });
                        alert.SetNegativeButton("Quit", (senderAler, args) =>
                        {
                            FinishAffinity();
                        });
                        var dialog = alert.Create();
                        dialog.Show();
                    }
                    else
                    {
                        Mvx.Resolve<IMvxMessenger>().Publish(new PermissionMessage(this, "location", true));
                    }
                    break;
                }
            }
        }
    }
}

