using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Mailer.Infrastructure.Extensions
{
    /// <summary>
    /// Source https://haacked.com/archive/2019/07/29/query-filter-by-interface/
    /// </summary>
    public static class ExpressionExtensions
    {
        // This magic is courtesy of this StackOverflow post.
        // https://stackoverflow.com/questions/38316519/replace-parameter-type-in-lambda-expression
        // I made some tweaks to adapt it to our needs - @haacked
        public static Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(
            this Expression<Func<TSource, bool>> root)
        {
            var visitor = new ParameterTypeVisitor<TSource, TTarget>();
            return (Expression<Func<TTarget, bool>>)visitor.Visit(root);
        }

        private class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
        {
            private ReadOnlyCollection<ParameterExpression> _parameters;

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _parameters?.FirstOrDefault(p => p.Name == node.Name)
                       ?? (node.Type == typeof(TSource) ? Expression.Parameter(typeof(TTarget), node.Name) : node);
            }

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                _parameters = VisitAndConvert(node.Parameters, "VisitLambda");
                return Expression.Lambda(Visit(node.Body), _parameters);
            }
        }
    }
}
