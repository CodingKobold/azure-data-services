using Azure.Data.Tables;
using CodingKobold.DataStores.Api.Entities;
using CodingKobold.DataStores.Api.Factories;
using CodingKobold.DataStores.Api.Helpers;

namespace CodingKobold.DataStores.Api.Services
{
    public interface ISampleEntitiesService
    {
        IReadOnlyList<SampleEntity> Get(int? key);
        bool AddOrUpdate(int key, string? value);
        bool Delete(int key);
        bool AddBatch(Dictionary<int, string> keyValues);
    }

    public class SampleEntitiesService : ISampleEntitiesService
    {
        const string PartitionKey = "Sample";

        private readonly TableClient _tableClient;

        public SampleEntitiesService(ITableClientFactory tableClientFactory)
        {
            _tableClient = tableClientFactory.GetClient(StorageTableNames.Samples);
        }

        public IReadOnlyList<SampleEntity> Get(int? key)
        {
            string? filterQuery = null;

            if (key != null)
            {
                filterQuery = $"RowKey eq '{key}'";
            }

            var entities = _tableClient.Query<SampleEntity>(filterQuery).ToList();
            return entities;
        }

        public bool AddOrUpdate(int key, string? value)
        {
            SampleEntity entity = new()
            {
                PartitionKey = PartitionKey,
                RowKey = key.ToString(),
                Value = value
            };

            var response = _tableClient.UpsertEntity(entity);
            return !response.IsError;
        }

        public bool Delete(int key)
        {
            var response = _tableClient.DeleteEntity(PartitionKey, key.ToString());
            return !response.IsError;
        }

        public bool AddBatch(Dictionary<int, string> keyValues)
        {
            var entities = keyValues.Select(kv => new SampleEntity
            {
                PartitionKey = PartitionKey,
                RowKey = kv.Key.ToString(),
                Value = kv.Value
            });

            var transaction = entities.Select(entity => new TableTransactionAction(TableTransactionActionType.Add, entity));

            var response = _tableClient.SubmitTransaction(transaction);
            return !response.GetRawResponse().IsError;
        }
    }
}
