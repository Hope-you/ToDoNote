using AutoMapper;
using MyToDo.Api;
using System.Reflection.Metadata;
using TodoNote.Api.Context;
using ToDoNote.Shared.Dtos;
using ToDoNote.Shared.Parameters;

namespace TodoNote.Api.Service
{
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(MemoDto entity)
        {
            try
            {
                //通过mapper把页面的tododto转换陈todo
                var memo = mapper.Map<Memo>(entity);
                //转换之后再写入
                await unitOfWork.GetRepository<Memo>().InsertAsync(memo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, memo);
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
                var resp = unitOfWork.GetRepository<Memo>();
                var memos = await resp.GetPagedListAsync(predicate: s => string.IsNullOrEmpty(parameter
                    .Search) ? true : s.Title.Contains(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate)
                    );
                return new ApiResponse(true, memos);
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
                var resp = unitOfWork.GetRepository<Memo>();
                var memo = await resp.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
                if (memo == null)
                    return new ApiResponse($"为查到数据，删除{id}异常");
                resp.Delete(memo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, memo);
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
                var resp = unitOfWork.GetRepository<Memo>();
                var memo = await resp.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
                if (memo == null) return new ApiResponse("无数据");
                return new ApiResponse(true, memo);
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto entity)
        {
            try
            {
                var resp = unitOfWork.GetRepository<Memo>();
                var memo = await resp.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(entity.Id));
                if (memo == null) return new ApiResponse("未找到，更新失败！");
                memo.UpdateDate = DateTime.Now;
                memo.Content = entity.Content;
                memo.Title = entity.Title;
                resp.Update(memo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, memo);
                return new ApiResponse("更新失败");
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }
    }
}
