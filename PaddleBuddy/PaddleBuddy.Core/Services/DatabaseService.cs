using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using PaddleBuddy.Core.Models.Map;
using System.Threading.Tasks;
using MvvmCross.Platform;
using PaddleBuddy.Core.DependencyServices;
using System.Linq;
using PaddleBuddy.Core.Utilities;
using PaddleBuddy.Core.Models.LinqModels;

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
        public int ClosestRiverId { get; set; }

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

        public River GetRiver(int id)
        {
            return (from river in Rivers where river.Id == id select river).Single();
        }

        public Path GetClosestRiver()
        {
            var curr = LocationService.GetInstance().CurrentLocation;
            var point = (from p in Points let dist = PBUtilities.Distance(curr, p) orderby dist ascending select p).First();
            ClosestRiverId = point.RiverId;
            return GetPath(point.RiverId);
        }

        public Point GetPoint(int id)
        {
            return (from point in Points where point.Id == id select point).Single();
        }

        public Path GetPath(int riverId)
        {
            var points = (from p in Points where p.RiverId == riverId select p).ToList();
            return new Path
            {
                RiverId = riverId,
                Points = points
            };
        }

        public Path GetPath(Point start, Point end)
        {
            //todo: clean this up
            var path = new Path();
            path.Points = new List<Point>();
            if (start.RiverId != end.RiverId)
            {
                MessengerService.Toast(this, "invalid points for path", true);
            }
            path.RiverId = start.RiverId;
            var tempList = (from p in Points where p.RiverId == start.RiverId join lnk in Links on p.Id equals lnk.Begin select new PointWithNext(p, lnk.End)).ToList();
            if (tempList.Count > 0)
            {
                var temp = (from p in tempList where p.Point.Id == start.Id select p).Single();
                if (temp == null)
                {
                    MessengerService.Toast(this, "Failed to get path", true);
                }
                else
                {
                    path.Points.Add(temp.Point);
                } 
                while (temp != null && temp.Point.Id != end.Id)
                {
                    temp = (from p in tempList where p.Point.Id == temp.Next select p).First();
                    if (temp != null) path.Points.Add(temp.Point);
                    else
                    {
                        MessengerService.Toast(this, "Failed to get path", true);
                        break;
                    }
                }
                
            }
            return path;
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
