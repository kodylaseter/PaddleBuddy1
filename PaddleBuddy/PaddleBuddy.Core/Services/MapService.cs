using PaddleBuddy.Core.Models.Map;
using System.Linq;
using PaddleBuddy.Core.Utilities;

namespace PaddleBuddy.Core.Services
{
    public class MapService : ApiService
    {
        private static MapService _mapService;
        public int ClosestRiverId { get; set; }

        public static MapService GetInstance()
        {
            return _mapService ?? (_mapService = new MapService());
        }

        public River GetRiver(int id)
        {
            return (from river in DatabaseService.GetInstance().Rivers where river.Id == id select river).Single();
        }

        public Path GetClosestRiver()
        {
            var curr = LocationService.GetInstance().GetCurrentLocation();
            var point = (from p in DatabaseService.GetInstance().Points let dist = PBUtilities.Distance(curr, p) orderby dist ascending select p).First();
            ClosestRiverId = point.RiverId;
            return GetPath(point.RiverId);
        }

        public Point GetPoint(int id)
        {
            return (from point in DatabaseService.GetInstance().Points where point.Id == id select point).Single();
        }

        public Path GetPath(int riverId)
        {
            var points = (from p in DatabaseService.GetInstance().Points where p.RiverId == riverId select p).ToList();
            return new Path
            {
                RiverId = riverId,
                Points = points
            };
        }
    }
}
