using System;
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
            Data = items;
        }

        public ObservableCollection<SearchItem> Filter(string searchText)
        {
            var filteredList = new ObservableCollection<SearchItem>();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                try
                {
                    filteredList = new ObservableCollection<SearchItem>(Data.Where(w => !string.IsNullOrWhiteSpace(w.SearchString) && w.SearchString.Contains(searchText)));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return filteredList;
        }

        public static ObservableCollection<SearchItem> ArrayToSearchSource(object[] arr)
        {
            var list = new ObservableCollection<SearchItem>();
            foreach(var item in arr)
            {
                if (item.GetType() == typeof(River))
                {
                    var river = item as River;
                    if (river != null)
                    {
                        list.Add(new SearchItem
                        {
                            SearchString = river.Name,
                            Item = river
                        });
                    }
                }
                else if (item.GetType() == typeof (Point))
                {
                    var point = item as Point;
                    if (point != null)
                    {
                        list.Add(new SearchItem
                        {
                            SearchString = point.Label,
                            Item = point
                        });
                    }
                }
            }
            return list;
        }
    }
}
