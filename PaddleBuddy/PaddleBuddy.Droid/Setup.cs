using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Android.Content;
using Android.Net;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Support.V7.Fragging.Presenter;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using PaddleBuddy.Core;
using PaddleBuddy.Core.DependencyServices;
using PaddleBuddy.Droid.DependencyServices;

namespace PaddleBuddy.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            var app = new App();
            Mvx.RegisterSingleton<ILocationProvider>(new LocationProviderAndroid());
            Mvx.RegisterSingleton<INetwork>(new NetworkAndroid());
            return app;
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
		{
			typeof(Android.Support.Design.Widget.NavigationView).Assembly,
			typeof(Android.Support.Design.Widget.FloatingActionButton).Assembly,
			typeof(Android.Support.V7.Widget.Toolbar).Assembly,
			typeof(Android.Support.V4.Widget.DrawerLayout).Assembly,
			typeof(Android.Support.V4.View.ViewPager).Assembly,
			typeof(MvvmCross.Droid.Support.V7.RecyclerView.MvxRecyclerView).Assembly
		};

        /// <summary>
		/// This is very important to override. The default view presenter does not know how to show fragments!
        /// </summary>
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
