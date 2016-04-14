﻿using System;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using PaddleBuddy.Droid.Activities;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Fragments
{
    public abstract class BaseFragment : MvxFragment, Android.Widget.TextView.IOnEditorActionListener
    {
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;
        private bool _searchOpen;
        private InputMethodManager _imm;
        private Android.Support.V7.App.ActionBar _actionBar;
        private View _edtSearch;
        

        protected BaseFragment()
        {
            RetainInstance = true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _searchOpen = false;
            HasOptionsMenu = true;
            _imm = (InputMethodManager)Context.ApplicationContext.GetSystemService(Android.Content.Context.InputMethodService);
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
            _actionBar = ((MainActivity)Activity).SupportActionBar;
            _actionBar.SetCustomView(Resource.Layout.toolbar_search);
            _edtSearch = _actionBar.CustomView.FindViewById(Resource.Id.edtSearch);
            ((AppCompatEditText)_edtSearch).SetOnEditorActionListener(this);

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
                HandleSearch(item);
            }
            return base.OnOptionsItemSelected(item);
        }


        protected abstract int FragmentId { get; }

        public void HandleSearch(IMenuItem item)
        {
            if (_searchOpen)
            {
                _actionBar.SetDisplayShowTitleEnabled(true);
                _actionBar.SetDisplayShowCustomEnabled(false);
                item.SetIcon(Resource.Drawable.ic_search_white);
                _imm.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, 0);
            }
            else
            {
                _actionBar.SetDisplayShowTitleEnabled(false);
                _actionBar.SetDisplayShowCustomEnabled(true);
                _edtSearch.RequestFocus();
                _imm.ShowSoftInput(_edtSearch, ShowFlags.Implicit);
                item.SetIcon(Resource.Drawable.ic_clear_white);
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

        public bool OnEditorAction(Android.Widget.TextView v, [GeneratedEnum] ImeAction actionId, KeyEvent e)
        {
            if (actionId == ImeAction.Search)
            {
                //TODO implement search
                return true;
            }
            return false;
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

