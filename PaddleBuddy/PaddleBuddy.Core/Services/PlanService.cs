using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;

namespace PaddleBuddy.Core.Services
{
    public class PlanService : ApiService
    {
        private static PlanService _planService;

        public static PlanService GetInstance()
        {
            return _planService ?? (_planService = new PlanService());
        }

        public async Task<Response> EstimateTime(int startID, int endID, int riverID)
        {
            Response resp = new Response();
            try
            {
                resp = await GetAsync("estimate_time/", new {p1 = startID, p2 = endID, river = riverID});
                if (resp.Success)
                {
                    resp.Data = JsonConvert.DeserializeObject<TimeEstimate>(resp.Data.ToString());
                }
            }
            catch (JsonException)
            {
                Mvx.Resolve<IMvxMessenger>().Publish(new ToastMessage(this, "Failed estimate_time call!", true));
            }
            return resp;
        }
    }
}
