using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Java.Lang;
using PaddleBuddy.Core.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace PaddleBuddy.Droid.Services
{
    public class PermissionService
    {
        public static async Task<bool> CheckOrRequestLocation()
        {
            //string[] permissionsLocation =
            //    {
            //      Manifest.Permission.AccessCoarseLocation,
            //      Manifest.Permission.AccessFineLocation
            //    };
            //int requestLocationId = 0;
            //string permission = Manifest.Permission.AccessFineLocation;
            //if (ContextCompat.CheckSelfPermission(Application.Context, permission) == Permission.Granted) return true;
            //ActivityCompat.RequestPermissions(activity, permissionsLocation, requestLocationId);
            //return true;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("PBUDDY", "Error getting permission: " + ex);
            }
            MessengerService.Toast(null, "Location services required, closing app", true);
            return false;
        }
    }
}