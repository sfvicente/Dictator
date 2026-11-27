using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Revolution;
using Dictator.ConsoleInterface.Treasury;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.Revolution;

[TestFixture]
public class RevolutionAndTreasuryScreensTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private Mock<IPressAnyKeyWithYesControl> _pressAnyKeyWithYesControlMock;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _pressAnyKeyWithYesControlMock = new Mock<IPressAnyKeyWithYesControl>();
    }

    [Test]
    public void RevolutionScreen_Show_ClearsRedAndDisplaysRevolutionHeader()
    {
        var screen = new RevolutionScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Red), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(11, 12, "REVOLUTION", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void RevolutionNoAlliesScreen_Show_DisplaysOnYourOwnMessage()
    {
        var screen = new RevolutionNoAlliesScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "      You're ON YOUR OWN !      "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void RevolutionAllyLowPopularityScreen_Show_DisplaysYouMustBeJokingMessage()
    {
        var screen = new RevolutionAllyLowPopularityScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "      You must be JOKING !      "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void RevolutionOverthrownScreen_Show_DisplaysOverthrownAndLiquidatedMessage()
    {
        var screen = new RevolutionOverthrownScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Black, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 10, "    You have been OVERTHROWN    "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Write("LIQUIDATED", ConsoleColor.White), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void RevolutionCrushedDialog_Show_DisplaysCrushedAndPunishPromptAndReturnsChoice()
    {
        var dialog = new RevolutionCrushedDialog(_consoleServiceMock.Object, _pressAnyKeyWithYesControlMock.Object);
        _pressAnyKeyWithYesControlMock.Setup(p => p.Show()).Returns(DialogResult.Yes);

        DialogResult result = dialog.Show();

        Assert.That(result, Is.EqualTo(DialogResult.Yes));
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 10, "  The REVOLT has been CRUSHED   "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "\u008F\u008FPUNISH the REVOLUTIONARIES ?\u008F\u008F"), Times.Once);
    }

    [Test]
    public void BankruptcyScreen_Show_DisplaysTreasuryBankruptAndDropWarnings()
    {
        var screen = new BankruptcyScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(4, 6, "The TREASURY is "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(20, 6, "BANKRUPT"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 15, "and YOUR own STRENGTH will DROP"), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void AccountControl_Show_WhenTreasuryPositiveAndSwissAccountOpen_DisplaysHoldsAndSwissBalance()
    {
        var control = new AccountControl(_consoleServiceMock.Object);
        var account = new Account
        {
            TreasuryBalance = 500,
            MonthlyCosts = 60,
            HasSwissBankAccount = true,
            SwissBankAccountBalance = 250
        };

        control.Show(account);

        _consoleServiceMock.Verify(cs => cs.WriteAt(2, 13, " The TREASURY ", ConsoleColor.Blue, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Write("holds $500,000 ", ConsoleColor.Blue, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(3, 15, " MONTHLY COSTS are $60,000 ", ConsoleColor.Blue, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(3, 18, "[SWISS Acct holds $250,000]", ConsoleColor.Blue, ConsoleColor.White), Times.Once);
    }

    [Test]
    public void AccountControl_Show_WhenTreasuryNegative_DisplaysFlashingOwesAndAbsoluteBalance()
    {
        var control = new AccountControl(_consoleServiceMock.Object);
        var account = new Account
        {
            TreasuryBalance = -120,
            MonthlyCosts = 60,
            HasSwissBankAccount = false
        };

        control.Show(account);

        _consoleServiceMock.Verify(cs => cs.Write("OWES", ConsoleColor.Blue, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Write(" $120,000 ", ConsoleColor.Blue, ConsoleColor.White), Times.Once);
    }
}
