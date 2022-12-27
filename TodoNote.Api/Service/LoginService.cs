using AutoMapper;
using MyToDo.Api;
using System.Security.Principal;
using TodoNote.Api.Context;
using ToDoNote.Shared.Dtos;

namespace TodoNote.Api.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> LoginAsync(string Account, string password)
        {
            try
            {
                var user = await work.GetRepository<User>().GetFirstOrDefaultAsync(
                    predicate: x =>
                    x.Account.Equals(Account) &&
                    x.PassWord.Equals(password));
                if (user == null) return new ApiResponse("账号或密码不正确");
                return new ApiResponse(true, user);
            }
            catch (Exception ee)
            {
                return new ApiResponse(ee.Message);
            }
        }

        public async Task<ApiResponse> Register(UserDto user)
        {
            try
            {
                var model = mapper.Map<User>(user);
                var reps = work.GetRepository<User>();
                var tempuser = await reps.GetFirstOrDefaultAsync(predicate: x => x.Account.Equals(user.Account));
                if (tempuser != null)
                    return new ApiResponse("当前用户已存在");
                await reps.InsertAsync(model);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, model);
                return new ApiResponse("数据写入失败");
            }
            catch (Exception ee)
            {
                return new ApiResponse("注册失败" + ee.Message);
            }
        }
    }
}
