using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class BaseDependencyServiceAndroid
    {
        public IMvxMessenger Messenger { get; set; }

        public BaseDependencyServiceAndroid()
        {
            Messenger = Mvx.Resolve<IMvxMessenger>();
        }
    }
}