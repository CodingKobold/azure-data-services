using CodingKobold.DataStores.Api.Entities;
using CodingKobold.DataStores.Api.Helpers;
using CodingKobold.DataStores.Api.Models;
using System.Globalization;

namespace CodingKobold.DataStores.Api.Mappers
{
    public interface IRatedDayTableEntityMapper : ITableEntityMapper<RatedDayEntity, RatedDay>
    {
    }

    internal class DayTableEntityMapper : IRatedDayTableEntityMapper
    {
        public RatedDay Map(RatedDayEntity dayEntity)
        {
            return new()
            {
                Date = DateTime.ParseExact(dayEntity.RowKey, DateTimeHelper.DateTimeFormat, CultureInfo.InvariantCulture),
                Type = Enum.Parse<RatedDayType>(dayEntity.PartitionKey),
                Rating = dayEntity.Rating,
                Note = dayEntity.Note
            };
        }

        public IReadOnlyList<RatedDay> Map(IList<RatedDayEntity> dayEntities) => dayEntities.Select(entity => Map(entity)).ToList();

        RatedDayEntity ITableEntityMapper<RatedDayEntity, RatedDay>.Map(RatedDay day)
        {
            return new()
            {
                RowKey = day.Date.Date.ToSortableString(),
                PartitionKey = day.Type.ToString(),
                Rating = day.Rating,
                Note = day.Note
            };
        }
    }
}
