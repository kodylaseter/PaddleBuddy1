using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;
using AlertDialog = Android.App.AlertDialog;

namespace PaddleBuddy.Droid.Services
{
    public class PermissionService
    {
        private static readonly string[] PermissionsLocation =
                {
                  Manifest.Permission.AccessCoarseLocation,
                  Manifest.Permission.AccessFineLocation
                };

        private const string Permission = Manifest.Permission.AccessFineLocation;

        public static void RequestLocation(AppCompatActivity activity)
        {
            if (!CheckLocation())
            {
                ActivityCompat.RequestPermissions(activity, PermissionsLocation, PermissionCodes.LOCATION);
            }
        }

        public static bool CheckLocation()
        {
            
            return ContextCompat.CheckSelfPermission(Application.Context, Permission) == Android.Content.PM.Permission.Granted;
        }

        public static void SetupLocation(AppCompatActivity activity)
        {
            if (!CheckLocation())
            {
                RequestLocation(activity);
            }
            else
            {
                Task.Run(() => Core.Services.LocationService.SetupLocation());
            }
        }
    }
}