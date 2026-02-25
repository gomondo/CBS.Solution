using CBS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbContextFactory<CBSDbContext> _contextFactory;

        public GenericRepository(IDbContextFactory<CBSDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Add(T entity)
        {

            var context = await _contextFactory.CreateDbContextAsync();
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {

            var context = await _contextFactory.CreateDbContextAsync();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(T entity)
        {
            var context = await _contextFactory.CreateDbContextAsync();
            context.Set<T>().Remove(entity);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<T> GetById(long id)
        {
            var context = await _contextFactory.CreateDbContextAsync();
            var result = await context.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var context = await _contextFactory.CreateDbContextAsync();
            return context.Set<T>().AsQueryable();
        }
    }
}