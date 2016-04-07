using System;
using System.Threading.Tasks;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Models;
using Flurl.Http;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using PaddleBuddy.Services;

namespace PaddleBuddy.Core.Services
{
    public class UserService : ApiService
    {
        private static UserService _userService;

        public static UserService GetInstance()
        {
            return _userService ?? (_userService = new UserService());
        }

        public async Task<Response> Login(User user)
        {
            return await PostAsync("login", user);
        }

        public async Task<Response> Register(User user)
        {
            return await PostAsync("register", user);
        }
    }
}
