using CosmosDBSql.Business.Core;
using CosmosDBSql.Business.Infra;
using CosmosDBSql.Business.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBSql
{
    public class Startup
    {

        public async Task Run()
        {
            int totalUsers;
            try
            {
                Console.WriteLine("Enter number of users to insert");
                totalUsers = int.Parse(Console.ReadLine());

                IServiceProvider serviceProvider = ConfigureServices();
                IUserService userService = GetImplementation<IUserService>();
                ICosmosService cosmosService = GetImplementation<ICosmosService>();

                for (int i = 1; i <= totalUsers ; i++)
                {
                    Rootobject apiUser = await userService.GetUser();
                    var cosmosUser = apiUser.results.FirstOrDefault();
                    await cosmosService.Insert(new User
                    { 
                        Id=cosmosUser?.id?.value ?? Guid.NewGuid().ToString(),
                        FirstName= cosmosUser.name.first, 
                        Age=cosmosUser.dob.age,
                        LastName= cosmosUser.name.last
                    }, cosmosUser.name.first);

                    Console.WriteLine($"Inserted: {cosmosUser.name.first}");
                }

                IServiceProvider ConfigureServices()
                {
                    IServiceCollection services = new ServiceCollection();
                    var configuration = new ConfigurationBuilder()
                             .AddJsonFile(
                    Path.Combine(AppContext.BaseDirectory, string.Format("..{0}..{0}..{0}", Path.DirectorySeparatorChar), "config.json"),
                    optional: true
                ).Build();

                    services.Configure<CosmosConfig>(cfg=> {
                        cfg.ContainerName = configuration["Cosmos:ContainerName"];
                        cfg.Account = configuration["Cosmos:Account"];
                        cfg.DbName = configuration["Cosmos:DbName"];
                        cfg.Key = configuration["Cosmos:Key"];
                    });

                    return services
                       .AddScoped<IUserService, UserService>()
                       .AddScoped<ICosmosService, CosmosService>()
                       .AddHttpClient()
                       .BuildServiceProvider();
                }

                TImplementation GetImplementation<TImplementation>()
                {
                    try
                    {
                        return serviceProvider.GetRequiredService<TImplementation>();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (AggregateException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

            Console.WriteLine($"Completed: Inserted {totalUsers} users");
            Console.ReadKey();


        }

    }
}
