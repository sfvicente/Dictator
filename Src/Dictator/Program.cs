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
            using IHost host = CreateHostBuilder(args).Build();

            Game game = host.Services.GetRequiredService<Game>();

            game.Start();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
                services.AddScoped<IEngine, Engine>()
                    .AddScoped<IUserInterface, UserInterface>()
            .AddScoped<IIntroScreen, IntroScreen>()
            .AddScoped<IWelcomeScreen, WelcomeScreen>()
            .AddScoped<ITitleScreen, TitleScreen>()
            .AddScoped<IAccount, Account>()
            .AddSingleton<Game>()
        //.AddSingleton<ISingletonOperation, DefaultOperation>()
        //.AddTransient<OperationLogger>()
        );
    }
}
