using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Models;
using PaddleBuddy.Services;
using PaddleBuddy.Core.Models.Messages;

namespace PaddleBuddy.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private string _status;


        public LoginViewModel()
        {
            
        }

        private async void Login()
        {
            IsLoading = true;
            var user = new User()
            {
                email = Email,
                password = Password,

            };
            var resp = await ApiService.GetInstance().Login(user);
            if (resp.Success)
            {
                ShowViewModel<HomeViewModel>();
            }
            else
            {
                Messenger.Publish(new ToastMessage(this, "Email or password not correct!", true));
            }
            IsLoading = false;

        }
        public bool IsLoading { get; set; }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }


        public ICommand RegisterCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<RegisterViewModel>());
            }
        }

        public ICommand LoginCommand
        {
            get { return new MvxCommand(Login); }
        }
    }
}