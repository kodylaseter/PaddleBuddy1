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
            var end = JsonConvert.DeserializeObject<River>(resp.Data.ToString());
            return end;
        }
    }
}
