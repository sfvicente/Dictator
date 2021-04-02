﻿using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Advice;
using Dictator.ConsoleInterface.Assassination;
using Dictator.ConsoleInterface.Audience;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.End;
using Dictator.ConsoleInterface.Escape;
using Dictator.ConsoleInterface.Month;
using Dictator.ConsoleInterface.News;
using Dictator.ConsoleInterface.PoliceReport;
using Dictator.ConsoleInterface.PresidentialDecision;
using Dictator.ConsoleInterface.Revolution;
using Dictator.ConsoleInterface.Start;
using Dictator.ConsoleInterface.Treasury;
using Dictator.ConsoleInterface.War;
using Dictator.Core;
using Dictator.Core.Services;
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
            Host.CreateDefaultBuilder(args).ConfigureServices((_, services) => services
            // Main
            .AddSingleton<Game>()
            .AddScoped<IEngine, Engine>()
            .AddScoped<IUserInterface, UserInterface>()
            // Screens
            .AddScoped<IIntroScreen, IntroScreen>()
            .AddScoped<IWelcomeScreen, WelcomeScreen>()
            .AddScoped<ITitleScreen, TitleScreen>()
            .AddScoped<ITreasuryReportScreen, TreasuryReportScreen>()
            .AddScoped<IBankruptcyScreen, BankruptcyScreen>()
            .AddScoped<IPoliceReportRequestDialog, PoliceReportRequestDialog>()
            .AddScoped<IPoliceReportScreen, PoliceReportScreen>()
            .AddScoped<IAudienceScreen, AudienceScreen>()
            .AddScoped<IAudienceDecisionDialog, AudienceDecisionDialog>()
            .AddScoped<IAdviceRequestDialog, AdviceRequestDialog>()
            .AddScoped<IAdviceScreen, AdviceScreen>()
            .AddScoped<INewsflashScreen, NewsflashScreen>()
            .AddScoped<IMonthScreen, MonthScreen>()
            .AddScoped<IPresidentialDecisionMainDialog, PresidentialDecisionMainDialog>()
            .AddScoped<IPresidentialDecisionSubDialog, PresidentialDecisionSubDialog>()
            .AddScoped<IPresidentialDecisionActionDialog, PresidentialDecisionActionDialog>()
            .AddScoped<ILoanScreen, LoanScreen>()
            .AddScoped<ITransferToSwissBankAccount, TransferToSwissBankAccount>()
            .AddScoped<IAssassinationScreen, AssassinationScreen>()
            .AddScoped<IAssassinationFailedScreen, AssassinationFailedScreen>()
            .AddScoped<IAssassinationSuccededScreen, AssassinationSuccededScreen>()
            .AddScoped<IRevolutionScreen, RevolutionScreen>()
            .AddScoped<IEscapeAttemptDialog, EscapeAttemptDialog>()
            .AddScoped<IEscapeByHelicopterScreen, EscapeByHelicopterScreen>()
            .AddScoped<IEscapeByHelicopterFailScreen, EscapeByHelicopterFailScreen>()
            .AddScoped<IEscapeToLeftotoScreen, EscapeToLeftotoScreen>()
            .AddScoped<IRevolutionScreen, RevolutionScreen>()
            .AddScoped<IRevolutionAskForHelpDialog, RevolutionAskForHelpDialog>()
            .AddScoped<IRevolutionNoAlliesScreen, RevolutionNoAlliesScreen>()
            .AddScoped<IRevolutionAllyLowPopularityScreen, RevolutionAllyLowPopularityScreen>()
            .AddScoped<IRevolutionStartedScreen, RevolutionStartedScreen>()
            .AddScoped<IRevolutionOverthrownScreen, RevolutionOverthrownScreen>()
            .AddScoped<IRevolutionCrushedScreen, RevolutionCrushedScreen>()
            .AddScoped<IWarThreatScreen, WarThreatScreen>()
            .AddScoped<IWarScreen, WarScreen>()
            .AddScoped<IWarWonScreen, WarWonScreen>()
            .AddScoped<IWarLostScreen, WarLostScreen>()
            .AddScoped<IEndScreen, EndScreen>()
            // Controls
            .AddScoped<IKeyPanel, KeyPanel>()
            .AddScoped<IPressAnyKeyControl, PressAnyKeyControl>()
            .AddScoped<IPressAnyKeyOrOptionControl, PressAnyKeyOrOptionControl>()
            .AddScoped<IPressAnyKeyWithYesControl, PressAnyKeyWithYesControl>()
            // Models
            .AddScoped<IAccount, Account>()
            .AddScoped<IGovernmentStats, GovernmentStats>()
            .AddScoped<IGroupStats, GroupStats>()
            .AddScoped<IDecisionStats, DecisionStats>()
            .AddScoped<INewsStats, NewsStats>()
            .AddScoped<IAudienceStats, AudienceStats>()
            // Services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IPlotService, PlotService>()
            .AddScoped<IScoreService, ScoreService>()
        );
    }
}
