using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Resource.API.Serves
{
    public interface IBaseServe<T> where T : class
    {
         Task<bool> AddAsync(T t);
        Task<bool> AddRangeAsync(List<T> t);
        IEnumerable<T> GetQueryable(Func<T, bool> whereLambda);
        Task<T> GetModelAsync(Expression<Func<T, bool>> whereLambda);
        Task<List<T>> GetModelsAsync(Func<T, bool> whereLambda);

    }
}
