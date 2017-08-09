using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using System.Text.RegularExpressions;


namespace Common
{
    public static class Helper
    {
        //public Helper()
        //{
        
        //}

        public static class ExpressionBuilder
        {
            private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
            private static MethodInfo startsWithMethod =
            typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            private static MethodInfo endsWithMethod =
            typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
            private static MethodInfo compareMethod = typeof(string).GetMethod("Compare", new Type[] { typeof(string), typeof(string), typeof(StringComparison) });
            private static MethodInfo toLowerhMethod = typeof(string).GetMethod("ToLower", new Type[] { });

            public static Expression<Func<T,bool>> GetExpression<T>(IList<Filter> filters)
            {
                if (filters.Count == 0)
                    return null;

                ParameterExpression param = Expression.Parameter(typeof(T), "t");
                Expression exp = null;

                if (filters.Count == 1)
                    exp = GetExpression<T>(param, filters[0]);
                else if (filters.Count == 2)
                    exp = GetExpression<T>(param, filters[0], filters[1]);
                else
                {
                    while (filters.Count > 0)
                    {
                        var f1 = filters[0];
                        var f2 = filters[1];

                        if (exp == null)
                            exp = GetExpression<T>(param, filters[0], filters[1]);
                        else
                            exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                        filters.Remove(f1);
                        filters.Remove(f2);

                        if (filters.Count == 1)
                        {
                            exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                            filters.RemoveAt(0);
                        }
                    }
                }

                return Expression.Lambda<Func<T, bool>>(exp, param);
            }

            private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
            {
                MemberExpression member = Expression.Property(param, filter.PropertyName);

                UnaryExpression constant = null;
     //if (member.Type == typeof(Decimal?))
     //{
     //   constant = Expression.Convert(Expression.Constant(Decimal.Parase(filter.Value.ToString()))), member.Type);
     //}
     //else if (member.Type == typeof(DateTime?))
     //{
     //   constant = Expression.Convert(Expression.Constant(DateTime.Parase(filter.Value.ToString()))), member.Type);
     //}
     //else
     //{
     //   constant = Expression.Convert(Expression.Constant(filter.Value), member.Type);
     //}

      //          ConstantExpression constant = Expression.Constant(filter.Value);
                constant = Expression.Convert(Expression.Constant(filter.Value), member.Type);
                ConstantExpression stringComparisonConstant = Expression.Constant(StringComparison.OrdinalIgnoreCase);
                switch (filter.Operation)
                {
                    case Op.Equals:
                        return Expression.Equal(member, constant);

                    case Op.GreaterThan:
                        return Expression.GreaterThan(member, constant);

                    case Op.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(member, constant);

                    case Op.LessThan:
                        return Expression.LessThan(member, constant);

                    case Op.LessThanOrEqual:
                        return Expression.LessThanOrEqual(member, constant);

                    case Op.Contains:
                        //return Expression.Call(member, containsMethod, constant);
                        var exp = Expression.Call(member, toLowerhMethod); // call ToLower() on the property
                        return Expression.Call(exp, containsMethod, constant);
                        //return Expression.Call(member, containsMethod, constant, stringComparisonConstant);

                    case Op.StartsWith:
                        return Expression.Call(member, startsWithMethod, constant);

                    case Op.EndsWith:
                        return Expression.Call(member, endsWithMethod, constant);

                    case Op.StringEquals:
                        var zeroConstant = Expression.Constant(0);
                        var compareExpression = Expression.Call(compareMethod, member, constant, stringComparisonConstant);
                        return Expression.Equal(compareExpression, zeroConstant);
                }

                return null;
            }

            private static BinaryExpression GetExpression<T>(ParameterExpression param, Filter filter1, Filter filter2)
            {
                Expression bin1 = GetExpression<T>(param, filter1);
                Expression bin2 = GetExpression<T>(param, filter2);

                return Expression.AndAlso(bin1, bin2);
            }
        }


        public static string fixPutusanNumber(string data)
        {
            data = data.Trim();
            if (data.Substring(data.Length - 1, 1) == ".")
            {
                data = data.Substring(0, data.Length - 1).Trim();
            }
            //putNum = data; 

            return data;
        }


        public static string fixNameCompact(string name)
        {
            string rest = name.Trim().ToLower();
            if (rest != null)
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                rest = rgx.Replace(rest, "");
                rest = rest.Replace(" ", "");
                rest = rest.Replace("-", "");
            }
            return rest;
        }

        public static string RemoveNumberAndFunnyCharacters(string name)
        {
            string rest = name.Trim().ToLower();
            if (rest != null)
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                rest = rgx.Replace(rest, "");
                //rest = rest.Replace(" ", "");
                //rest = rest.Replace("-", "");
            }
            return rest;
        }
    }

    public class Filter
    {
        public string PropertyName { get; set; }
        public Op Operation { get; set; }
        public object Value { get; set; }
    }

    public enum Op
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith,
        StringEquals
    }
}
