using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared.Helpers
{
    public static class PredicateExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expression1,
                Expression<Func<T, bool>> expression2)
            where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "e");

            var combined = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    Expression.Invoke(expression1, parameter),
                    Expression.Invoke(expression2, parameter)
                ),
                parameter
            );

            return combined;
        }
    }
}
