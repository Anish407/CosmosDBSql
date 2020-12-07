using CosmosDBSql.Business.Core;
using CosmosDBSql.Business.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CosmosDBSql.Business.Infra
{
    public class UserService : IUserService
    {
        public HttpClient HttpClient { get; set; }

        public UserService(IHttpClientFactory httpClientFactory)
        {
            HttpClient = httpClientFactory.CreateClient("UserClient");
        }

        public async Task<Rootobject> GetUser()
        {
            try
            {
                var response = await HttpClient.GetAsync("https://randomuser.me/api/");
                return JsonConvert.DeserializeObject<Rootobject>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
