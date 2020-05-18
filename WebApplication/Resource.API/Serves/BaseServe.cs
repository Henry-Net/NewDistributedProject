using EntityModels.Common;
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

        public async Task<bool> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        #region 增


        public async Task<ResultCommon> AddModelAsync(T t)
        {
            var result = new ResultCommon()
            {
                Code = CommonCode.Failure,
                Message = "添加失败"
            };
            await _dbContext.Set<T>().AddAsync(t);
            if (await SaveChangeAsync())
            {
                result.Code = CommonCode.Successful;
                result.Message = "添加成功";
            }
            return result;
        }
        public async Task<ResultCommon> AddModelsAsync(List<T> t)
        {
            var result = new ResultCommon()
            {
                Code = CommonCode.Failure,
                Message = "添加失败"
            };
            await _dbContext.Set<T>().AddRangeAsync(t);
            if (await SaveChangeAsync())
            {
                result.Code = CommonCode.Successful;
                result.Message = "添加成功";
            }
            return result;
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

        #endregion


        #region 删

        public async Task<bool> DeleteAsync(T t)
        {
            _dbContext.Set<T>().Attach(t);
            _dbContext.Entry<T>(t).State = EntityState.Deleted;
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteRangeAsync(List<T> t)
        {
            await _dbContext.Set<T>().AddRangeAsync(t);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        #endregion

        public IQueryable<T> GetQueryable(Func<T, bool> whereLambda)
        {
            return _dbContext.Set<T>().AsNoTracking().Where(whereLambda).AsQueryable();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> whereLambda)

        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(whereLambda);
        }
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambda)

        {
            return await _dbContext.Set<T>().AsNoTracking().Where(whereLambda).ToListAsync();
        }
        public async Task<ResultModel<T>> GetModelAsync(Expression<Func<T, bool>> whereLambda)
           
        {
            ResultModel<T> resultModel = new ResultModel<T>()
            {
                Code = CommonCode.Successful
            };
            if (whereLambda!=null)
            {
                resultModel.Model = await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(whereLambda);
            }
            return resultModel;
        }
        public async Task<ResultList<T>> GetModelsAsync()
        {
            ResultList<T> resultList = new ResultList<T>()
            {
                Code = CommonCode.Successful
            };
            var list = await _dbContext.Set<T>().AsNoTracking().ToListAsync();
            resultList.List = list;
            resultList.Total = list!=null?list.Count:0;
            return resultList;
        }
    }
}
