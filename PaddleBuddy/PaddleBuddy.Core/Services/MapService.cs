using System.Threading.Tasks;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Models.Messages;

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
            River end;
            try
            {
                var resp = await GetAsync("river/" + id);
                end = JsonConvert.DeserializeObject<River>(resp.Data.ToString());
            }
            catch (JsonException e)
            {
                throw e;
            }
            return end;
        }

        public async Task<River> GetClosestRiver()
        {
            River result = new River();
            try
            {
                var resp = await PostAsync("closest_river/", LocationService.GetInstance().GetCurrentLocation());
                if (resp.Success)
                {
                    result = JsonConvert.DeserializeObject<River>(resp.Data.ToString());
                }
                else
                {
                    Mvx.Resolve<IMvxMessenger>().Publish(new ToastMessage(this, "Failed api call!", true));
                }
            }
            catch (JsonException e)
            {
                throw e;
            }
            return result;
        }
    }
}
