using Microsoft.EntityFrameworkCore.Query;
using MyShopAPI.Core.Models;
using System.Linq.Expressions;

namespace MyShopAPI.Core.IRepository
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, Object>>? include = null);
        IEnumerable<T> GetAll(RequestParams requestParams, Expression<Func<T, bool>>? expression = null, List<string>? includes = null);
        Task<T> Get(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IIncludableQueryable<T, Object>>? include = null);
        Task Insert(T entity);
        Task<T> TrackedGet(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IIncludableQueryable<T, Object>>? include = null);
        Task InsertRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}
