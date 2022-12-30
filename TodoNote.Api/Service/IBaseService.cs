﻿using ToDoNote.Shared.Parameters;

namespace TodoNote.Api.Service
{
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsync(QueryParameter parameter);
        Task<ApiResponse> GetSingleAsync(int id);

        Task<ApiResponse> AddAsync(T entity);

        Task<ApiResponse> UpdateAsync(T entity);
        Task<ApiResponse> DeleteAsync(int id);

    }
}
