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
            .AddScoped<IAccountScreen, AccountScreen>()
            .AddScoped<IPoliceReportRequestScreen, PoliceReportRequestScreen>()
            .AddScoped<IPoliceReportScreen, PoliceReportScreen>()
            .AddScoped<IRequestScreen, RequestScreen>()
            .AddScoped<IBankruptcyScreen, BankruptcyScreen>()
            .AddScoped<INewsflashScreen, NewsflashScreen>()
            .AddScoped<IAccount, Account>()
            .AddScoped<IGovernmentStats, GovernmentStats>()
            .AddScoped<IGroupStats, GroupStats>()
            .AddScoped<INewsStats, NewsStats>()
            .AddSingleton<Game>()
        //.AddSingleton<ISingletonOperation, DefaultOperation>()
        //.AddTransient<OperationLogger>()
        );
    }
}
