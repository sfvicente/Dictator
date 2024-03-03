using Dictator.Common.Extensions;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to reporting within the game.
/// </summary>
public interface IReportService
{
    /// <summary>
    ///     Retrieves a request for a detailed account or statement from police on the state of the groups and government.
    /// </summary>
    /// <returns>A police report request with information on the popularity and strength requirements.</returns>
    public PoliceReportRequest RequestPoliceReport();

    /// <summary>
    ///     Retrieves the police report with the current month, revolution strength, player strength and group information.
    /// </summary>
    /// <returns>The police report with the current month, revolution strength, player strength and group information.</returns>
    public PoliceReport GetPoliceReport();
}

/// <summary>
///     Provides functionality related to reporting within the game.
/// </summary>
public class ReportService : IReportService
{
    private readonly IAccountService accountService;
    private readonly IGroupService groupService;
    private readonly IGovernmentService governmentService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ReportService"/> class from a <see cref="IAccountService"/>,
    ///     a <see cref="IGroupService"/> and a <see cref="IGovernmentService"/> components.
    /// </summary>
    /// <param name="accountService">The service used to provide functionality related to the treasury and associated costs and
    /// the Swiss bank account.</param>
    /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
    /// <param name="governmentService">The service used to provide functionality related to the government settings and operations.</param>
    public ReportService(IAccountService accountService, IGroupService groupService, IGovernmentService governmentService)
    {
        this.accountService = accountService;
        this.groupService = groupService;
        this.governmentService = governmentService;
    }

    /// <summary>
    ///     Retrieves a request for a detailed account or statement from police on the state of the groups and government.
    /// </summary>
    /// <returns>A police report request with information on the popularity and strength requirements.</returns>
    public PoliceReportRequest RequestPoliceReport()
    {
        PoliceReportRequest policeReportRequest = new PoliceReportRequest
        {
            HasEnoughBalance = accountService.GetTreasuryBalance() > 0,
            IsPlayerPopularWithSecretPolice = IsPlayerPopularWithSecretPolice(),
            HasPoliceEnoughStrength = HasPoliceEnoughStrength(),
            PolicePopularity = groupService.GetPopularityByGroupType(GroupType.SecretPolice),
            PoliceStrength = groupService.GetStrengthByGroupType(GroupType.SecretPolice)
        };

        return policeReportRequest;
    }

    /// <summary>
    ///     Retrieves the police report with the current month, revolution strength, player strength and group information.
    /// </summary>
    /// <returns>The police report with the current month, revolution strength, player strength and group information.</returns>
    public PoliceReport GetPoliceReport()
    {
        PoliceReport policeReport = new PoliceReport
        {
            Month = governmentService.GetMonth(),
            Groups = groupService.GetGroups().AsReadOnly(),
            PlayerStrength = governmentService.GetPlayerStrength(),
            MonthlyRevolutionStrength = governmentService.GetMonthlyRevolutionStrength()
        };

        return policeReport;
    }

    /// <summary>
    ///     Determines if the player is popular with the secret police.
    /// </summary>
    /// <returns><c>true</c> if the player is popular with the secret police; otherwise, <c>false</c>.</returns>
    private bool IsPlayerPopularWithSecretPolice()
    {
        int secretPolicePopularity = groupService.GetPopularityByGroupType(GroupType.SecretPolice);

        return secretPolicePopularity > governmentService.GetMonthlyMinimalPopularityAndStrength();
    }

    /// <summary>
    ///     Determines if the level of the police police strength is greater than the minimal requirement for the current month.
    /// </summary>
    /// <returns><c>true</c> if police has enough; otherwise, <c>false</c>.</returns>
    private bool HasPoliceEnoughStrength()
    {
        int policeStrength = groupService.GetStrengthByGroupType(GroupType.SecretPolice);

        return policeStrength > governmentService.GetMonthlyMinimalPopularityAndStrength();
    }
}
