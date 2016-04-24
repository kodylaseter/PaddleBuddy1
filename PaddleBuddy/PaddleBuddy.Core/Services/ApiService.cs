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
    public abstract class ApiService : BaseService
    {
        //private const string ApiBase = "http://paddlebuddy-pbdb.rhcloud.com/";
        private const string ApiBase = "http://10.0.3.3:4000/api/mobile/";
        private const string ContentTypeJson = "application/json";

        //TODO implement internet checking mechanism
        public async Task<Response> PostAsync(string uri, object data)
        {
            Response response;
            var fullUri = ApiBase + uri;
            try
            {
                response = await fullUri.WithHeader("ContentType", ContentTypeJson)
                .PostJsonAsync(data).ReceiveJson<Response>();
            }
            catch (Exception e)
            {
                Messenger.Publish(new ToastMessage(this, "Problem reaching remote server!", false));
                response = new Response
                {
                    Success = false
                };
            }
            return response;
        }

        public async Task<Response> GetAsync(string uri)
        {
            var fullUri = ApiBase + uri;
            Response response;
            try
            {
                response = await fullUri.GetJsonAsync<Response>();
            }
            catch (Exception e)
            {
                Messenger.Publish(new ToastMessage(this, "Problem reaching remote server!", false));
                response = new Response
                {
                    Success = false
                };
            }

            return response;
        }
    }
}
