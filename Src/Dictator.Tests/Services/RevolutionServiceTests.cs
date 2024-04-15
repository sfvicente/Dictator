using Dictator.Core.Services;
using Dictator.Core.Models;
using Dictator.Core;

namespace Dictator.Tests;

[TestFixture]
public class RevolutionServiceTests
{
    private Mock<IRandomService> _randomServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IStatsService> _statsServiceMock;
    private Mock<IGovernmentService> _governmentServiceMock;
    private Mock<IRevolution> _revolutionMock;
    private RevolutionService _revolutionService;

    [SetUp]
    public void SetUp()
    {
        _randomServiceMock = new Mock<IRandomService>();
        _groupServiceMock = new Mock<IGroupService>();
        _statsServiceMock = new Mock<IStatsService>();
        _governmentServiceMock = new Mock<IGovernmentService>();
        _revolutionMock = new Mock<IRevolution>();

        _revolutionService = new RevolutionService(
            _randomServiceMock.Object,
            _revolutionMock.Object,
            _groupServiceMock.Object,
            _statsServiceMock.Object,
            _governmentServiceMock.Object);
    }

    [Test]
    public void TryTriggerRevoltGroup_WhenRevoltGroupExists_ReturnsTrue()
    {
        // Arrange
        var group = new Group(GroupType.Army, 20, 10, string.Empty, string.Empty);

        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns([group, null, null, null, null, null]);
        _randomServiceMock
            .SetupSequence(r => r.Next(3))
            .Returns(0)
            .Returns(1)
            .Returns(2);
        group.Status = GroupStatus.Revolution;

        // Act
        bool result = _revolutionService.TryTriggerRevoltGroup();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void TryTriggerRevoltGroup_WhenRevoltGroupDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var group = new Group(GroupType.Army, 20, 10, string.Empty, string.Empty);

        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns([null, null, null, null, null, null]);
        _randomServiceMock
            .SetupSequence(r => r.Next(3))
            .Returns(0)
            .Returns(1)
            .Returns(2);

        // Act
        bool result = _revolutionService.TryTriggerRevoltGroup();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void GetRevolutionary_WhenRevolutionaryExists_ReturnsRevolutionary()
    {
        // Arrange
        var revolutionaryGroup = new Group(GroupType.Army, 20, 10, "Army", string.Empty);
        var revolutionaryAlly = new Group(GroupType.Peasants, 30, 10, "Peasants", string.Empty);

        revolutionaryGroup.Ally = revolutionaryAlly;
        _revolutionMock
            .SetupGet(r => r.RevolutionaryGroup)
            .Returns(revolutionaryGroup);
        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns([revolutionaryGroup, revolutionaryAlly]);

        // Act
        var result = _revolutionService.GetRevolutionary();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Army", result.RevolutionaryGroupName);
        Assert.AreEqual("Peasants", result.RevolutionaryGroupAllyName);
        Assert.AreEqual(50, result.CombinedStrength);
    }

    [Test]
    public void GetRevolutionary_WhenRevolutionaryDoesNotExist_ReturnsNullRevolutionary()
    {
        // Arrange
        _revolutionMock
            .SetupGet(r => r.RevolutionaryGroup)
            .Returns((Group)null);

        // Act
        var result = _revolutionService.GetRevolutionary();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNull(result.RevolutionaryGroupName);
        Assert.IsNull(result.RevolutionaryGroupAllyName);
        Assert.AreEqual(0, result.CombinedStrength);
    }

    [Test]
    public void FindPossibleAllies_ShouldReturnDictionaryOfPossibleAllies()
    {
        // Arrange
        var group1 = new Group(GroupType.Army, 30, 10, string.Empty, string.Empty);
        var group2 = new Group(GroupType.Peasants, 20, 10, string.Empty, string.Empty);
        var group3 = new Group(GroupType.Landowners, 15, 10, string.Empty, string.Empty);
        var group4 = new Group(GroupType.SecretPolice, 5, 10, string.Empty, string.Empty);
        var group5 = new Group(GroupType.Americans, 10, 10, string.Empty, string.Empty);
        var group6 = new Group(GroupType.Russians, 25, 10, string.Empty, string.Empty);

        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns([group1, group2, group3, group4, group5, group6]);
        _statsServiceMock
            .Setup(s => s.GetMonthlyMinimalPopularityAndStrength())
            .Returns(15);

        // Act
        var result = _revolutionService.FindPossibleAllies();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
        Assert.IsTrue(result.ContainsKey(1));
        Assert.IsTrue(result.ContainsKey(2));
        Assert.IsTrue(result.ContainsKey(6));
    }

    [Test]
    public void DoesGroupAcceptAllianceInRevolution_WhenGroupAcceptsAlliance_ReturnsTrue()
    {
        // Arrange
        var group = new Group(GroupType.Army, 30, 10, string.Empty, string.Empty);

        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns([group]);
        _statsServiceMock
            .Setup(s => s.GetMonthlyMinimalPopularityAndStrength())
            .Returns(20);

        // Act
        var result = _revolutionService.DoesGroupAcceptAllianceInRevolution(1);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void DoesGroupAcceptAllianceInRevolution_WhenGroupDoesNotAcceptAlliance_ReturnsFalse()
    {
        // Arrange
        var group = new Group(GroupType.Army, 10, 10, string.Empty, string.Empty);

        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns([group]);
        _statsServiceMock
            .Setup(s => s.GetMonthlyMinimalPopularityAndStrength())
            .Returns(20);

        // Act
        var result = _revolutionService.DoesGroupAcceptAllianceInRevolution(1);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void SetPlayerAllyForRevolution_ShouldSetPlayerAlly()
    {
        // Arrange
        var group = new Group(GroupType.Army, 30, 10, string.Empty, string.Empty);

        _groupServiceMock
            .Setup(g => g.GetGroupById(1))
            .Returns(group);

        // Act
        _revolutionService.SetPlayerAllyForRevolution(1);

        // Assert
        _revolutionMock.VerifySet(r => r.PlayerAlly = group, Times.Once);
    }

    [Test]
    public void DoesRevolutionSucceed_WhenRevolutionSucceeds_ReturnsTrue()
    {
        // Arrange
        _revolutionMock.SetupGet(r => r.RevolutionStrength).Returns(10);
        _revolutionMock.SetupGet(r => r.PlayerStrength).Returns(5);
        _randomServiceMock.Setup(r => r.Next(3)).Returns(1);

        // Act
        var result = _revolutionService.DoesRevolutionSucceed();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void DoesRevolutionSucceed_WhenRevolutionFails_ReturnsFalse()
    {
        // Arrange
        _revolutionMock.SetupGet(r => r.RevolutionStrength).Returns(5);
        _revolutionMock.SetupGet(r => r.PlayerStrength).Returns(10);
        _randomServiceMock.Setup(r => r.Next(3)).Returns(1);

        // Act
        var result = _revolutionService.DoesRevolutionSucceed();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void PunishRevolutionaries_ShouldResetRevolutionariesStrengthAndPopularity()
    {
        // Arrange
        var revolutionaryGroup = new Group(GroupType.Army, 30, 10, string.Empty, string.Empty);
        var revolutionaryAlly = new Group(GroupType.Peasants, 30, 10, string.Empty, string.Empty);
        
        _revolutionMock
            .SetupGet(r => r.RevolutionaryGroup)
            .Returns(revolutionaryGroup);
        revolutionaryGroup.Ally = revolutionaryAlly;

        // Act
        _revolutionService.PunishRevolutionaries();

        // Assert
        _groupServiceMock.Verify(g => g.SetStrength(revolutionaryGroup.Type, 0), Times.Once);
        _groupServiceMock.Verify(g => g.SetPopularity(revolutionaryGroup.Type, 0), Times.Once);
        _groupServiceMock.Verify(g => g.SetStrength(revolutionaryAlly.Type, 0), Times.Once);
        _groupServiceMock.Verify(g => g.SetPopularity(revolutionaryAlly.Type, 0), Times.Once);
    }

    [Test]
    public void ApplyRevolutionCrushedEffects_ShouldApplyEffects()
    {
        // Arrange
        _revolutionMock
            .SetupGet(r => r.PlayerAlly)
            .Returns(new Group(GroupType.Army, 20, 10, string.Empty, string.Empty));

        // Act
        _revolutionService.ApplyRevolutionCrushedEffects();

        // Assert
        _groupServiceMock.Verify(g => g.SetPopularity(GroupType.Army, 9), Times.Once);
        _governmentServiceMock.Verify(g => g.SetPlotBonus(It.IsAny<int>()), Times.Once);
        _groupServiceMock.Verify(g => g.ResetStatusAndAllies(), Times.Once);
    }
}
