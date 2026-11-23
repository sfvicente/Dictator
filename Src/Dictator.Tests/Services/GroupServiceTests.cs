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

    [Test]
    public void ApplyPopularityChange_AppliesEncodedOffsetsAndClampsWithinBounds()
    {
        // 'Q' is +4 (7 + 4 -> clamped to 9), 'I' is -4 (7 - 4 = 3), 'M' is 0
        _groupService.ApplyPopularityChange("QIMMMMMM");

        Assert.AreEqual(9, _groupService.GetPopularityByGroupType(GroupType.Army));
        Assert.AreEqual(3, _groupService.GetPopularityByGroupType(GroupType.Peasants));
        Assert.AreEqual(7, _groupService.GetPopularityByGroupType(GroupType.Landowners));
    }

    [Test]
    public void ApplyStrengthChange_AppliesEncodedOffsetsAndClampsWithinBounds()
    {
        // 'P' is +3 (6 + 3 = 9), 'K' is -2 (6 - 2 = 4), 'M' is 0
        _groupService.ApplyStrengthChange("PKMMMM");

        Assert.AreEqual(9, _groupService.GetStrengthByGroupType(GroupType.Army));
        Assert.AreEqual(4, _groupService.GetStrengthByGroupType(GroupType.Peasants));
        Assert.AreEqual(6, _groupService.GetStrengthByGroupType(GroupType.Landowners));
    }

    [Test]
    public void DoesMainPopulationHatePlayer_ReturnsTrueOnlyWhenAllThreePlotAssassination()
    {
        Assert.IsFalse(_groupService.DoesMainPopulationHatePlayer());

        _groupService.GetGroupByType(GroupType.Army).Status = GroupStatus.Assassination;
        _groupService.GetGroupByType(GroupType.Peasants).Status = GroupStatus.Assassination;
        Assert.IsFalse(_groupService.DoesMainPopulationHatePlayer());

        _groupService.GetGroupByType(GroupType.Landowners).Status = GroupStatus.Assassination;
        Assert.IsTrue(_groupService.DoesMainPopulationHatePlayer());
    }

    [Test]
    public void ResetStatusAndAllies_ClearsMainPopulationStatusAndAllies()
    {
        Group army = _groupService.GetGroupByType(GroupType.Army);
        army.Status = GroupStatus.Revolution;
        army.Ally = _groupService.GetGroupByType(GroupType.Guerillas);

        _groupService.ResetStatusAndAllies();

        Assert.AreEqual(GroupStatus.Default, army.Status);
        Assert.IsNull(army.Ally);
    }
}
