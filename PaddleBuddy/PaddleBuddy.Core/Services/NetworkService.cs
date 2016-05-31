using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using Plugin.Connectivity;

namespace PaddleBuddy.Core.Services
{
    public class NetworkService
    {
        public static bool IsOnline
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }

        public static bool IsServerAvailable
        {
            get
            {
                return IsOnline;
            }
        }
    }
}
