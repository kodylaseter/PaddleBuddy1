using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models.Map;
using System.Threading.Tasks;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;

namespace PaddleBuddy.Core.Services
{
    public class DatabaseService : ApiService
    {
        private static DatabaseService _databaseService;
        private List<River> _rivers;
        private List<Point> _points;
        private List<Link> _links;
        private IStorageService storageService;
        private string[] names = new[] { "points", "rivers", "links" };

        public static DatabaseService GetInstance()
        {
            return _databaseService ?? (_databaseService = new DatabaseService());
        }

        public async Task<bool> Setup(bool sync = false)
        {
            if (storageService == null) storageService = Mvx.Resolve<IStorageService>();
            if (sync) return await UpdateAll();
            if (storageService.HasData(names))
            {
                Points = JsonConvert.DeserializeObject<List<Point>>(storageService.ReadSerializedFromFile("points"));
                Rivers = JsonConvert.DeserializeObject<List<River>>(storageService.ReadSerializedFromFile("rivers"));
                Links = JsonConvert.DeserializeObject<List<Link>>(storageService.ReadSerializedFromFile("links"));
            }
            var isUpdated = (Points != null && Rivers != null && Links != null && Points.Count > 0 && Rivers.Count > 0 && Links.Count > 0);
            MessengerService.Toast(this, isUpdated ? "Data obtained from local copy" : "Data not updated", false);
            return isUpdated ? true : await UpdateAll();
        }

        public async Task<bool> UpdateAll()
        {
            var result = await UpdateRivers() && await UpdateLinks() && await UpdatePoints();
            Task.Run(() => SaveData());
            return result;
        }

        public void SaveData()
        {
            var points = JsonConvert.SerializeObject(_points);
            var rivers = JsonConvert.SerializeObject(_rivers);
            var links = JsonConvert.SerializeObject(_links);
            storageService.SaveSerializedToFile(points, "points");
            storageService.SaveSerializedToFile(rivers, "rivers");
            storageService.SaveSerializedToFile(links, "links");
        }

        public bool IsReady
        {
            get
            {
                return (_rivers != null && _rivers.Count > 0) &&
                    (_points != null && _points.Count > 0) &&
                    (_links != null && _links.Count > 0);
            }
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
            Stopwatch stop = new Stopwatch();
            stop.Start();
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
            stop.Stop();
            Debug.WriteLine("pbuddy points: " + stop.Elapsed);
            return true;
        }

        public async Task<bool> UpdateRivers()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
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
            stop.Stop();
            Debug.WriteLine("pbuddy rivers: " + stop.Elapsed);
            return true;
        }

        public async Task<bool> UpdateLinks()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
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
            stop.Stop();
            Debug.WriteLine("pbuddy links: " + stop.Elapsed);
            return true;
        }
    }
}
