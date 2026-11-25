using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.PresidentialDecision;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.PresidentialDecision;

[TestFixture]
public class PresidentialDecisionDialogTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyOrOptionControl> _pressAnyKeyOrOptionControlMock;
    private PresidentialDecisionMainDialog _mainDialog;
    private PresidentialDecisionSubDialog _subDialog;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyOrOptionControlMock = new Mock<IPressAnyKeyOrOptionControl>();

        _mainDialog = new PresidentialDecisionMainDialog(_consoleServiceMock.Object, _pressAnyKeyOrOptionControlMock.Object);
        _subDialog = new PresidentialDecisionSubDialog(_consoleServiceMock.Object, _pressAnyKeyOrOptionControlMock.Object);
    }

    [TestCase(ConsoleKey.D1, DecisionType.PleaseAGroup)]
    [TestCase(ConsoleKey.D2, DecisionType.PleaseAllGroups)]
    [TestCase(ConsoleKey.D3, DecisionType.ImproveYourChances)]
    [TestCase(ConsoleKey.D4, DecisionType.RaiseSomeCash)]
    [TestCase(ConsoleKey.D5, DecisionType.StrengthenAGroup)]
    [TestCase(ConsoleKey.Spacebar, DecisionType.None)]
    public void MainDialog_Show_MapsPressedKeyToExpectedDecisionType(ConsoleKey key, DecisionType expectedType)
    {
        _pressAnyKeyOrOptionControlMock.Setup(p => p.Show()).Returns(key);

        DecisionType result = _mainDialog.Show();

        Assert.That(result, Is.EqualTo(expectedType));
    }

    [Test]
    public void SubDialog_Show_WhenAllDecisionsUsed_DisplaysAllSectionUsedUpMessage()
    {
        var decisions = new[]
        {
            new Dictator.Core.Models.Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, 0, -8, "NPPMMMMM", "LMMLMM", "DECISION 1") { HasBeenUsed = true }
        };
        _pressAnyKeyOrOptionControlMock.Setup(p => p.Show()).Returns(ConsoleKey.Spacebar);

        int option = _subDialog.Show(decisions);

        Assert.That(option, Is.EqualTo(99));
        _consoleServiceMock.Verify(cs => cs.PrintAt(12, 3, "ALL of this section USED UP", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
    }

    [Test]
    public void SubDialog_Show_WithUnusedDecision_DisplaysOptionAndReturnsSelectedIndex()
    {
        var decisions = new[]
        {
            new Dictator.Core.Models.Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, 0, -8, "NPPMMMMM", "LMMLMM", "DECREASE GENERAL TAXATION LEVEL ")
        };
        _pressAnyKeyOrOptionControlMock.Setup(p => p.Show()).Returns(ConsoleKey.D1);

        int option = _subDialog.Show(decisions);

        Assert.That(option, Is.EqualTo(1));
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, It.IsAny<int>(), "1.", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, It.IsAny<int>(), "DECREASE GENERAL TAXATION LEVEL ", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
    }

    [Test]
    public void ActionDialog_ShowInsufficientFunds_DisplaysMissingTreasuryCashAndLoanHints()
    {
        var pressAnyKeyMock = new Mock<IPressAnyKeyControl>();
        var pressAnyKeyWithYesMock = new Mock<IPressAnyKeyWithYesControl>();
        var actionDialog = new PresidentialDecisionActionDialog(
            _consoleServiceMock.Object,
            pressAnyKeyMock.Object,
            pressAnyKeyWithYesMock.Object);

        var decision = new Dictator.Core.Models.Decision(
            DecisionType.ImproveYourChances,
            DecisionSubType.PurchaseHelicopter,
            -12,
            0,
            "IIJMMKMM",
            "MMMMMM",
            "BUY an ESCAPE HELICOPTER        ");

        actionDialog.ShowInsufficientFunds(decision, isRussianLoanAvailable: true, isAmericanLoanAvailable: true);

        _consoleServiceMock.Verify(cs => cs.WriteAt(3, 9, "The CASH for this decision is", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(5, 11, " NOT in the TREASURY", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 14, " Perhaps the RUSSIANS can help ?", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 16, "THE AMERICANS are generous folk ", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        pressAnyKeyMock.Verify(p => p.Show(), Times.Once);
    }
}
