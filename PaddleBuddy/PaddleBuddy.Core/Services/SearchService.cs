using Newtonsoft.Json;
using PaddleBuddy.Core.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaddleBuddy.Core.Services
{
    public class SearchService : ApiService
    {
        private static SearchService _searchService = new SearchService();
        public List<River> Data { get; set; }

        public static SearchService GetInstance()
        {
            return _searchService;
        }
        
        public SearchService()
        {
            GetData();
        }

        private async void GetData()
        {
            try
            {
                var resp = await GetAsync("rivers");
                Data = JsonConvert.DeserializeObject<List<River>>(resp.Data.ToString());
            } catch (Exception e)
            {
                throw e;
            }
        }


    }
}
