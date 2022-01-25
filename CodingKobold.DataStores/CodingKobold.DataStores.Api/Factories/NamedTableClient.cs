using Azure.Data.Tables;

namespace CodingKobold.DataStores.Api.Factories
{
    public class NamedTableClient
    {
        public string Name { get; private set; }
        public TableClient Client { get; private set; }

        public NamedTableClient(string name, TableClient client)
        {
            Name = name;
            Client = client;
        }
    }
}
