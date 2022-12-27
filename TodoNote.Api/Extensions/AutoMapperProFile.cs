using AutoMapper;
using TodoNote.Api.Context;
using ToDoNote.Shared.Dtos;

namespace TodoNote.Api.Extensions
{
    public class AutoMapperProFile:MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();

            CreateMap<Memo, MemoDto>().ReverseMap();
        }
    }
}
