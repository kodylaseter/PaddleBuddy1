using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Models;
using PaddleBuddy.Services;

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
            var user = new User()
            {
                email = Email,
                password = Password,

            };
            var resp = await ApiService.GetInstance().Login(user);
            if (resp.Success)
            {
                ShowViewModel<MapViewModel>();
            }
            else
            {
                //TODO uncomment this when messenger is implemented
                //Messenger.Publish(new ToastMessage(this, "Email or password not correct!", true));
            }

        }
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