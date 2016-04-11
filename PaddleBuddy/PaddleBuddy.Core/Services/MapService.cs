using System.Threading.Tasks;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models;
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
            var resp = await GetAsync("river/" + id);
            River end;
            try
            {
                end = JsonConvert.DeserializeObject<River>(resp.Data.ToString());
            }
            catch (JsonException e)
            {
                throw e;
            }
            return end;
        }
    }
}
