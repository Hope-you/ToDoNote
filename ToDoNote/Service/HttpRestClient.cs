using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ToDoNote.Shared;

namespace ToDoNote.Service
{
    public class HttpRestClient
    {
        private readonly string apiUrl;
        protected readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new RestClient();
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest("", baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            client.Options.BaseUrl = new Uri(apiUrl + baseRequest.Route);
            var res = await client.ExecuteAsync(request);



            if (res.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse>(res.Content);
            else
            {
                return new ApiResponse()
                {
                    Status = false,
                    Message = res.ErrorMessage + res.StatusCode.ToString()
                };
            }
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(apiUrl + baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter != null)
                //增加post参数，
                request.AddJsonBody(baseRequest.Parameter);
            //request.AddParameter("param", , ParameterType.RequestBody);
            var res = await client.ExecuteAsync(request);
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse<T>>(res.Content);
            else
            {
                return new ApiResponse<T>()
                {
                    Status = false,
                    Message = res.ErrorMessage + res.StatusCode.ToString()
                };
            }
        }
    }
}
