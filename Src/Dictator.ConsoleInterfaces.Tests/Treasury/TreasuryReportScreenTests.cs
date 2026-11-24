using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Treasury;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.Treasury;

[TestFixture]
public class TreasuryReportScreenTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IAccountControl> _accountControlMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private TreasuryReportScreen _treasuryReportScreen;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _accountControlMock = new Mock<IAccountControl>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();

        _treasuryReportScreen = new TreasuryReportScreen(
            _consoleServiceMock.Object,
            _accountControlMock.Object,
            _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_DisplaysDollarBackgroundBannerAndAccountControl()
    {
        // Arrange
        var account = new Account
        {
            TreasuryBalance = 1000,
            MonthlyCosts = 60,
            HasSwissBankAccount = false
        };

        // Act
        _treasuryReportScreen.Show(account);

        // Assert
        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.DarkGreen), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.DarkGreen), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear('$'), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(9, 9, "TREASURY REPORT", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _accountControlMock.Verify(ac => ac.Show(account), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }
}
