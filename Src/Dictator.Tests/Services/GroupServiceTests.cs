using Dictator.Core.Models;
using Dictator.Core.Services;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class GroupServiceTests
{
    private GroupService _groupService;

    [SetUp]
    public void Setup()
    {
        _groupService = new GroupService();
        _groupService.Initialise();
    }

    [Test]
    public void IncreasePopularityDoesNotExceedMaximumAllowed()
    {
        // Act
        _groupService.IncreasePopularity(GroupType.Army, 10);

        // Assert
        Assert.AreEqual(9, _groupService.GetPopularityByGroupType(GroupType.Army));
    }

    [Test]
    public void DecreasePopularityDoesNotLowerPopularityBelowZero()
    {
        // Act
        _groupService.DecreasePopularity(GroupType.Army, 20);

        // Assert
        Assert.AreEqual(0, _groupService.GetPopularityByGroupType(GroupType.Army));
    }
}
