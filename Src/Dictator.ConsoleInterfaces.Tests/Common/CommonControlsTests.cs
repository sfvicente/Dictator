using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterfaces.Tests.Common;

[TestFixture]
public class CommonControlsTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IKeyPanel> _keyPanelMock;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _keyPanelMock = new Mock<IKeyPanel>();
    }

    [Test]
    public void KeyPanel_Show_DrawsThreeRowsWithDefaultDarkRedBackground()
    {
        var keyPanel = new KeyPanel(_consoleServiceMock.Object);

        keyPanel.Show();

        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 22, "                                ", ConsoleColor.DarkRed, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 23, "              KEY               ", ConsoleColor.DarkRed, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 24, "                                ", ConsoleColor.DarkRed, ConsoleColor.White), Times.Once);
    }

    [Test]
    public void KeyPanel_ShowWithCustomBackground_DrawsThreeRowsWithSpecifiedBackground()
    {
        var keyPanel = new KeyPanel(_consoleServiceMock.Object);

        keyPanel.Show(ConsoleColor.DarkBlue);

        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 23, "              KEY               ", ConsoleColor.DarkBlue, ConsoleColor.White), Times.Once);
    }

    [Test]
    public void PressAnyKeyControl_Show_DisplaysKeyPanelAndWaitsForKey()
    {
        var control = new PressAnyKeyControl(_consoleServiceMock.Object, _keyPanelMock.Object);

        control.Show();

        _keyPanelMock.Verify(kp => kp.Show(), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WaitForKeyPressed(), Times.Once);
    }

    [Test]
    public void PressAnyKeyOrOptionControl_Show_DisplaysKeyPanelAndReturnsPressedKey()
    {
        var control = new PressAnyKeyOrOptionControl(_consoleServiceMock.Object, _keyPanelMock.Object);
        _consoleServiceMock.Setup(cs => cs.ReadKey()).Returns(ConsoleKey.D3);

        ConsoleKey key = control.Show();

        Assert.That(key, Is.EqualTo(ConsoleKey.D3));
        _keyPanelMock.Verify(kp => kp.Show(), Times.Once);
    }

    [TestCase(ConsoleKey.Y, DialogResult.Yes)]
    [TestCase(ConsoleKey.N, DialogResult.No)]
    [TestCase(ConsoleKey.Spacebar, DialogResult.No)]
    public void PressAnyKeyWithYesControl_Show_DisplaysYesBannerAndMapsKeyToDialogResult(ConsoleKey key, DialogResult expectedResult)
    {
        var control = new PressAnyKeyWithYesControl(_consoleServiceMock.Object, _keyPanelMock.Object);
        _consoleServiceMock.Setup(cs => cs.ReadKey()).Returns(key);

        DialogResult result = control.Show();

        Assert.That(result, Is.EqualTo(expectedResult));
        _consoleServiceMock.Verify(cs => cs.WriteAt(11, 21, " \"Y\"= YES ", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _keyPanelMock.Verify(kp => kp.Show(), Times.Once);
    }
}
