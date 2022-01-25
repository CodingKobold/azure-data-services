using Azure.Data.Tables;

namespace CodingKobold.DataStores.Api.Factories
{
    public interface ITableClientFactory
    {
        TableClient GetClient(string name);
    }

    public class TableClientFactory : ITableClientFactory
    {
        private readonly IDictionary<string, TableClient> _clients;

        public TableClientFactory(IEnumerable<NamedTableClient> clients)
        {
            _clients = clients.ToDictionary(n => n.Name, n => n.Client);
        }

        public TableClient GetClient(string name)
        {
            var client = _clients[name];

            if (client == null)
            {
                throw new ArgumentException(name);
            }
            
            return client;
        }
    }
}
