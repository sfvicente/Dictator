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
            .AddScoped<ITreasuryReportScreen, TreasuryReportScreen>()
            .AddScoped<IPoliceReportRequestDialog, PoliceReportRequestDialog>()
            .AddScoped<IPoliceReportScreen, PoliceReportScreen>()
            .AddScoped<IAudienceScreen, AudienceScreen>()
            .AddScoped<IBankruptcyScreen, BankruptcyScreen>()
            .AddScoped<INewsflashScreen, NewsflashScreen>()
            .AddScoped<IMonthScreen, MonthScreen>()
            .AddScoped<IDecisionMainDialog, DecisionMainDialog>()
            .AddScoped<IDecisionSubDialog, DecisionSubDialog>()
            .AddScoped<IAssassinationScreen, AssassinationScreen>()
            .AddScoped<IRevolutionScreen, RevolutionScreen>()
            .AddScoped<IEscapeAttemptScreen, EscapeAttemptScreen>()
            .AddScoped<IEscapeByHelicopterScreen, EscapeByHelicopterScreen>()
            .AddScoped<IEscapeByHelicopterFailScreen, EscapeByHelicopterFailScreen>()
            .AddScoped<IEscapeToLeftotoScreen, EscapeToLeftotoScreen>()
            .AddScoped<IRevolutionOverthrownScreen, RevolutionOverthrownScreen>()
            .AddScoped<IRevolutionCrushedScreen, RevolutionCrushedScreen>()
            .AddScoped<IAdviceRequestScreen, AdviceRequestScreen>()
            .AddScoped<IWarThreatScreen, WarThreatScreen>()
            .AddScoped<IWarScreen, WarScreen>()
            .AddScoped<IEndScreen, EndScreen>()
            .AddScoped<IPressAnyKeyControl, PressAnyKeyControl>()
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
