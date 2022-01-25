using Azure;
using Azure.Data.Tables;
using CodingKobold.DataStores.Api.Models;

namespace CodingKobold.DataStores.Api.Entities
{
    public class RatedDayEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public DayRating Rating { get; set; }
        public string? Note { get; set; }
    }
}
