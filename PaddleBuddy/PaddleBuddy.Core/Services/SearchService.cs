using System.Collections.Generic;
using PaddleBuddy.Core.Models;

namespace PaddleBuddy.Core.Services
{
    public class SearchService
    {
        private static SearchService _searchService;
        private List<SearchItem> _data;

        public static SearchService GetInstance()
        {
            return _searchService ?? (_searchService = new SearchService());
        }
        
        public SearchService()
        {
            Data = new List<SearchItem>();
            SetData();
        }

        private void SetData()
        {
            if (Data.Count > 0) Data.Clear();
            foreach (var item in DatabaseService.GetInstance().Rivers)
            {
                Data.Add(new SearchItem { SearchString = item.Name, Item = item });
            }
        }

        public List<SearchItem> Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
