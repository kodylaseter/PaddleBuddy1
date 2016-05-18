using System;
using System.Threading.Tasks;
using Flurl.Http;
using PaddleBuddy.Core.Models;
using PaddleBuddy.Core.Models.Messages;

namespace PaddleBuddy.Core.Services
{
    public abstract class ApiService : BaseService
    {
        //private const string ApiBase = "http://paddlebuddy-pbdb.rhcloud.com/";
        private const string ApiBase = "http://10.0.3.3:4000/api/mobile/";
        private const string ContentTypeJson = "application/json";

        //TODO implement internet checking mechanism
        public async Task<Response> PostAsync(string url, object data)
        {
            Response response = new Response();

            if (NetworkService.IsServerAvailable)
            {
                var fullUrl = ApiBase + url;
                try
                {
                    response = await fullUrl.WithHeader("ContentType", ContentTypeJson)
                        .PostJsonAsync(data).ReceiveJson<Response>();
                }
                catch (Exception e)
                {
                    MessengerService.Toast(this, "Problem reaching remote server!", false);
                    response = new Response
                    {
                        Success = false
                    };
                }
            }
            else
            {
                response.Success = false;
                response.Detail = "No network connection";
                MessengerService.Toast(this, "No network connection available!", true);
            }
            return response;

        }

        public async Task<Response> GetAsync(string url)
        {
            Response response = new Response();
            if (NetworkService.IsServerAvailable)
            {
                var fullUrl = ApiBase + url;
                try
                {
                    response = await fullUrl.GetJsonAsync<Response>();
                }
                catch (Exception e)
                {
                    MessengerService.Toast(this, "Problem reaching remote server!", false);
                    response = new Response
                    {
                        Success = false
                    };
                }
            }
            else
            {
                response.Success = false;
                response.Detail = "No network connection";
                MessengerService.Toast(this, "No network connection available!", true);
            }
            return response;
        }

        public async Task<Response> GetAsync(string url, object multiple)
        {
            Response response = new Response();
            if (NetworkService.IsServerAvailable)
            {
                var fullUrl = ApiBase + url;
                try
                {
                    response = await fullUrl.WithHeaders(multiple).GetJsonAsync<Response>();
                }
                catch (Exception)
                {
                    MessengerService.Toast(this, "Problem reaching remote server!", false);
                    response = new Response
                    {
                        Success = false
                    };
                }
            }
            else
            {
                response.Success = false;
                response.Detail = "No network connection";
                MessengerService.Toast(this, "No network connection available!", true);
            }

            return response;
        }

        public async Task<Response> GetAsync(string url, string name, string value)
        {
            //TODO: implement single header api get
            throw new NotImplementedException();
        }
    }
}
