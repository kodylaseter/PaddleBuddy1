using System.Threading.Tasks;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models;
using System;
using System.Collections.Generic;
using PaddleBuddy.Core.Models.LinqModels;
using PaddleBuddy.Core.Utilities;
using System.Linq;

namespace PaddleBuddy.Core.Services
{
    public class PlanService : ApiService
    {
        private static PlanService _planService;

        public static PlanService GetInstance()
        {
            return _planService ?? (_planService = new PlanService());
        }

        //public async Task<Response> EstimateTime(int startID, int endID, int riverID)
        //{
        //    Response resp = new Response();
        //    try
        //    {
        //        resp = await GetAsync("estimate_time/", new {p1 = startID, p2 = endID, river = riverID});
        //        if (resp.Success)
        //        {
        //            resp.Data = JsonConvert.DeserializeObject<TripEstimate>(resp.Data.ToString());
        //        }
        //    }
        //    catch (JsonException)
        //    {
        //        MessengerService.Toast(this, "Failed estimate_time call!", true);
        //    }
        //    return resp;
        //}

        public TripEstimate EstimateTrip(int startId, int endId, int riverId)
        {
            var trip = new TripEstimate();
            var beginList = (from point in DatabaseService.GetInstance().Points where point.RiverId == riverId select new { BeginId = point.Id, BeginLat = point.Lat, BeginLng = point.Lng }).ToList();
            var endList = (from point in DatabaseService.GetInstance().Points where point.RiverId == riverId select new { EndId = point.Id, EndLat = point.Lat, EndLng = point.Lng }).ToList();
            var result = (from link in DatabaseService.GetInstance().Links join p1 in beginList on link.Begin equals p1.BeginId join p2 in endList on link.End equals p2.EndId select new LinkPoint(link.Id, link.Begin, link.End, link.Speed, link.RiverId, (float)p1.BeginLat, (float)p1.BeginLng, (float)p2.EndLat, (float)p2.EndLng)).ToList();

            var temp = (from first in result where first.Begin == startId select first).First();
            if (temp == null)
            {
                MessengerService.Toast(this, "Could not find first point", true);
                return trip;
            }
            int newId;
            var list = new List<LinkPoint>();
            while (temp != null && temp.End != endId)
            {
                list.Add(temp);
                newId = temp.End;
                result.Remove(temp);
                temp = (from f in result where f.Begin == newId select f).First();
            }
            if (temp == null)
            {
                MessengerService.Toast(this, "Did not reach end point", true);
                return trip;
            }
            if (temp.End == endId)
            {
                list.Add(temp);
            }
            if (list.ElementAt(0).Begin == startId && list.Last().End == endId)
            {
                trip = PBUtilities.LinksToEstimate(result);
                trip.RiverId = riverId;
                trip.StartId = startId;
                trip.EndId = endId;
            }
            return trip;
        }
    }
}
