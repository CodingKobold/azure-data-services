using Azure.Data.Tables;
using CodingKobold.DataStores.Api.Builders;
using CodingKobold.DataStores.Api.Entities;
using CodingKobold.DataStores.Api.Factories;
using CodingKobold.DataStores.Api.Helpers;
using CodingKobold.DataStores.Api.Mappers;
using CodingKobold.DataStores.Api.Models;

namespace CodingKobold.DataStores.Api.Services
{
    public interface IRatedDaysService
    {
        IReadOnlyList<RatedDay> Get(RatedDayFilters filters);
        bool Add(RatedDay day);
    }

    internal class RatedDaysService : IRatedDaysService
    {
        private readonly TableClient _tableClient;
        private readonly IRatedDayTableEntityMapper _tableEntityMapper;
        private readonly IRatedDaysTableFilterBuilder _tableFilterBuilder;

        public RatedDaysService(ITableClientFactory tableClientFactory,
            IRatedDayTableEntityMapper dayTableEntityMapper,
            IRatedDaysTableFilterBuilder tableFilterBuilder)
        {
            _tableClient = tableClientFactory.GetClient(StorageTableNames.RatedDays);
            _tableEntityMapper = dayTableEntityMapper;
            _tableFilterBuilder = tableFilterBuilder;
        }

        public IReadOnlyList<RatedDay> Get(RatedDayFilters filters)
        {
            var oDataFilter = _tableFilterBuilder
                .EqualTo(e => e.PartitionKey, filters.Type)
                .GreaterOrEqualThan(e => e.RowKey, filters.DateFrom)
                .LesserOrEqualThan(e => e.RowKey, filters.DateTo)
                .Build();

            var entities = _tableClient.Query<RatedDayEntity>(oDataFilter).ToList();

            return _tableEntityMapper.Map(entities);
        }

        public bool Add(RatedDay day)
        {
            var dayEntity = _tableEntityMapper.Map(day);
            var response = _tableClient.AddEntity(dayEntity);
            return !response.IsError;
        }
    }
}
