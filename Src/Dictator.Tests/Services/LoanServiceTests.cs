﻿using Dictator.Core.Services;
using Dictator.Core.Models;

namespace Dictator.Tests;

[TestFixture]
public class LoanServiceTests
{
    private Mock<IRandomService> _randomMock;
    private Mock<IStateManagementService> _stateManagementServiceMock;
    private Mock<IStatsService> _statsServiceMock;
    private Mock<IGovernmentService> _governmentMock;
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IAccountService> _accountServiceMock;
    private LoanService _loanService;

    [SetUp]
    public void Setup()
    {
        _randomMock = new Mock<IRandomService>();
        _stateManagementServiceMock = new Mock<IStateManagementService>();
        _statsServiceMock = new Mock<IStatsService>();
        _governmentMock = new Mock<IGovernmentService>();
        _groupServiceMock = new Mock<IGroupService>();
        _accountServiceMock = new Mock<IAccountService>();

        _loanService = new LoanService(
            _randomMock.Object,
            _stateManagementServiceMock.Object,
            _accountServiceMock.Object,
            _groupServiceMock.Object,
            _statsServiceMock.Object,
            _governmentMock.Object);
    }

    [Test]
    public void AskForLoan_TooEarly_ReturnsRefusalTooEarly()
    {
        // Arrange
        _randomMock
            .Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(0); // Simulate too early condition
        _governmentMock
            .Setup(g => g.GetMonth())
            .Returns(1); // Assume it's the first month

        // Act
        var result = _loanService.AskForLoan(LenderCountry.America);

        // Assert
        Assert.IsFalse(result.IsAccepted);
        Assert.AreEqual(LoanApplicationRefusalType.TooEarly, result.RefusalType);
    }

    [Test]
    public void AskForLoan_WhenPreviousLoanExists_ReturnsRefusalAlreadyUsed()
    {
        // Arrange
        LenderCountry lenderCountry = LenderCountry.America;

        _randomMock
            .Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(1);
        _governmentMock
            .Setup(g => g.GetMonth())
            .Returns(10);
        _stateManagementServiceMock
            .Setup(s => s.HasLoadBeenGranted(lenderCountry))
            .Returns(true);

        // Act
        var result = _loanService.AskForLoan(lenderCountry);

        // Assert
        Assert.IsFalse(result.IsAccepted);
        Assert.AreEqual(LoanApplicationRefusalType.AlreadyUsed, result.RefusalType);
    }

    [Test]
    public void AskForLoan_WhenGroupNotPopularEnough_ReturnsRefusalNotPopularEnough()
    {
        // Arrange
        _randomMock
            .Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(1);
        _governmentMock
            .Setup(g => g.GetMonth())
            .Returns(10);
        _statsServiceMock
            .Setup(g => g.GetMonthlyMinimalPopularityAndStrength())
            .Returns(10);
        _groupServiceMock
            .Setup(g => g.GetGroupTypeByCountry(It.IsAny<LenderCountry>()))
            .Returns(GroupType.Americans);
        _groupServiceMock
            .Setup(g => g.GetGroupByType(It.IsAny<GroupType>()))
            .Returns(new Group(GroupType.Americans, 1, 1, string.Empty, string.Empty) { Popularity = 1 });

        // Act
        var result = _loanService.AskForLoan(LenderCountry.America);

        // Assert
        Assert.IsFalse(result.IsAccepted);
        Assert.AreEqual(LoanApplicationRefusalType.NotPopularEnough, result.RefusalType);
    }
}