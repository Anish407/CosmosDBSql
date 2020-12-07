using CosmosDBSql.Business.Model;
using System.Threading.Tasks;

namespace CosmosDBSql.Business.Core
{
    public interface IUserService
    {
        Task<Rootobject> GetUser();
    }
}