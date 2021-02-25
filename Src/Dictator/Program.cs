using Dictator.ConsoleInterface;
using Dictator.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Dictator
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.Start();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
            services.AddScoped<IAccount, Account>()
                    //.AddSingleton<ISingletonOperation, DefaultOperation>()
                    //.AddTransient<OperationLogger>()
        );
    }
}
