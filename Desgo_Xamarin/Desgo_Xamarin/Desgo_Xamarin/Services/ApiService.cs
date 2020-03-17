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
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Offline, sin conexión a internet",
                    };
                }

                var isReachable = await CrossConnectivity.Current.IsReachable("google.com");
                if (!isReachable)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Verifique su conexión",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Online, conexión a internet",
                };
            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Verifique su conexión TryCatch",
                };

            }
        }
    }


}
