using System;
using System.Threading.Tasks;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Models;
using Flurl.Http;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;

namespace PaddleBuddy.Services
{
    public abstract class ApiService
    {
        //private const string ApiBase = "http://paddlebuddy-pbdb.rhcloud.com/";
        private const string ApiBase = "http://127.0.0.1:4000/";
        private const string ContentTypeJson = "application/json";

        public async Task<Response> PostAsync(string uri, object data)
        {
            var response = new Response();
            var fullUri = ApiBase + "uri";
            try
            {
                response = await fullUri.WithHeader("ContentType", ContentTypeJson)
                .PostJsonAsync(data).ReceiveJson<Response>();
            }
            catch (Exception e)
            {
                Mvx.Resolve<IMvxMessenger>().Publish(new ToastMessage(this, "Problem reaching remote server!", false));
                throw;
            }

            return response;
        }

        
    }
}
