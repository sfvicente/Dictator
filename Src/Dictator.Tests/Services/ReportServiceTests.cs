using Dictator.Core.Models;
using Dictator.Core.Services;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class ReportServiceTests
{
    private Mock<IAccountService> _mockAccountService;
    private Mock<IGroupService> _mockGroupService;
    private Mock<IStatsService> _mockPopularityService;
    private Mock<IGovernmentService> _mockGovernmentService;
    private ReportService _reportService;

    [SetUp]
    public void Setup()
    {
        _mockAccountService = new Mock<IAccountService>();
        _mockGroupService = new Mock<IGroupService>();
        _mockPopularityService = new Mock<IStatsService>();
        _mockGovernmentService = new Mock<IGovernmentService>();
        _reportService = new ReportService(
            _mockAccountService.Object,
            _mockGroupService.Object,
            _mockPopularityService.Object,
            _mockGovernmentService.Object);
    }

    [Test]
    public void RequestPoliceReport_ReturnsValidRequest()
    {
        // Arrange
        _mockAccountService.Setup(x => x.GetTreasuryBalance()).Returns(100);
        _mockGroupService.Setup(x => x.GetPopularityByGroupType(GroupType.SecretPolice)).Returns(80);
        _mockGroupService.Setup(x => x.GetStrengthByGroupType(GroupType.SecretPolice)).Returns(90);
        _mockPopularityService.Setup(x => x.GetMonthlyMinimalPopularityAndStrength()).Returns(70);

        // Act
        PoliceReportRequest result = _reportService.RequestPoliceReport();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.HasEnoughBalance);
        Assert.IsTrue(result.IsPlayerPopularWithSecretPolice);
        Assert.IsTrue(result.HasPoliceEnoughStrength);
        Assert.AreEqual(80, result.PolicePopularity);
        Assert.AreEqual(90, result.PoliceStrength);
    }

    [Test]
    public void GetPoliceReport_ReturnsValidReport()
    {
        // Arrange
        Group[] groups = [
            new Group(GroupType.Army, 60, 80, "Army", "Army"),
            new Group(GroupType.Landowners, 70, 90, "Landowners", "Landowners")];
        _mockGroupService.Setup(x => x.GetGroups()).Returns(groups);
        _mockPopularityService.Setup(x => x.GetPlayerStrength()).Returns(150);
        _mockGovernmentService.Setup(x => x.GetMonth()).Returns(3);
        _mockGovernmentService.Setup(x => x.GetMonthlyRevolutionStrength()).Returns(200);

        // Act
        PoliceReport result = _reportService.GetPoliceReport();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Month);
        Assert.AreEqual(2, result.Groups.Count); // Assuming 2 groups are returned
        Assert.AreEqual(150, result.PlayerStrength);
        Assert.AreEqual(200, result.MonthlyRevolutionStrength);
    }

    [Test]
    public void IsPlayerPopularWithSecretPolice_ReturnsTrueWhenPopular()
    {
        // Arrange
        _mockGroupService.Setup(x => x.GetPopularityByGroupType(GroupType.SecretPolice)).Returns(90);
        _mockPopularityService.Setup(x => x.GetMonthlyMinimalPopularityAndStrength()).Returns(80);

        // Act
        bool result = _reportService.IsPlayerPopularWithSecretPolice();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void HasPoliceEnoughStrength_ReturnsTrueWhenEnoughStrength()
    {
        // Arrange
        _mockGroupService.Setup(x => x.GetStrengthByGroupType(GroupType.SecretPolice)).Returns(100);
        _mockPopularityService.Setup(x => x.GetMonthlyMinimalPopularityAndStrength()).Returns(80);

        // Act
        bool result = _reportService.HasPoliceEnoughStrength();

        // Assert
        Assert.IsTrue(result);
    }
}