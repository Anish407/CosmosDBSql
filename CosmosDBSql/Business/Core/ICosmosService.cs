using System.Threading.Tasks;

namespace CosmosDBSql.Business.Core
{
    public interface ICosmosService
    {
        Task Insert<T>(T item, string partitionKey);
    }
}