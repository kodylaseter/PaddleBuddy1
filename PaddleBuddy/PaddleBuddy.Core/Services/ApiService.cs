using System;
using System.Threading.Tasks;
using Flurl.Http;
using PaddleBuddy.Core.Models;

namespace PaddleBuddy.Core.Services
{
    public abstract class ApiService : BaseService
    {
        private const string ContentTypeJson = "application/json";
        
        public async Task<Response> PostAsync(string url, object data)
        {
            var response = new Response();

            if (NetworkService.IsServerAvailable)
            {
                var fullUrl = SysPrefs.ApiBase + url;
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
            var response = new Response();
            if (NetworkService.IsServerAvailable)
            {
                var fullUrl = SysPrefs.ApiBase + url;
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
            var response = new Response();
            if (NetworkService.IsServerAvailable)
            {
                var fullUrl = SysPrefs.ApiBase + url;
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
