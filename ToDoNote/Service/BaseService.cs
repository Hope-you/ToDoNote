using MyToDo.Shared.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoNote.Shared;
using ToDoNote.Shared.Parameters;

namespace ToDoNote.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;

        public BaseService(HttpRestClient client, string serviceName)
        {
            this.client = client;
            this.serviceName = serviceName;
        }

        public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Add";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Delete;
            request.Route = $"api/{serviceName}/Delete?id={id}";
            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{serviceName}/GetAll?PageIndex={parameter.PageIndex}" +
                $"&PageSize={parameter.PageSize}" +
                (string.IsNullOrEmpty(parameter.Search) ? "" : $"&Search={parameter.Search}");
            return await client.ExecuteAsync<PagedList<TEntity>>(request);
        }

        public async Task<ApiResponse<TEntity>> GetFirsrOrDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{serviceName}/Get?id={id}";
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/UpDate";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }
    }
}
