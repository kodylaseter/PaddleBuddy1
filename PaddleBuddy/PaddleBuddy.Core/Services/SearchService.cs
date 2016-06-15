using System.Collections.Generic;
using PaddleBuddy.Core.Models;
using System.Collections.ObjectModel;
using PaddleBuddy.Core.Models.Map;
using System.Linq;

namespace PaddleBuddy.Core.Services
{
    public class SearchService
    {
        public ObservableCollection<SearchItem> Data { get; set; }
        public SearchService()
        {
            Data = new ObservableCollection<SearchItem>();
        }

        public void SetData(ObservableCollection<SearchItem> items)
        {
            if (Data.Count > 0) Data.Clear();
            foreach (var item in DatabaseService.GetInstance().Rivers)
            {
                Data.Add(new SearchItem { SearchString = item.Name, Item = item });
            }
        }

        public ObservableCollection<SearchItem> Filter(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) return new ObservableCollection<SearchItem>();
            return new ObservableCollection<SearchItem>(Data.Where(w => w.SearchString.Contains(searchText)));
        }

        public static ObservableCollection<SearchItem> ArrayToSearchSource(object[] arr)
        {
            var list = new ObservableCollection<SearchItem>();
            foreach(var item in arr)
            {
                if (item.GetType() == typeof(River))
                {
                    var river = item as River;
                    list.Add(new SearchItem
                    {
                        SearchString = river.Name,
                        Item = river
                    });
                }
                else
                {
                    var a = 4;
                }
            }
            return list;
        }
    }
}
