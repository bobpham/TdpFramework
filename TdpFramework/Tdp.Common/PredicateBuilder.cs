using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Tdp.Common
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                         Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
              (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                           Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
              (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> BuildGreaterThan<T>(string propertyName, object constantValue)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var nameProperty = Expression.Property(parameter, propertyName); //x.Id

            Type propertyType = PropertyUtils.GetPropertyTypeFromClass<T>(propertyName);

            Type valueType = constantValue.GetType();

            if (propertyType != valueType)
                return null;

            var constant = Expression.Constant(constantValue, propertyType);
            var body = Expression.GreaterThan(nameProperty, constant); //x.Id >= 3
            return Expression.Lambda<Func<T, bool>>(body, parameter); //x => x.Id >= 3

        }

        public static Expression<Func<T, bool>> BuildEqual<T>(string propertyName, object constantValue)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var nameProperty = Expression.Property(parameter, propertyName); //x.Id

            Type propertyType = PropertyUtils.GetPropertyTypeFromClass<T>(propertyName);

            Type valueType = constantValue.GetType();

            if (propertyType != valueType)
                return null;

            var constant = Expression.Constant(constantValue, propertyType);
            var body = Expression.Equal(nameProperty, constant); //x.Id >= 3
            return Expression.Lambda<Func<T, bool>>(body, parameter); //x => x.Id >= 3

        }

        public static Expression<Func<T, bool>> BuildContains<T>(string propertyName, string constantValue)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var nameProperty = Expression.Property(parameter, propertyName); //x.Id

            // c. get MethodInfo of the StartWith method found in the String object
            var startsWithMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

            var lowerMemberCall = Expression.Call(nameProperty, toLowerMethod);

            // d. get expression that represents the name value to search 
            var searchValue = Expression.Constant(constantValue.ToLower(), typeof(string));

            // e. get the start with expression call
            var body = Expression.Call(lowerMemberCall, startsWithMethod, new Expression[1] { searchValue });

            return Expression.Lambda<Func<T, bool>>(body, parameter); //x => x.Id >= 3
        }


    }
}
