﻿using Android.Runtime;
using MvvmCross.Droid.Support.V7.Fragging.Attributes;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("paddlebuddy.droid.fragments.HomeFragment")]
    public class HomeFragment : BaseFragment<HomeViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_home;
    }
}