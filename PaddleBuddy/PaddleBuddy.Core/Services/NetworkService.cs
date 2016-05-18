using System;
using System.ServiceModel;
using System.Threading.Tasks;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using Plugin.Connectivity;

namespace PaddleBuddy.Core.Services
{
    public class NetworkService
    {
        private static INetwork _network;
        private static INetwork Network
        {
            get { return _network ?? (_network = Mvx.Resolve<INetwork>()); }
        }

        public static bool IsOnline
        {
            get { return Network.IsOnline; }
        }

        public static bool IsServerAvailable
        {
            get
            {
                return Network.IsServerAvailable;
            }
        }
    }
}
