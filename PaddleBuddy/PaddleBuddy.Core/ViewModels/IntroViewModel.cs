using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace PaddleBuddy.Core.ViewModels
{
    public class IntroViewModel : BaseViewModel
    {
        public ICommand RegisterCommand
        {
            get { return new MvxCommand(() => ShowViewModel<RegisterViewModel>()); }
        }

        public ICommand LoginCommand
        {
            get { return new MvxCommand(() => ShowViewModel<LoginViewModel>()); }
        }

        public IntroViewModel()
        {
        }
    }
}