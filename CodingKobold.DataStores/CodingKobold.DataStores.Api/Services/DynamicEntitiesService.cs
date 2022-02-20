using Azure.Data.Tables;
using CodingKobold.DataStores.Api.Factories;
using CodingKobold.DataStores.Api.Helpers;

namespace CodingKobold.DataStores.Api.Services
{
    public interface IDynamicEntitiesService
    {
        IReadOnlyList<TableEntity> Get(int? key);
        bool AddOrUpdate(int key, Dictionary<string, string> keyValues);
        bool Delete(int key);
    }

    public class DynamicEntitiesService : IDynamicEntitiesService
    {
        const string PartitionKey = "Dynamic";

        private readonly TableClient _tableClient;

        public DynamicEntitiesService(ITableClientFactory tableClientFactory)
        {
            _tableClient = tableClientFactory.GetClient(StorageTableNames.Dynamics);
        }

        public IReadOnlyList<TableEntity> Get(int? key)
        {
            var filterQuery = key != null ? $"RowKey eq '{key}'" : null;
            var entities = _tableClient.Query<TableEntity>(filterQuery).ToList();
            return entities;
        }

        public bool AddOrUpdate(int key, Dictionary<string, string> keyValues)
        {
            TableEntity entity = new(PartitionKey, key.ToString());

            foreach (var keyValue in keyValues)
            {
                entity[keyValue.Key] = keyValue.Value;
            }

            var response = _tableClient.UpsertEntity(entity);
            return !response.IsError;
        }

        public bool Delete(int key)
        {
            var response = _tableClient.DeleteEntity(PartitionKey, key.ToString());
            return !response.IsError;
        }
    }
}
