using System.Threading.Tasks;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.Services
{
    public class MapService : ApiService
    {
        private static MapService _mapService;

        public static MapService GetInstance()
        {
            return _mapService ?? (_mapService = new MapService());
        }

        public async Task<River> GetRiver(int id)
        {
            var end = new River();
            try
            {
                var resp = await GetAsync("river/" + id);
                if (resp.Success)
                {
                    end = JsonConvert.DeserializeObject<River>(resp.Data.ToString());
                }
                else
                {
                    MessengerService.Toast(this, "Failed GetRiver api call!", true);
                }
            }
            catch (JsonException e)
            {
                throw e;
            }
            return end;
        }

        public async Task<River> GetClosestRiver()
        {
            var result = new River();
            try
            {
                var resp = await PostAsync("closest_river/", LocationService.GetInstance().GetCurrentLocation());
                if (resp.Success)
                {
                    result = JsonConvert.DeserializeObject<River>(resp.Data.ToString());
                }
                else
                {
                    MessengerService.Toast(this, "Failed GetClosestRiver api call!", true);
                }
            }
            catch (JsonException e)
            {
                MessengerService.Toast(this, "Failed GetClosestRiver api call!", true);
            }
            return result;
        }

        public async Task<Point> GetPoint(int id)
        {
            var p = new Point();
            try
            {
                var resp = await GetAsync("point/" + id);
                if (resp.Success)
                {
                    p = JsonConvert.DeserializeObject<Point>(resp.Data.ToString());
                }
                else
                {
                    MessengerService.Toast(this, "Failed GetPoint API call", true);
                }
            }
            catch (JsonException)
            {
                MessengerService.Toast(this, "Failed GetPoint API call", true);
            }
            return p;
        }
    }
}
