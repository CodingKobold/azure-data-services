using Azure.Data.Tables;
using CodingKobold.DataStores.Api.Helpers;
using System.Linq.Expressions;

namespace CodingKobold.DataStores.Api.Builders
{
    internal abstract class BaseTableFilterBuilder<TEntity> : ITableFilterBuilder<TEntity>
        where TEntity : class, ITableEntity, new()
    {
        private readonly List<string> _filterList;

        public BaseTableFilterBuilder()
        {
            _filterList = new List<string>();
        }

        public string Build()
        {
            return string.Join(" and ", _filterList);
        }

        public ITableFilterBuilder<TEntity> GreaterOrEqualThan(Expression<Func<TEntity, object>> expression, object? value)
        {
            return AddFilter(expression, value, ODataOperators.GreaterThanOrEqual);
        }

        public ITableFilterBuilder<TEntity> LesserOrEqualThan(Expression<Func<TEntity, object>> expression, object? value)
        {
            return AddFilter(expression, value, ODataOperators.LesserThanOrEqual);
        }

        public ITableFilterBuilder<TEntity> EqualTo(Expression<Func<TEntity, object>> expression, object? value)
        {
            return AddFilter(expression, value, ODataOperators.Equal);
        }

        private ITableFilterBuilder<TEntity> AddFilter(Expression<Func<TEntity, object>> expression, object? value, string operatorString)
        {
            if (value == null)
            {
                return this;
            }

            var columnName = GetColumnName(expression);

            var filter = $"{columnName} {operatorString} '{GetSortableString(value)}'";
            _filterList.Add(filter);

            return this;
        }

        private static object GetColumnName(Expression<Func<TEntity, object>> expression)
        {
            if (expression.Body is MemberExpression member)
            {
                return member.Member.Name;
            }

            throw new ArgumentException();
        }

        private static string? GetSortableString(object value)
        {
            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue.ToSortableString();
            }

            return value.ToString();
        }
    }
}
