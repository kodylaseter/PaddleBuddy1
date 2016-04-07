using MvvmCross.Core.ViewModels;

namespace PaddleBuddy.Core.ViewModels
{
    public class MainViewModel
        : BaseViewModel
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