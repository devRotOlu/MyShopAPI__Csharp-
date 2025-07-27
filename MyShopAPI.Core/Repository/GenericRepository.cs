using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Core.Models;
using MyShopAPI.Data.ApplicationDBContext;
using System.Linq.Expressions;
using X.PagedList;

namespace MyShopAPI.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _databaseContext;

        private DbSet<T> _db;

        public GenericRepository(IApplicationDbContext databaseContext)
        {
            _databaseContext = (DbContext)databaseContext;
            _db = _databaseContext.Set<T>();
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, Object>>? include = null)
        {
            IQueryable<T> query = _db;

            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }


        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }


        public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, Object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include!(query);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.AsNoTracking();
        }

        public IEnumerable<T> GetAll(RequestParams requestParams, Expression<Func<T, bool>>? expression = null, List<string>? includes = null)
        {
            IQueryable<T> query = _db;

            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.AsNoTracking().ToPagedList(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _databaseContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _db.AttachRange(entities);
            foreach (T entity in entities)
            {
                _databaseContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _db.AnyAsync(predicate);
        }
    }
}
