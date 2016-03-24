using System;
using System.Threading.Tasks;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Models;
using Flurl.Http;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;

namespace PaddleBuddy.Services
{
    public class ApiService
    {
        private static ApiService apiService;
        private const string ApiBase = "http://paddlebuddy-pbdb.rhcloud.com/";
        //private const string ApiBase = "http://0.0.0.0:8081/";
        private const string ContentTypeJson = "application/json";
        

        public static ApiService GetInstance()
        {
            if (apiService == null) apiService = new ApiService();
            return apiService;
        }

        public async Task<Response> Login(User user)
        {
            return await PostAsync(ApiBase + "login", user);
        }

        public async Task<Response> Register(User user)
        {
            return await PostAsync(ApiBase + "register", user);
        }

        public async Task<Response> PostAsync(string uri, object data)
        {
            Response response = new Response();
            try
            {
                response = await uri.WithHeader("ContentType", ContentTypeJson)
                .PostJsonAsync(data).ReceiveJson<Response>();
            }
            catch (Exception e)
            {
                Mvx.Resolve<IMvxMessenger>().Publish(new ToastMessage(this, "Problem reaching remote server!", false));
                throw e;
            }

            return response;
        }
    }
}
