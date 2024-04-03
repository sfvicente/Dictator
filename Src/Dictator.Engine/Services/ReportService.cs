using Dictator.Common.Extensions;
using Dictator.Core.Models;

namespace Dictator.Core.Services;

public interface IReportService
{
    PoliceReportRequest RequestPoliceReport();
    PoliceReport GetPoliceReport();
    bool IsPlayerPopularWithSecretPolice();
    bool HasPoliceEnoughStrength();
}

public class ReportService : IReportService
{
    private readonly IAccountService _accountService;
    private readonly IGroupService _groupService;
    private readonly IStatsService _statsService;
    private readonly IGovernmentService _governmentService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ReportService"/> class from a <see cref="IAccountService"/>,
    ///     a <see cref="IGroupService"/> and a <see cref="IGovernmentService"/> components.
    /// </summary>
    /// <param name="accountService">The service used to provide functionality related to the treasury and associated costs and
    /// the Swiss bank account.</param>
    /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
    /// <param name="governmentService">The service used to provide functionality related to the government settings and operations.</param>
    public ReportService(
        IAccountService accountService,
        IGroupService groupService,
        IStatsService statsService,
        IGovernmentService governmentService)
    {
        _accountService = accountService;
        _groupService = groupService;
        _statsService = statsService;
        _governmentService = governmentService;
    }

    /// <summary>
    ///     Retrieves a request for a detailed account or statement from police on the state of the groups and government.
    /// </summary>
    /// <returns>A police report request with information on the popularity and strength requirements.</returns>
    public PoliceReportRequest RequestPoliceReport()
    {
        PoliceReportRequest policeReportRequest = new()
        {
            HasEnoughBalance = _accountService.GetTreasuryBalance() > 0,
            IsPlayerPopularWithSecretPolice = IsPlayerPopularWithSecretPolice(),
            HasPoliceEnoughStrength = HasPoliceEnoughStrength(),
            PolicePopularity = _groupService.GetPopularityByGroupType(GroupType.SecretPolice),
            PoliceStrength = _groupService.GetStrengthByGroupType(GroupType.SecretPolice)
        };

        return policeReportRequest;
    }

    /// <summary>
    ///     Retrieves the police report with the current month, revolution strength, player strength and group information.
    /// </summary>
    /// <returns>The police report with the current month, revolution strength, player strength and group information.</returns>
    public PoliceReport GetPoliceReport()
    {
        PoliceReport policeReport = new()
        {
            Month = _governmentService.GetMonth(),
            Groups = _groupService.GetGroups().AsReadOnly(),
            PlayerStrength = _statsService.GetPlayerStrength(),
            MonthlyRevolutionStrength = _governmentService.GetMonthlyRevolutionStrength()
        };

        return policeReport;
    }

    /// <summary>
    ///     Determines if the player is popular with the secret police.
    /// </summary>
    /// <returns><c>true</c> if the player is popular with the secret police; otherwise, <c>false</c>.</returns>
    public bool IsPlayerPopularWithSecretPolice()
    {
        int secretPolicePopularity = _groupService.GetPopularityByGroupType(GroupType.SecretPolice);

        return secretPolicePopularity > _statsService.GetMonthlyMinimalPopularityAndStrength();
    }

    /// <summary>
    ///     Determines if the level of the police police strength is greater than the minimal requirement for the current month.
    /// </summary>
    /// <returns><c>true</c> if police has enough; otherwise, <c>false</c>.</returns>
    public bool HasPoliceEnoughStrength()
    {
        int policeStrength = _groupService.GetStrengthByGroupType(GroupType.SecretPolice);

        return policeStrength > _statsService.GetMonthlyMinimalPopularityAndStrength();
    }
}
