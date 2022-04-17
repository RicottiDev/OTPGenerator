using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OTPGenerator.Infrastructure.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T element);
        Task<bool> ExistAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateAsync(int id, T element);
        Task<T> DeleteAsync(T element);
    }
}
