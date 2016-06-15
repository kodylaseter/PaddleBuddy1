using System;
using System.Collections.ObjectModel;
using System.Globalization;
using MvvmCross.Platform.Converters;
using PaddleBuddy.Core.Models;

namespace PaddleBuddy.Core.Converters
{
    public class InverseBoolValueConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }
    }

    public class ItemsSourceNotEmptyValueConverter : MvxValueConverter<ObservableCollection<SearchItem>, bool>
    {
        protected override bool Convert(ObservableCollection<SearchItem> value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value != null && value.Count > 0);
        }
    }

    public class ObjectNotNullValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }
    }
}
