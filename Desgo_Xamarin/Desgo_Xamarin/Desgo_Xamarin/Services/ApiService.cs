using System;
using System.Collections.Generic;
using System.Text;

namespace Desgo_Xamarin.Services
{

    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Services;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Offline, sin conexión a internet",
                };
            }

            //var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
            //    "google.com");
            //if (!isReachable)
            //{
            //    return new Response
            //    {
            //        IsSuccess = false,
            //        Message = "Verifique su conexión",
            //    };
            //}

            return new Response
            {
                IsSuccess = true,
                Message = "Online, conexión a internet",
            };
        }
    /*    public async Task<Response> GetList<T>(string urlBase, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }*/
    }


}
