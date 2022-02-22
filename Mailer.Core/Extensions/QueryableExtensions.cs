using Ardalis.Specification;
using System.Linq.Expressions;

namespace System.Linq
{
    /// <summary>
    /// Extension methods for <see cref="IQueryable{T}"/>
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Extends given query source with expression if condition is true
        /// </summary>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Extends given query with Take syntax if condition is true
        /// </summary>
        public static IQueryable<TSource> TakeIf<TSource>(this IQueryable<TSource> source, bool condition, int count)
        {
            return condition ? source.Take(count) : source;
        }

        /// <summary>
        /// Easier and faster way to page given source
        /// </summary>
        public static IQueryable<TSource> PageBy<TSource>(this IQueryable<TSource> source, int skipTake, int takeCount)
        {
            return source.Skip(skipTake).Take(takeCount);
        }

        public static ISpecificationBuilder<T> WhereIf<T>(this ISpecificationBuilder<T> specificationBuilder, bool condition,Expression<Func<T, bool>> criteria)
        {
            return condition ? specificationBuilder.Where(criteria, condition: true) : specificationBuilder;
        }


    }
}
