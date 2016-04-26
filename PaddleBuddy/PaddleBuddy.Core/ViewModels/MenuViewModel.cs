using System;
using MvvmCross.Core.ViewModels;

namespace PaddleBuddy.Core.ViewModels
{
    public class MenuViewModel
        : BaseViewModel
    {

        public void ShowViewModelAndroid(Type viewModel)
        {
            ShowViewModel(viewModel);
        }
    }
}