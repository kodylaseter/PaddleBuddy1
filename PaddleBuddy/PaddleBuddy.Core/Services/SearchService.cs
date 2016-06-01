using System.Collections.Generic;
using PaddleBuddy.Core.Models;

namespace PaddleBuddy.Core.Services
{
    public class SearchService
    {
        private static readonly SearchService _searchService = new SearchService();
        public List<SearchItem> Data { get; set; }

        public static SearchService GetInstance()
        {
            return _searchService;
        }
        
        public SearchService()
        {
            SetData();
        }

        private void SetData()
        {
            foreach (var item in DatabaseService.GetInstance().Rivers)
            {
                Data?.Add(new SearchItem { SearchString = item.Name, Item = item });
            }
            //try
            //{
            //    var resp = await GetAsync("all_rivers");
            //    Data = new List<SearchItem>();
            //    if (resp.Success)
            //    {

            //    }
            //} catch (Exception)
            //{
            //    MessengerService.Toast(this, "Failed to get search data", true);
            //}
        }


    }
}
