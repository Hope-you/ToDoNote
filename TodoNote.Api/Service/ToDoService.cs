using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyToDo.Api;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using TodoNote.Api.Context;
using ToDoNote.Shared.Dtos;
using ToDoNote.Shared.Parameters;

namespace TodoNote.Api.Service
{
    /// <summary>
    /// 待办事项ToDo实现
    /// </summary>
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ToDoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(ToDoDto entity)
        {
            try
            {
                //通过mapper把页面的tododto转换陈todo
                var todo = mapper.Map<ToDo>(entity);
                //转换之后再写入
                await unitOfWork.GetRepository<ToDo>().InsertAsync(todo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    //savechange之后，会自动更新这个对象的id，可以理解成自动和数据库同步
                    return new ApiResponse(true, todo);
                return new ApiResponse(false, "添加数据失败");
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var resp = unitOfWork.GetRepository<ToDo>();
                var todos = await resp.GetPagedListAsync(predicate: s => string.IsNullOrEmpty(parameter
                    .Search) ? true : s.Title.Contains(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate)
                    );
                return new ApiResponse(true, todos);
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }



        public async Task<ApiResponse> GetAllAsync(ToDoQueryParameter toDoQueryParameter)
        {
            try
            {
                var resp = unitOfWork.GetRepository<ToDo>();
                var todos = await resp.GetPagedListAsync(predicate: s => (string.IsNullOrEmpty(toDoQueryParameter
                    .Search) || s.Title.Contains(toDoQueryParameter.Search)) && (toDoQueryParameter.Status == null || s.Status.Equals(toDoQueryParameter.Status)),
                pageIndex: toDoQueryParameter.PageIndex,
                    pageSize: toDoQueryParameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate)
                    );
                return new ApiResponse(true, todos);
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var resp = unitOfWork.GetRepository<ToDo>();
                var todo = await resp.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
                if (todo == null)
                    return new ApiResponse($"为查到数据，删除{id}异常");
                resp.Delete(todo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, todo);
                return new ApiResponse(false, "删除失败");
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }


        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var resp = unitOfWork.GetRepository<ToDo>();
                var todo = await resp.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
                if (todo == null) return new ApiResponse("无数据");
                return new ApiResponse(true, todo);
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto entity)
        {
            try
            {
                var resp = unitOfWork.GetRepository<ToDo>();
                var todo = await resp.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(entity.Id));
                if (todo == null) return new ApiResponse("未找到，更新失败！");
                todo.Status = entity.Status;
                todo.UpdateDate = DateTime.Now;
                todo.Content = entity.Content;
                todo.Title = entity.Title;
                resp.Update(todo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, todo);
                return new ApiResponse("更新失败");
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }

    }
}
