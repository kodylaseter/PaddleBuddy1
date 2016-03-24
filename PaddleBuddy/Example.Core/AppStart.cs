using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
			ShowViewModel<HomeViewModel>();
        }
    }
}