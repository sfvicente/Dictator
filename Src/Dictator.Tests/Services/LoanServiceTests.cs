using NUnit.Framework;
using Moq;
using NUnit.Framework.Internal;
using Dictator.Core.Services;
using Dictator.Core.Models;

namespace Dictator.Tests;

[TestFixture]
public class LoanServiceTests
{
    [Test]
    public void AskForLoan_TooEarly_ReturnsRefusalTooEarly()
    {
        // Arrange
        var randomMock = new Mock<IRandomService>();
        randomMock.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(0); // Simulate too early condition
        var governmentMock = new Mock<IGovernmentService>();
        governmentMock.Setup(g => g.GetMonth()).Returns(1); // Assume it's the first month
        var groupServiceMock = new Mock<IGroupService>();
        var accountServiceMock = new Mock<IAccountService>();

        var loanService = new LoanService(randomMock.Object, accountServiceMock.Object, groupServiceMock.Object, governmentMock.Object);

        // Act
        var result = loanService.AskForLoan(LenderCountry.America);

        // Assert
        Assert.IsFalse(result.IsAccepted);
        Assert.AreEqual(LoanApplicationRefusalType.TooEarly, result.RefusalType);
    }

    [Test]
    public void AskForLoan_PreviousLoanExists_ReturnsRefusalAlreadyUsed()
    {
        // Arrange
        var randomMock = new Mock<IRandomService>();
        var governmentMock = new Mock<IGovernmentService>();
        var groupServiceMock = new Mock<IGroupService>();
        var accountServiceMock = new Mock<IAccountService>();
        var loanService = new LoanService(randomMock.Object, accountServiceMock.Object, groupServiceMock.Object, governmentMock.Object);

        // Act
        var result = loanService.AskForLoan(LenderCountry.America); // Assuming a previous loan exists

        // Assert
        Assert.IsFalse(result.IsAccepted);
        Assert.AreEqual(LoanApplicationRefusalType.AlreadyUsed, result.RefusalType);
    }

    [Test]
    public void AskForLoan_GroupNotPopularEnough_ReturnsRefusalNotPopularEnough()
    {
        // Arrange
        var randomMock = new Mock<IRandomService>();
        randomMock.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(100); // Mock random result
        var governmentMock = new Mock<IGovernmentService>();
        governmentMock.Setup(g => g.GetMonthlyMinimalPopularityAndStrength()).Returns(100); // Mock government service
        var groupServiceMock = new Mock<IGroupService>();
        groupServiceMock
            .Setup(g => g.GetGroupTypeByCountry(It.IsAny<LenderCountry>()))
            .Returns(GroupType.Americans);
        groupServiceMock
            .Setup(g => g.GetGroupByType(It.IsAny<GroupType>()))
            .Returns(new Group(GroupType.Americans, 1, 1, string.Empty, string.Empty) { Popularity = 1 }); // Assuming group popularity is less than minimal required
        var accountServiceMock = new Mock<IAccountService>();
        var loanService = new LoanService(randomMock.Object, accountServiceMock.Object, groupServiceMock.Object, governmentMock.Object);

        // Act
        var result = loanService.AskForLoan(LenderCountry.America);

        // Assert
        Assert.IsFalse(result.IsAccepted);
        Assert.AreEqual(LoanApplicationRefusalType.NotPopularEnough, result.RefusalType);
    }
}
