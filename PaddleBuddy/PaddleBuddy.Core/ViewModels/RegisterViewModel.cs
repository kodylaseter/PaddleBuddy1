﻿using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using PaddleBuddy.Core.Models;

namespace PaddleBuddy.Core.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public async void Register()
        {
            User user = new User()
            {
                Admin = false,
                Email = Email,
                Password = Password,
                Username = Username,
                Salt = "notasalt"
            };
            //TODO implement register
            //var response = await ApiService.GetInstance().Register(user);
            //if (response.Success)
            //{
            //    Messenger.Publish(new ToastMessage(this, "Registered!", true));
            //    ShowViewModel<HomeViewModel>();
            //}
            //else
            //{
            //    Messenger.Publish(new ToastMessage(this, response.Detail, true)); 
            //}
            ShowViewModel<HomeViewModel>();

        }

        public ICommand SubmitCommand
        {
            get
            {
                return new MvxCommand(Register);
            }
        }

        public bool IsLoading { get; set; }
    }
}
