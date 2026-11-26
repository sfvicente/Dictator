using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Escape;
using Dictator.ConsoleInterface.Police;
using Dictator.ConsoleInterface.War;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.War;

[TestFixture]
public class WarEscapeAndPoliceDialogsTests
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
    public void WarLeftotoInvadesScreen_Show_DisplaysRitimbanAndLeftotanStrengths()
    {
        var screen = new WarLeftotoInvadesScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
        var stats = new WarStats { RitimbanStrength = 18, LeftotanStrength = 12 };

        screen.Show(stats);

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.DarkRed), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(7, 8, " LEFTOTO  INVADES ", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "     Ritimban Strength is 18    ", ConsoleColor.DarkRed, ConsoleColor.Gray), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 14, "     Leftotan Strength is 12    ", ConsoleColor.DarkRed, ConsoleColor.Gray), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void WarThreatScreen_Show_DisplaysThreatOfWarMessage()
    {
        var screen = new WarThreatScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 6, "   THREAT of WAR with LEFTOTO   "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void EscapeAttemptDialog_Show_DisplaysPromptAndReturnsControlResult()
    {
        var dialog = new EscapeAttemptDialog(_consoleServiceMock.Object, _pressAnyKeyWithYesControlMock.Object);
        _pressAnyKeyWithYesControlMock.Setup(p => p.Show()).Returns(DialogResult.Yes);

        DialogResult result = dialog.Show();

        Assert.That(result, Is.EqualTo(DialogResult.Yes));
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "        ESCAPE ATTEMPT ?        "), Times.Once);
    }

    [Test]
    public void PoliceReportRequestDialog_WhenAvailable_DisplaysCostAndReturnsUserChoice()
    {
        var dialog = new PoliceReportRequestDialog(
            _consoleServiceMock.Object,
            _pressAnyKeyControlMock.Object,
            _pressAnyKeyWithYesControlMock.Object);
        var request = new PoliceReportRequest
        {
            HasEnoughBalance = true,
            IsPlayerPopularWithSecretPolice = true,
            HasPoliceEnoughStrength = true,
            PolicePopularity = 7,
            PoliceStrength = 6
        };
        _pressAnyKeyWithYesControlMock.Setup(p => p.Show()).Returns(DialogResult.Yes);

        DialogResult result = dialog.Show(request);

        Assert.That(result, Is.EqualTo(DialogResult.Yes));
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 13, "         ( costs $1000 )        "), Times.Once);
    }

    [Test]
    public void PoliceReportRequestDialog_WhenNotAvailable_DisplaysReasonsAndReturnsNo()
    {
        var dialog = new PoliceReportRequestDialog(
            _consoleServiceMock.Object,
            _pressAnyKeyControlMock.Object,
            _pressAnyKeyWithYesControlMock.Object);
        var request = new PoliceReportRequest
        {
            HasEnoughBalance = false,
            IsPlayerPopularWithSecretPolice = false,
            HasPoliceEnoughStrength = false,
            PolicePopularity = 1,
            PoliceStrength = 2
        };

        DialogResult result = dialog.Show(request);

        Assert.That(result, Is.EqualTo(DialogResult.No));
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 10, "          NOT AVAILABLE         "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "  Your POPULARITY with us is 1  "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 13, "      POLICE strength is 2      "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 14, "    You can't AFFORD a REPORT   "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }
}
