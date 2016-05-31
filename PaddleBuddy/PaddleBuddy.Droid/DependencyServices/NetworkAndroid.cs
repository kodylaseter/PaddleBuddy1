using PaddleBuddy.Core;
using PaddleBuddy.Core.DependencyServices;
using Plugin.Connectivity;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class NetworkAndroid : BaseDependencyServiceAndroid, INetwork
    {
        public bool IsOnline
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }

        public bool IsServerAvailable
        {
            get
            {
                //TODO: refactor this into core service
                //consider using reachability.net package

                //return IsOnline && CrossConnectivity.Current.IsRemoteReachable(SysPrefs.ApiBase).Result;
                var a = IsOnline;
                var b = CrossConnectivity.Current.IsRemoteReachable(SysPrefs.Website).Result;
                return a && b;
            }
        }
    }
}