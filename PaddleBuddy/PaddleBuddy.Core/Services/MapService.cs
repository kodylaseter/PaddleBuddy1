using PaddleBuddy.Core.Models.Map;
using System.Linq;
using PaddleBuddy.Core.Utilities;

namespace PaddleBuddy.Core.Services
{
    public class MapService : ApiService
    {
        private static MapService _mapService;

        public static MapService GetInstance()
        {
            return _mapService ?? (_mapService = new MapService());
        }


    }
}
