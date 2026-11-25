using Dictator.Core.Services;
using Dictator.Core.Models;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class PlotServiceTests
{
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IStatsService> _statsServiceMock;
    private Mock<IGovernmentService> _governmentServiceMock;
    private PlotService _plotService;

    [SetUp]
    public void SetUp()
    {
        _groupServiceMock = new Mock<IGroupService>();
        _statsServiceMock = new Mock<IStatsService>();
        _governmentServiceMock = new Mock<IGovernmentService>();

        _plotService = new PlotService(_groupServiceMock.Object, _statsServiceMock.Object, _governmentServiceMock.Object);
    }

    [Test]
    public void Plot_WhenBelowPlotBonusMonth_ShouldNotTriggerAssassinationsOrRevolutions()
    {
        // Arrange
        _governmentServiceMock
            .Setup(g => g.GetMonth())
            .Returns(1);

        // Act
        _plotService.Plot();

        // Assert
        _groupServiceMock.Verify(g => g.ResetStatusAndAllies(), Times.Never);
    }

    [Test]
    public void Plot_WhenAbove2ndMonth_ShouldExecutePlotLogic()
    {
        // Arrange
        var groups = new Group[]
        {
            new Group(GroupType.Army, 30, 10, string.Empty, string.Empty),
            new Group(GroupType.Peasants, 20, 10, string.Empty, string.Empty),
            new Group(GroupType.Landowners, 15, 10, string.Empty, string.Empty),
            new Group(GroupType.SecretPolice, 5, 10, string.Empty, string.Empty),
            new Group(GroupType.Americans, 10, 10, string.Empty, string.Empty),
            new Group(GroupType.Russians, 25, 10, string.Empty, string.Empty)
        };

        _governmentServiceMock
                .Setup(g => g.GetMonth())
                .Returns(3);
        _governmentServiceMock
            .Setup(g => g.GetPlotBonus())
            .Returns(0);
        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns(groups);
        _statsServiceMock
            .Setup(s => s.GetMonthlyMinimalPopularityAndStrength())
            .Returns(2);
        _governmentServiceMock
            .Setup(g => g.GetMonthlyRevolutionStrength())
            .Returns(10);

        // Act
        _plotService.Plot();

        // Assert
        _groupServiceMock.Verify(g => g.ResetStatusAndAllies(), Times.Once);
    }

    [Test]
    public void Plot_WhenInternalPlotLogicExecuted_ShouldSetGroupStatusCorrectly()
    {
        // Arrange
        _governmentServiceMock
            .Setup(g => g.GetMonth())
            .Returns(3);
        _governmentServiceMock
            .Setup(g => g.GetPlotBonus())
            .Returns(0);

        var groups = new Group[]
        {
            new Group(GroupType.Army, 1, 2, string.Empty, string.Empty),
            new Group(GroupType.Peasants, 2, 3, string.Empty, string.Empty),
            new Group(GroupType.Landowners, 3, 4, string.Empty, string.Empty),
            new Group(GroupType.Guerillas, 5, 10, string.Empty, string.Empty),
            new Group(GroupType.Leftotans, 10, 10, string.Empty, string.Empty),
            new Group(GroupType.SecretPolice, 25, 10, string.Empty, string.Empty)

        };

        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns(groups);
        _statsServiceMock.Setup(s => s.GetMonthlyMinimalPopularityAndStrength())
            .Returns(2);
        _governmentServiceMock
            .Setup(g => g.GetMonthlyRevolutionStrength())
            .Returns(10);

        // Act
        _plotService.Plot();

        // Assert
        Assert.AreEqual(GroupStatus.Assassination, groups[0].Status);
        Assert.AreEqual(GroupStatus.Assassination, groups[1].Status);
        Assert.AreEqual(GroupStatus.Default, groups[2].Status);
    }

    [Test]
    public void Plot_WhenUnpopularGroupsHaveEnoughCombinedStrength_SetsRevolutionStatusAndAlly()
    {
        // Arrange
        _governmentServiceMock.Setup(g => g.GetMonth()).Returns(4);
        _governmentServiceMock.Setup(g => g.GetPlotBonus()).Returns(0);
        _statsServiceMock.Setup(s => s.GetMonthlyMinimalPopularityAndStrength()).Returns(3);
        _governmentServiceMock.Setup(g => g.GetMonthlyRevolutionStrength()).Returns(10);

        var groups = new Group[]
        {
            new Group(GroupType.Army, 2, 6, "The ARMY", "   ARMY   "),
            new Group(GroupType.Peasants, 8, 6, "The PEASANTS", " PEASANTS "),
            new Group(GroupType.Landowners, 8, 6, "The LANDOWNERS", "LANDOWNERS"),
            new Group(GroupType.Guerillas, 0, 5, "The GUERILLAS", "GUERILLAS "),
            new Group(GroupType.Leftotans, 7, 6, "The LEFTOTANS", "LEFTOTANS "),
            new Group(GroupType.SecretPolice, 7, 6, "The SECRET POLICE", " S.POLICE ")
        };
        _groupServiceMock.Setup(g => g.GetGroups()).Returns(groups);

        // Act
        _plotService.Plot();

        // Assert
        Assert.AreEqual(GroupStatus.Revolution, groups[0].Status);
        Assert.AreEqual(groups[3], groups[0].Ally);
    }
}
