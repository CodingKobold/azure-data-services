using Azure.Data.Tables;
using System.Linq.Expressions;

namespace CodingKobold.DataStores.Api.Builders
{
    public interface ITableFilterBuilder<TEntity> where TEntity : class, ITableEntity, new()
    {
        string Build();
        ITableFilterBuilder<TEntity> GreaterOrEqualThan(Expression<Func<TEntity, object>> expression, object? value);
        ITableFilterBuilder<TEntity> LesserOrEqualThan(Expression<Func<TEntity, object>> expression, object? value);
        ITableFilterBuilder<TEntity> EqualTo(Expression<Func<TEntity, object>> expression, object? value);
    }
}
