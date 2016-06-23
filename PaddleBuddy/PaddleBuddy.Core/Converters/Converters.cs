using System;
using System.Collections.ObjectModel;
using System.Globalization;
using MvvmCross.Platform.Converters;
using PaddleBuddy.Core.Models;
using System.Collections;

namespace PaddleBuddy.Core.Converters
{
    public class InverseBoolValueConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }
    }

    //public class ItemsSourceNotEmptyValueConverter : MvxValueConverter<object, bool>
    //{
    //    protected override bool Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value != null)
    //        {
    //            var obsCol = value as ObservableCollection<object>;
    //            if (obsCol != null && obsCol.Count > 0) return true;
    //        }
    //        return false;
    //    }
    //}

    public class ItemsSourceNotEmptyValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var obs = value as IList;
                if (obs != null && obs.Count > 0) return true;
            }
            return false;
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
