using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Advice;
using Dictator.ConsoleInterface.Assassination;
using Dictator.ConsoleInterface.Audience;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.End;
using Dictator.ConsoleInterface.Escape;
using Dictator.ConsoleInterface.Month;
using Dictator.ConsoleInterface.News;
using Dictator.ConsoleInterface.Reporting;
using Dictator.ConsoleInterface.PresidentialDecision;
using Dictator.ConsoleInterface.Revolution;
using Dictator.ConsoleInterface.Start;
using Dictator.ConsoleInterface.Treasury;
using Dictator.ConsoleInterface.War;
using Dictator.Core;
using Dictator.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        /// <summary>
        ///     Creates a new instance of the host builder with all the required services added to the contaier.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
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
            .AddScoped<ILoanApplicationScreen, LoanApplicationScreen>()
            .AddScoped<ILoanApplicationResultScreen, LoanApplicationResultScreen>()
            .AddScoped<ITransferToSwissBankAccountScreen, TransferToSwissBankAccountScreen>()
            .AddScoped<IAssassinationScreen, AssassinationScreen>()
            .AddScoped<IAssassinationFailedScreen, AssassinationFailedScreen>()
            .AddScoped<IAssassinationSuccededScreen, AssassinationSuccededScreen>()
            .AddScoped<IRevolutionScreen, RevolutionScreen>()
            .AddScoped<IEscapeAttemptDialog, EscapeAttemptDialog>()
            .AddScoped<IHelicopterEscapeScreen, HelicopterEscapeScreen>()
            .AddScoped<IHelicopterWontStartScreen, HelicopterWontStartScreen>()
            .AddScoped<IHelicopterEngineFailureScreen, HelicopterEngineFailureScreen>()
            .AddScoped<IEscapeToLeftotoScreen, EscapeToLeftotoScreen>()
            .AddScoped<IGuerillasCelebratingScreen, GuerillasCelebratingScreen>()
            .AddScoped<IGuerillasMissedScreen, GuerillasMissedScreen>()
            .AddScoped<IRevolutionScreen, RevolutionScreen>()
            .AddScoped<IRevolutionAskForHelpDialog, RevolutionAskForHelpDialog>()
            .AddScoped<IRevolutionNoAlliesScreen, RevolutionNoAlliesScreen>()
            .AddScoped<IRevolutionAllyLowPopularityScreen, RevolutionAllyLowPopularityScreen>()
            .AddScoped<IRevolutionStartedScreen, RevolutionStartedScreen>()
            .AddScoped<IRevolutionOverthrownScreen, RevolutionOverthrownScreen>()
            .AddScoped<IRevolutionCrushedDialog, RevolutionCrushedDialog>()
            .AddScoped<IWarThreatScreen, WarThreatScreen>()
            .AddScoped<IWarLeftotoInvadesScreen, WarLeftotoInvadesScreen>()
            .AddScoped<IWarWonScreen, WarWonScreen>()
            .AddScoped<IWarLostScreen, WarLostScreen>()
            .AddScoped<IWarExecutionScreen, WarExecutionScreen>()
            .AddScoped<IEndScreen, EndScreen>()
            // Controls: Specific
            .AddScoped<IAccountControl, AccountControl>()
            // Controls: Generic
            .AddScoped<IKeyPanel, KeyPanel>()
            .AddScoped<IPressAnyKeyControl, PressAnyKeyControl>()
            .AddScoped<IPressAnyKeyOrOptionControl, PressAnyKeyOrOptionControl>()
            .AddScoped<IPressAnyKeyWithYesControl, PressAnyKeyWithYesControl>()
            // Models
            .AddScoped<IAccount, Account>()
            .AddScoped<IGovernment, Government>()
            .AddScoped<IRevolution, Revolution>()
            // Services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IGovernmentService, GovernmentService>()
            .AddScoped<IGroupService, GroupService>()
            .AddScoped<IDecisionService, DecisionService>()
            .AddScoped<INewsService, NewsService>()
            .AddScoped<IAudienceService, AudienceService>()
            .AddScoped<IPlotService, PlotService>()
            .AddScoped<IScoreService, ScoreService>()
            .AddScoped<IRevolutionService, RevolutionService>()
            .AddScoped<IEscapeService, EscapeService>()
            .AddScoped<ILoanService, LoanService>()
            .AddScoped<IAssassinationService, AssassinationService>()
            .AddScoped<IWarService, WarService>()
            .AddScoped<IReportService, ReportService>()
        );
    }
}
