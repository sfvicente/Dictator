using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Revolution;
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
            .AddScoped<IAudienceDecisionDialog, AudienceDecisionDialog>()
            .AddScoped<IAdviceRequestDialog, AdviceRequestDialog>()
            .AddScoped<IAdviceScreen, AdviceScreen>()
            .AddScoped<IBankruptcyScreen, BankruptcyScreen>()
            .AddScoped<INewsflashScreen, NewsflashScreen>()
            .AddScoped<IMonthScreen, MonthScreen>()
            .AddScoped<IPresidentialDecisionMainDialog, PresidentialDecisionMainDialog>()
            .AddScoped<IPresidentialDecisionSubDialog, PresidentialDecisionSubDialog>()
            .AddScoped<IPresidentialDecisionActionDialog, PresidentialDecisionActionDialog>()
            .AddScoped<ILoanScreen, LoanScreen>()
            .AddScoped<IAssassinationScreen, AssassinationScreen>()
            .AddScoped<IAssassinationFailedScreen, AssassinationFailedScreen>()
            .AddScoped<IAssassinationSuccededScreen, AssassinationSuccededScreen>()
            .AddScoped<IRevolutionScreen, RevolutionScreen>()
            .AddScoped<IEscapeAttemptDialog, EscapeAttemptDialog>()
            .AddScoped<IEscapeByHelicopterScreen, EscapeByHelicopterScreen>()
            .AddScoped<IEscapeByHelicopterFailScreen, EscapeByHelicopterFailScreen>()
            .AddScoped<IEscapeToLeftotoScreen, EscapeToLeftotoScreen>()
            .AddScoped<IRevolutionAskForHelpDialog, RevolutionAskForHelpDialog>()
            .AddScoped<IRevolutionNoAlliesScreen, RevolutionNoAlliesScreen>()
            .AddScoped<IRevolutionAllyLowPopularityScreen, RevolutionAllyLowPopularityScreen>()
            .AddScoped<IRevolutionStartedScreen, RevolutionStartedScreen>()
            .AddScoped<IRevolutionOverthrownScreen, RevolutionOverthrownScreen>()
            .AddScoped<IRevolutionCrushedScreen, RevolutionCrushedScreen>()
            .AddScoped<IWarThreatScreen, WarThreatScreen>()
            .AddScoped<IWarScreen, WarScreen>()
            .AddScoped<IWarResultScreen, WarResultScreen>()
            .AddScoped<IEndScreen, EndScreen>()
            .AddScoped<IKeyPanel, KeyPanel>()
            .AddScoped<IPressAnyKeyControl, PressAnyKeyControl>()
            .AddScoped<IPressAnyKeyOrOptionControl, PressAnyKeyOrOptionControl>()
            .AddScoped<IPressAnyKeyWithYesControl, PressAnyKeyWithYesControl>()
            .AddScoped<IAccount, Account>()
            .AddScoped<IGovernmentStats, GovernmentStats>()
            .AddScoped<IGroupStats, GroupStats>()
            .AddScoped<IDecisionStats, DecisionStats>()
            .AddScoped<INewsStats, NewsStats>()
            .AddScoped<IAudienceStats, AudienceStats>()
            .AddSingleton<Game>()
        //.AddSingleton<ISingletonOperation, DefaultOperation>()
        //.AddTransient<OperationLogger>()
        );
    }
}
