using Newtonsoft.Json;
using PaddleBuddy.Core.Models.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;

namespace PaddleBuddy.Core.Services
{
    public class SearchService : ApiService
    {
        private static readonly SearchService _searchService = new SearchService();
        public List<SearchItem> Data { get; set; }

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
                Data = new List<SearchItem>();
                if (resp.Success)
                {
                    foreach (var item in JsonConvert.DeserializeObject<List<River>>(resp.Data.ToString()))
                    {
                        Data?.Add(new SearchItem { SearchString = item.Name, Item = item });
                    }
                }
            } catch (Exception)
            {
                Messenger.Publish(new ToastMessage(this, "Failed to get search data", true));
            }
        }


    }
}
