namespace CodingKobold.DataStores.Api.Mappers
{
    public interface ITableEntityMapper<TEntity, TModel>
    {
        TModel Map(TEntity entity);
        IReadOnlyList<TModel> Map(IList<TEntity> entities);

        TEntity Map(TModel model);
    }
}
