using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models.Map;
using System.Threading.Tasks;

namespace PaddleBuddy.Core.Services
{
    public class DatabaseService : ApiService
    {
        private static DatabaseService _databaseService;
        private List<River> _rivers;
        private List<Point> _points;
        private List<Link> _links;

        public static DatabaseService GetInstance()
        {
            return _databaseService ?? (_databaseService = new DatabaseService());
        }

        public async Task<bool> UpdateAll()
        {
            var result = await UpdatePoints() && await UpdateRivers() && await UpdateLinks();
            return result;
        }

        public List<Point> Points
        {
            get { return _points; }
            set { _points = value; }
        }


        public List<River> Rivers
        {
            get { return _rivers; }
            set { _rivers = value; }
        }


        public List<Link> Links
        {
            get { return _links; }
            set { _links = value; }
        }

        public async Task<bool> UpdatePoints()
        {
            try
            {
                var resp = await GetAsync("all_points/");
                if (resp.Success)
                {
                    Points = JsonConvert.DeserializeObject<List<Point>>(resp.Data.ToString());
                }
                else
                {
                    MessengerService.Toast(this, "Failed to update points", true);
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                MessengerService.Toast(this, "Failed to update points", true);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateRivers()
        {
            try
            {
                var resp = await GetAsync("all_rivers/");
                if (resp.Success)
                {
                    Rivers = JsonConvert.DeserializeObject<List<River>>(resp.Data.ToString());
                }
                else
                {
                    MessengerService.Toast(this, "Failed to update rivers", true);
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                MessengerService.Toast(this, "Failed to update rivers", true);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateLinks()
        {
            try
            {
                var resp = await GetAsync("all_links/");
                if (resp.Success)
                {
                    Links = JsonConvert.DeserializeObject<List<Link>>(resp.Data.ToString());
                }
                else
                {
                    MessengerService.Toast(this, "Failed to update links", true);
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                MessengerService.Toast(this, "Failed to update links", true);
                return false;
            }
            return true;
        }
    }
}
