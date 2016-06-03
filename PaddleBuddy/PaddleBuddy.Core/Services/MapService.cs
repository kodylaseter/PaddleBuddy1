using System.Threading.Tasks;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models.Map;
using System.Linq;
using System;

namespace PaddleBuddy.Core.Services
{
    public class MapService : ApiService
    {
        private static MapService _mapService;

        public static MapService GetInstance()
        {
            return _mapService ?? (_mapService = new MapService());
        }

        public River GetRiver(int id)
        {
            return (from river in DatabaseService.GetInstance().Rivers where river.Id == id select river).Single();
        }

        public River GetClosestRiver()
        {
            return new River();
            //var newPi = Math.PI / 180;
            //var point = LocationService.GetInstance().GetCurrentLocation();
            //var river = from p in DatabaseService.GetInstance().Points where ((Math.Acos(Math.Sin(point.Lat + newPi) * Math.Sin(p.Lat * newPi) + Math.Cos(point.Lat + newPi) * Math.Cos(p.Lat * newPi) * Math.Cos(point.Lng - p.Lng) * newPi))
        }

        public Point GetPoint(int id)
        {
            return (from point in DatabaseService.GetInstance().Points where point.Id == id select point).Single();
        }
    }
}
