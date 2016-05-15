using Android.App;
using Android.Content;
using Android.Net;
using Android.Views.Accessibility;
using PaddleBuddy.Core.DependencyServices;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class NetworkAndroid : BaseDependencyServiceAndroid, INetwork
    {
        bool INetwork.IsOnline
        {
            get
            {
                var cm =
                (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                var netInfo = cm.ActiveNetworkInfo;
                return netInfo != null && netInfo.IsConnectedOrConnecting;
            }
        }
    }
}