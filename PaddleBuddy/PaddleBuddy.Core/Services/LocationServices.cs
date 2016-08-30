using System.Threading.Tasks;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.Services
{
    public class LocationService
    {
        private static LocationService _locationService;
        private readonly ILocationProvider _locationProvider;

        public LocationService()
        {
            _locationProvider = Mvx.Resolve<ILocationProvider>();
        }

        public static LocationService GetInstance()
        {
            return _locationService ?? (_locationService = new LocationService());
        }

        public Point CurrentLocation
        {
            get
            {
                return _locationProvider.CurrentLocation;
            }
        }
    }
}
