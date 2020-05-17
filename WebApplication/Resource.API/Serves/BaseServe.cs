using Microsoft.EntityFrameworkCore;
using Resource.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.API.Serves
{
    public class BaseServe<T>:IBaseServe<T> where T : class
    {
        private readonly ResourceDbContext _dbContext;

        public BaseServe(ResourceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(T t)
        {
            await _dbContext.Set<T>().AddAsync(t);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddRangeAsync(List<T> t)
        {
            await _dbContext.Set<T>().AddRangeAsync(t);
            return await _dbContext.SaveChangesAsync() > 0;
        }




        public IQueryable<T> GetQueryable(Func<T,bool> whereLambda)
        {
             return _dbContext.Set<T>().AsNoTracking().Where(whereLambda).AsQueryable();
        }
        public async Task<T> GetModelAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(whereLambda);
        }
        public async Task<List<T>> GetModelsAsync(Func<T, bool> whereLambda)
        {
           return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
    }
}
