using MyToDo.Shared.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoNote.Shared;
using ToDoNote.Shared.Dtos;
using ToDoNote.Shared.Parameters;

namespace ToDoNote.Service
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient client;

        public ToDoService(HttpRestClient client) : base(client, "ToDo")
        {
            this.client = client;
        }

        public async Task<ApiResponse<PagedList<ToDoDto>>> GetFilterAll(ToDoQueryParameter toDoQueryParameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/ToDo/GetAll?PageIndex={toDoQueryParameter.PageIndex}" +
                $"&PageSize={toDoQueryParameter.PageSize}" +
                (string.IsNullOrEmpty(toDoQueryParameter.Search) ? "" : $"&Search={toDoQueryParameter.Search}") +
                $"&Status={toDoQueryParameter.Status}";
            return await client.ExecuteAsync<PagedList<ToDoDto>>(request);
        }
    }
}
