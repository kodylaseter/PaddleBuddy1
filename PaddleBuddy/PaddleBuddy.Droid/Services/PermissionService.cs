using Android;
using Android.App;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using PaddleBuddy.Core.Models;

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
    }
}