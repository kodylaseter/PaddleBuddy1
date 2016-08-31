using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace PaddleBuddy.Core.Services
{
    public static class PermissionService
    {
        public static async Task<bool> CheckOrRequestPermission(Permission permission)
        {
            //var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            //if (status != PermissionStatus.Granted)
            //{
            //    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            //    status = results[permission];
            //}
            //if (status == PermissionStatus.Granted)
            //{
            //    return true;
            //}
            //MessengerService.Toast(null, "Error requesting permission, try again", true);
            //return false;


            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                    status = results[permission];
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            MessengerService.Toast(null, "Error requesting permission, try again", true);
            return false;
        }
    }
}
