using Azure;
using Azure.Data.Tables;

namespace CodingKobold.DataStores.Api.Entities
{
    public class SampleEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string? Value { get; set; }
    }
}
