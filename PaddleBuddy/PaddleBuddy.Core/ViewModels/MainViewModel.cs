using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.Services;

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