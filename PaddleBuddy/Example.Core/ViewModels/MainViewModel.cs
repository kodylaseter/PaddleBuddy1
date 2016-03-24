using MvvmCross.Core.ViewModels;

namespace PaddleBuddy.Core.ViewModels
{
    public class MainViewModel
        : MvxViewModel
    {
        public MainViewModel()
        {
        }

        public void ShowMenuAndFirstDetail()
        {
            ShowViewModel<MenuViewModel>();
        }
    }
}