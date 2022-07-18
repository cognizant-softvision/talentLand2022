using System;
using System.Linq;
using System.Linq.Expressions;

namespace TalentLand.Trivia.Infra.Persistence.Extensions
{
    public static class EntityFrameworkCoreExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool descending = false, bool anotherLevel = false)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
            Expression body = param;
            foreach(var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            LambdaExpression sort = Expression.Lambda(body, param);
            MethodCallExpression call = Expression.Call(typeof(Queryable), 
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : String.Empty), 
                new[] { typeof(T), body.Type },
                source.Expression,
                Expression.Quote(sort));
            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
