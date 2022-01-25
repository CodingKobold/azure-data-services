using CodingKobold.DataStores.Api.Entities;

namespace CodingKobold.DataStores.Api.Builders
{
    public interface IRatedDaysTableFilterBuilder : ITableFilterBuilder<RatedDayEntity>
    {
    }

    internal class RatedDaysTableFilterBuilder : BaseTableFilterBuilder<RatedDayEntity>, IRatedDaysTableFilterBuilder
    {
    }
}