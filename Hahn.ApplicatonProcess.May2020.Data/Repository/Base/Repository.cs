using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data.Context;
using Hahn.ApplicatonProcess.May2020.Domain.Entities.Base;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories;

namespace Hahn.ApplicatonProcess.May2020.Data.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
