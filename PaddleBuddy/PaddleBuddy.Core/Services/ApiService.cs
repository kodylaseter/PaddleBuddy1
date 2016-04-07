using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Map;
using PaddleBuddy.Core.Models.Messages;
using PaddleBuddy.Core.ViewModels;

namespace PaddleBuddy.Core.Services
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

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var fullUri = ApiBase + uri;
            var resp = await fullUri.GetAsync();

            return resp;
        }
    }
}
