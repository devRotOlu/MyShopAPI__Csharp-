using System.Linq.Expressions;

namespace MyShopAPI.Helpers
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool descending = false)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(param, propertyName);
            var lambda = Expression.Lambda(property, param);

            string methodName = descending ? "OrderByDescending" : "OrderBy";

            var result = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(lambda)
            );

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(result);
        }
    }

}
