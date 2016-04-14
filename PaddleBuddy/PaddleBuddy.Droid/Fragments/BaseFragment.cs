using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Droid.Activities;

namespace PaddleBuddy.Droid.Fragments
{
    public abstract class BaseFragment : MvxFragment
    {
        private Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;
        private bool _searchOpen;

        protected BaseFragment()
        {
            RetainInstance = true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _searchOpen = false;
            HasOptionsMenu = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			var ignore = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(FragmentId, null);

			_toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
			if (_toolbar != null)
			{
				((MainActivity)Activity).SetSupportActionBar(_toolbar);
				((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);

				_drawerToggle = new MvxActionBarDrawerToggle(
					Activity,                               // host Activity
					((MainActivity)Activity).DrawerLayout,  // DrawerLayout object
					_toolbar,                               // nav drawer icon to replace 'Up' caret
					Resource.String.drawer_open,            // "open drawer" description
					Resource.String.drawer_close            // "close drawer" description
				);
				_drawerToggle.DrawerOpened += (sender, e) => ((MainActivity)Activity).HideSoftKeyboard ();
				((MainActivity)Activity).DrawerLayout.SetDrawerListener(_drawerToggle);
			}

			return view;
		}

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.main_menu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_search)
            {
                HandleSearch();
            }
            return base.OnOptionsItemSelected(item);
        }


        protected abstract int FragmentId { get; }

        public void HandleSearch()
        {
            var actionBar = ((MainActivity)Activity).SupportActionBar;
            if (_searchOpen)
            {
                actionBar.SetDisplayShowTitleEnabled(true);
                actionBar.SetDisplayShowCustomEnabled(false);
            }
            else
            {
                actionBar.SetDisplayShowTitleEnabled(false);
                actionBar.SetCustomView(Resource.Layout.toolbar_search);
                actionBar.SetDisplayShowCustomEnabled(true);
            }
            _searchOpen = !_searchOpen;

        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (_toolbar != null)
                _drawerToggle.OnConfigurationChanged(newConfig);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            if (_toolbar != null)
                _drawerToggle.SyncState();
        }
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}

