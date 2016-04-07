using System;
using System.Threading.Tasks;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Models;
using PaddleBuddy.Services;

namespace PaddleBuddy.Core.Services
{
    public class MapService : ApiService
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

        //public async Task<River> GetRiver(int id)
        //{
        //    return await PostAsync("")
        //}
    }
}
