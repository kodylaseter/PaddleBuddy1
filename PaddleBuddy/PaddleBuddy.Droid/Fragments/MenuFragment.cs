﻿using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using PaddleBuddy.Core.Services;
using PaddleBuddy.Core.ViewModels;
using PaddleBuddy.Droid.Activities;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("paddlebuddy.droid.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_navigation, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            _navigationView.Menu.FindItem(Resource.Id.nav_map).SetChecked(true);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            item.SetCheckable(true);
            item.SetChecked(true);
            _previousMenuItem?.SetChecked(false);
            _previousMenuItem = item;

            Navigate(item.ItemId);

            return true;
        }

        private void Navigate(int itemId)
        {
            ((MainActivity)Activity).DrawerLayout.CloseDrawers ();
            //TODO: check if navigate delay is necessary
            //await Task.Delay (TimeSpan.FromMilliseconds (250));

            switch (itemId) {
                case Resource.Id.nav_map:
                    ViewModel.ShowViewModelAndroid(typeof(MapViewModel));
                    break;
                case Resource.Id.nav_preferences:
                    ViewModel.ShowViewModelAndroid(typeof(PreferencesViewModel));
                    break;
                case Resource.Id.nav_helpfeedback:
                    ViewModel.ShowViewModelAndroid(typeof(HelpFeedbackViewModel));
                    break;
                case Resource.Id.nav_plan:
                    ViewModel.ShowViewModelAndroid(typeof(PlanViewModel));
                    break;
                case Resource.Id.nav_recent:
                    ViewModel.ShowViewModelAndroid(typeof(RecentViewModel));
                    break;
                case Resource.Id.nav_points:
                    ViewModel.ShowViewModelAndroid(typeof(PointViewModel));
                    break;
                default:
                    MessengerService.Toast(this, "Invalid navigation!", true);
                    break;
            }
        }
    }
}