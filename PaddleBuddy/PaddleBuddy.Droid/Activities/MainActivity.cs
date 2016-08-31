﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;
using PaddleBuddy.Core.ViewModels;
using PaddleBuddy.Core.Services;
using Plugin.Permissions;
using ActionBar = Android.Support.V7.App.ActionBar;


namespace PaddleBuddy.Droid.Activities
{
    [Activity(
        Label = "PaddleBuddy",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop,
        Name = "paddlebuddy.droid.activities.MainActivity"
        )]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        public DrawerLayout DrawerLayout;
        public ActionBar _actionBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
                ViewModel.ShowMenuAndFirstDetail();
            _actionBar = SupportActionBar;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions)
        {
            base.OnRequestPermissionsResult(requestCode, permissions);
            var a = 5;
        }

        /*public override IFragmentCacheConfiguration BuildFragmentCacheConfiguration()
        {
            return new FragmentCacheConfigurationCustomFragmentInfo(); // custom FragmentCacheConfiguration is used because custom IMvxFragmentInfo is used -> CustomFragmentInfo
        }*/

        /*public override void OnFragmentCreated(IMvxCachedFragmentInfo fragmentInfo, FragmentTransaction transaction)
        {
            base.OnFragmentCreated(fragmentInfo, transaction);

            var myCustomInfo = fragmentInfo as CustomFragmentInfo;

            // You can do fragment + transaction based configurations here.
            // Note that, the cached fragment might be reused in another transaction afterwards.
        }*/

        /*private void CheckIfMenuIsNeeded(CustomFragmentInfo myCustomInfo)
        {
            //If not root, we will block the menu sliding gesture and show the back button on top
            if (myCustomInfo.IsRoot)
                ShowHamburguerMenu();
            else
                ShowBackButton();
        }*/

        /*private void ShowBackButton()
        {
            //TODO Tell the toggle to set the indicator off
            //this.DrawerToggle.DrawerIndicatorEnabled = false;

            //Block the menu slide gesture
            DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
        }*/

        /*private void ShowHamburguerMenu()
        {
            //TODO set toggle indicator as enabled 
            //this.DrawerToggle.DrawerIndicatorEnabled = true;

            //Unlock the menu sliding gesture
            DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
        }
		*/

        /*public override void OnBeforeFragmentChanging (IMvxCachedFragmentInfo fragmentInfo, FragmentTransaction transaction)
		{
			var currentFrag = SupportFragmentManager.FindFragmentById (Resource.Id.content_frame) as MvxFragment;

			if(currentFrag != null 
				&& fragmentInfo.ViewModelType != typeof(MenuViewModel) 
				&& currentFrag.FindAssociatedViewModelType() != fragmentInfo.ViewModelType)
				fragmentInfo.AddToBackStack = true;
			base.OnBeforeFragmentChanging (fragmentInfo, transaction);
		}*/

        /*public override void OnFragmentChanged(IMvxCachedFragmentInfo fragmentInfo)
        {
            var myCustomInfo = fragmentInfo as CustomFragmentInfo;
            CheckIfMenuIsNeeded(myCustomInfo);
        }*/


        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
                DrawerLayout.CloseDrawers();
            else
                base.OnBackPressed();
        }

		public void HideSoftKeyboard()
		{
			if (CurrentFocus == null) return;

			InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
			inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

			CurrentFocus.ClearFocus();
		}
    }

    /*public class CustomFragmentInfo : MvxCachedFragmentInfo
    {
        public CustomFragmentInfo(string tag, Type fragmentType, Type viewModelType, bool cacheFragment = true, bool addToBackstack = false,
            bool isRoot = false)
            : base(tag, fragmentType, viewModelType, cacheFragment, addToBackstack)
        {
            IsRoot = isRoot;
        }

        public bool IsRoot { get; set; }
    }*/
}