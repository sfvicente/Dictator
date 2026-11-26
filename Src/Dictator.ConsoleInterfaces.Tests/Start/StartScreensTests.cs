using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Start;

namespace Dictator.ConsoleInterfaces.Tests.Start;

[TestFixture]
public class StartScreensTests
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
    public void IntroScreen_Show_ClearsAndDisplaysCreditsAndPressAnyKey()
    {
        var introScreen = new IntroScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        introScreen.Show();

        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.DarkBlue), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(3, 5, "DICTATOR", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, "  Don PRIESTLEY                 ", ConsoleColor.Gray), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void WelcomeScreen_Show_WhenHighScoreIsZero_DisplaysFirstAttemptMessage()
    {
        var welcomeScreen = new WelcomeScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        welcomeScreen.Show(0);

        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.DarkBlue), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(8, 4, "WELCOME to OFFICE", ConsoleColor.DarkCyan, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, "rating of 0"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 14, "As this is your first attempt   "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void WelcomeScreen_Show_WhenHighScoreIsPositive_DisplaysBeatHighScoreChallenge()
    {
        var welcomeScreen = new WelcomeScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);

        welcomeScreen.Show(120);

        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, "rating of 120"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 14, "You can always try for 121 !"), Times.Once);
    }

    [Test]
    public void TitleScreen_Show_RendersDictatorBackgroundRitimbanFlagAndWaitsForKey()
    {
        var titleScreen = new TitleScreen(_consoleServiceMock.Object);

        titleScreen.Show();

        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 4, "Press any key to become DICTATOR", ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(13, 11, "RITIMBAN", ConsoleColor.DarkGreen, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(13, 17, "REPUBLIC", ConsoleColor.DarkGreen, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WaitForKeyPressed(), Times.Once);
    }
}
