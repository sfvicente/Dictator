using Dictator.Core.Models;

namespace Dictator.Core.Services;

public interface IPlotService
{
    public void Plot();
}

public class PlotService : IPlotService
{
    private readonly IGroupService _groupService;
    private readonly IPopularityService _popularityService;
    private readonly IGovernmentService _governmentService;

    public PlotService(
        IGroupService groupService, 
        IPopularityService popularityService,
        IGovernmentService governmentService)
    {
        _groupService = groupService;
        _popularityService = popularityService;
        _governmentService = governmentService;
    }

    /// <summary>
    ///     Checks for the required conditions and changes que appropriate group status
    ///     to the revolution and assassination modes.
    /// </summary>
    public void Plot()
    {
        // Do not trigger assassinations or revolutions after failed revolution
        if (_governmentService.GetMonth() < _governmentService.GetPlotBonus())
        {
            return;
        }

        // Do not trigger assassinations or revolutions in the first 2 months of government
        if (_governmentService.GetMonth() > 2)
        {
            _groupService.ResetStatusAndAllies();

            Group[] groups = _groupService.GetGroups();
            int monthlyMinimalPopularityAndStrength = _popularityService.GetMonthlyMinimalPopularityAndStrength();
            int monthlyRevolutionStrength = _governmentService.GetMonthlyRevolutionStrength();

            // Perform internal plot logic
            ExecutePlot(groups, monthlyMinimalPopularityAndStrength, monthlyRevolutionStrength);
        }
    }

    private void ExecutePlot(Group[] groups, int monthlyMinimalPopularityAndStrength, int monthlyRevolutionStrength)
    {
        for (int i = 0; i < 3; i++)
        {
            // Only groups with popularity less or equal to the monthly minimal popularity plot against the player
            if (groups[i].Popularity <= monthlyMinimalPopularityAndStrength)
            {
                // Cycle through the groups to find allies for the revolution
                for (int k = 0; k < 6; k++)
                {
                    // Skip this pairing if we are dealing with the same group or a group that has popularity above the minimal monthly popularity required
                    if (i == k || groups[k].Popularity > monthlyMinimalPopularityAndStrength)
                        continue;

                    // Verify if the strength of both groups combined is equal or greater than the monthly defined minimal revolution strength
                    if (groups[i].Strength + groups[k].Strength >= monthlyRevolutionStrength)
                    {
                        // If so, set the ally for the original group and the appropriate status
                        groups[i].Status = GroupStatus.Revolution;
                        groups[i].Ally = groups[k];
                        break;
                    }
                }

                // If no allies could be found for the group, set the group status to assassination
                if (groups[i].Status == GroupStatus.Default)
                {
                    groups[i].Status = GroupStatus.Assassination;
                }
            }
        }

    }
}
