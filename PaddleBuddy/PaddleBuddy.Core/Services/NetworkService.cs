using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;

namespace PaddleBuddy.Core.Services
{
    public class NetworkService
    {
        private static NetworkService _networkService;
        private readonly INetwork _network;

        public NetworkService()
        {
            _network = Mvx.Resolve<INetwork>();
        }

        public static NetworkService GetInstance()
        {
            return _networkService ?? (_networkService = new NetworkService());
        }

        public bool IsOnline
        {
            get
            {
                return _network.IsOnline;
            }
        }
    }
}
