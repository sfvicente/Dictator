using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Police;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.Police;

[TestFixture]
public class PoliceReportScreenTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private PoliceReportScreen _policeReportScreen;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _policeReportScreen = new PoliceReportScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_DisplaysHeadersGroupStatsAndRevolutionAllyNumber()
    {
        // Arrange
        var army = new Group(GroupType.Army, 5, 6, "The ARMY", "   ARMY   ")
        {
            Status = GroupStatus.Revolution
        };
        var guerillas = new Group(GroupType.Guerillas, 0, 6, "The GUERILLAS", "GUERILLAS ");
        army.Ally = guerillas;

        var peasants = new Group(GroupType.Peasants, 2, 4, "The PEASANTS", " PEASANTS ")
        {
            Status = GroupStatus.Assassination
        };

        var report = new PoliceReport
        {
            Month = 7,
            PlayerStrength = 4,
            MonthlyRevolutionStrength = 12,
            Groups = Array.AsReadOnly(new[] { army, peasants })
        };

        // Act
        _policeReportScreen.Show(report);

        // Assert
        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(9, 4, " POLICE  REPORT ", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Write("4", ConsoleColor.White, ConsoleColor.Black), Times.Once); // Guerillas ally ID (index 3 + 1 = 4)
        _consoleServiceMock.Verify(cs => cs.Write("A", ConsoleColor.White, ConsoleColor.Black), Times.Once); // Peasination status A
        _pressAnyKeyControlMock.Verify(p => p.Show(ConsoleColor.DarkBlue), Times.Once);
    }
}
