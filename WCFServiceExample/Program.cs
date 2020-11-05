using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WCFServiceExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = CreateHostBuilder(args).Build();
            var example = serviceProvider.Services.GetRequiredService<ExampleRun>();
            await example.TestAsync();

        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
              
                services.AddTransient<ExampleRun>();
            });
    }
}
