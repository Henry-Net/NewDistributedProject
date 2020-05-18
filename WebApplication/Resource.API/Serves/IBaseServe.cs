using EntityModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Resource.API.Serves
{
    public interface IBaseServe<T> where T : class
    {
        Task<bool> SaveChangeAsync();
         Task<bool> AddAsync(T t);
        Task<bool> AddRangeAsync(List<T> t);
        Task<ResultCommon> AddModelAsync(T t);
        Task<ResultCommon> AddModelsAsync(List<T> t);

        IQueryable<T> GetQueryable(Func<T, bool> whereLambda);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambda);
        Task<ResultModel<T>> GetModelAsync(Expression<Func<T, bool>> whereLambda);
        Task<ResultList<T>> GetModelsAsync();

    }
}
