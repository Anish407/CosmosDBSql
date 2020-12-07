using System;
using System.Threading.Tasks;

namespace CosmosDBSql
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await new Startup().Run();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Flatten());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
