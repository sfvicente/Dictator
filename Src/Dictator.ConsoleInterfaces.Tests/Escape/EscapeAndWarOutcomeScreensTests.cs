using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Escape;
using Dictator.ConsoleInterface.War;

namespace Dictator.ConsoleInterfaces.Tests.Escape;

[TestFixture]
public class EscapeAndWarOutcomeScreensTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
    }

    [Test]
    public void HelicopterEscapeScreen_Show_DisplaysEscapeByHelicopterMessage()
    {
        var screen = new HelicopterEscapeScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "   You ESCAPE by HELICOPTER !   "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void HelicopterWontStartScreen_Show_DisplaysHelicopterWontStartMessage()
    {
        var screen = new HelicopterWontStartScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "  The HELICOPTER won't START !  "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void HelicopterEngineFailureScreen_Show_DisplaysEngineFailureAndExecutionMessages()
    {
        var screen = new HelicopterEngineFailureScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 9, "    Helicopter ENGINE FAILURE   "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 15, "       Summarily EXECUTED       "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void EscapeToLeftotoScreen_Show_DisplaysMountainsToLeftotoMessage()
    {
        var screen = new EscapeToLeftotoScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 10, "   You have to get through the  "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, "      MOUNTAINS to LEFTOTO      "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void GuerillasCelebratingScreen_Show_DisplaysGuerillasCelebratingMessage()
    {
        var screen = new GuerillasCelebratingScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Black, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, " The GUERILLAS are "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Write("CELEBRATING", ConsoleColor.White), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void GuerillasMissedScreen_Show_DisplaysGuerillasDidntCatchYouMessage()
    {
        var screen = new GuerillasMissedScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 12, " The GUERILLAS didn't catch you "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void WarWonScreen_Show_DisplaysLeftotansRoutedMessage()
    {
        var screen = new WarWonScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(8, 12, " LEFTOTANS ROUTED ", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void WarLostScreen_Show_DisplaysLeftotanVictoryMessage()
    {
        var screen = new WarLostScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Black, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(8, 8, " LEFTOTAN VICTORY "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void WarExecutionScreen_Show_DisplaysEnemyOfThePeopleAndExecutedMessages()
    {
        var screen = new WarExecutionScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        screen.Show();

        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, "     YOU are judged to be an    "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 13, "   ENEMY of the PEOPLE and ...  "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 15, "       Summarily EXECUTED       "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }
}
