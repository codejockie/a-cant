using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Domain.Entities.Base;

namespace Hahn.ApplicatonProcess.May2020.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity);
        Task<T> GetByIdAsync(long id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
