using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Models;
using PaddleBuddy.Services;
using PaddleBuddy.Core.Models.Messages;

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
    }
}