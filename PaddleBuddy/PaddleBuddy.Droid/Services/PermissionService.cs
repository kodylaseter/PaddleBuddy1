using Android;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using PaddleBuddy.Core.Models;

namespace PaddleBuddy.Droid.Services
{
    public class PermissionService
    {
        public static bool CheckOrRequestLocation(AppCompatActivity activity)
        {
            string[] permissionsLocation =
                {
                  Manifest.Permission.AccessCoarseLocation,
                  Manifest.Permission.AccessFineLocation
                };
            string permission = Manifest.Permission.AccessFineLocation;
            if (ContextCompat.CheckSelfPermission(Application.Context, permission) == Permission.Granted) return true;
            ActivityCompat.RequestPermissions(activity, permissionsLocation, PermissionCodes.LOCATION);
            return false;
        }
    }
}