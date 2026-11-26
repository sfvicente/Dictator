using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.PresidentialDecision;
using Dictator.ConsoleInterface.Treasury;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.PresidentialDecision;

[TestFixture]
public class PresidentialDecisionScreensTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private Mock<IAccountControl> _accountControlMock;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _accountControlMock = new Mock<IAccountControl>();
    }

    [Test]
    public void LoanApplicationResultScreen_WhenAccepted_DisplaysApprovedAmount()
    {
        var screen = new LoanApplicationResultScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
        var result = new LoanApplicationResult
        {
            IsAccepted = true,
            GroupName = "The AMERICANS",
            Amount = 250
        };

        screen.Show(result);

        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "The AMERICANS will let you have"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(8, 14, "250,000 DOLLARS"), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [TestCase(LoanApplicationRefusalType.TooEarly, LenderCountry.America, 2, 12, "It's TOO EARLY to give AID")]
    [TestCase(LoanApplicationRefusalType.AlreadyUsed, LenderCountry.Russia, 2, 12, "Very sorry, NO MORE LOANS")]
    public void LoanApplicationResultScreen_WhenRefusedTooEarlyOrAlreadyUsed_DisplaysExpectedMessage(
        LoanApplicationRefusalType refusalType,
        LenderCountry country,
        int expectedCol,
        int expectedRow,
        string expectedText)
    {
        var screen = new LoanApplicationResultScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
        var result = new LoanApplicationResult
        {
            IsAccepted = false,
            RefusalType = refusalType,
            Country = country
        };

        screen.Show(result);

        _consoleServiceMock.Verify(cs => cs.WriteAt(expectedCol, expectedRow, expectedText), Times.Once);
    }

    [Test]
    public void LoanApplicationResultScreen_WhenNotPopularEnoughWithAmerica_DisplaysNuts()
    {
        var screen = new LoanApplicationResultScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
        var result = new LoanApplicationResult
        {
            IsAccepted = false,
            RefusalType = LoanApplicationRefusalType.NotPopularEnough,
            Country = LenderCountry.America
        };

        screen.Show(result);

        _consoleServiceMock.Verify(cs => cs.WriteAt(13, 13, "\"nuts !\"", ConsoleColor.DarkBlue, ConsoleColor.White), Times.Once);
    }

    [Test]
    public void LoanApplicationResultScreen_WhenNotPopularEnoughWithRussia_DisplaysNiet()
    {
        var screen = new LoanApplicationResultScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
        var result = new LoanApplicationResult
        {
            IsAccepted = false,
            RefusalType = LoanApplicationRefusalType.NotPopularEnough,
            Country = LenderCountry.Russia
        };

        screen.Show(result);

        _consoleServiceMock.Verify(cs => cs.WriteAt(13, 13, "NIET !", ConsoleColor.DarkRed, ConsoleColor.White), Times.Once);
    }

    [Test]
    public void TransferToSwissBankAccountScreen_WhenAmountStolenPositive_DisplaysTransferDetails()
    {
        var screen = new TransferToSwissBankAccountScreen(
            _consoleServiceMock.Object,
            _accountControlMock.Object,
            _pressAnyKeyControlMock.Object);
        var transfer = new SwissBankAccountTransfer
        {
            TreasuryPreviousBalance = 1000,
            AmountStolen = 500
        };
        var account = new Account { TreasuryBalance = 500, SwissBankAccountBalance = 500, HasSwissBankAccount = true };

        screen.Show(transfer, account);

        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 7, "The TREASURY held $1000,000"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 10, "$500,000 has been TRANSFERRED"), Times.Once);
        _accountControlMock.Verify(ac => ac.Show(account), Times.Once);
    }

    [Test]
    public void TransferToSwissBankAccountScreen_WhenAmountStolenZero_DisplaysNoTransferMade()
    {
        var screen = new TransferToSwissBankAccountScreen(
            _consoleServiceMock.Object,
            _accountControlMock.Object,
            _pressAnyKeyControlMock.Object);
        var transfer = new SwissBankAccountTransfer { TreasuryPreviousBalance = 0, AmountStolen = 0 };
        var account = new Account { TreasuryBalance = 0 };

        screen.Show(transfer, account);

        _consoleServiceMock.Verify(cs => cs.WriteAt(12, 7, "NO TRANSFER made"), Times.Once);
    }
}
