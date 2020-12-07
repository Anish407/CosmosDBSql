using CosmosDBSql.Business.Core;
using CosmosDBSql.Business.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CosmosDBSql.Business.Infra
{
    public class CosmosService : ICosmosService
    {
        private Container _container;
        private CosmosClient dbClient;
        public CosmosService(IOptions<CosmosConfig> configuration)
        {
            //string databaseName = configurationSection.GetSection("DatabaseName").Value;
            //string containerName = configurationSection.GetSection("ContainerName").Value;
            //string account = configurationSection.GetSection("Account").Value;
            //string key = configurationSection.GetSection("Key").Value;

            CosmosConfig = configuration.Value;
        }

        public CosmosConfig CosmosConfig { get; }

        public async Task Insert<T>(T item, string partitionKey)
        {
            try
            {
                CosmosClient client = new CosmosClient(CosmosConfig.Account, CosmosConfig.Key);
                dbClient = new CosmosClient(CosmosConfig.Account, CosmosConfig.Key);
                DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(CosmosConfig.DbName);
                await database.Database.CreateContainerIfNotExistsAsync(CosmosConfig.ContainerName, "/FirstName");
                _container = dbClient.GetContainer(CosmosConfig.DbName, CosmosConfig.ContainerName);
                await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
            }
            catch (AggregateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
