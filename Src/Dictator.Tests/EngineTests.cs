using Dictator.Core.Configuration;
using Dictator.Core.Models;
using Dictator.Core.Services;

namespace Dictator.Core.Tests;

[TestFixture]
public class EngineTests
{
    private Mock<IGameState> _gameStateMock;
    private Mock<IStateManagementService> _stateManagementServiceMock;
    private Mock<IAccountService> _accountServiceMock;
    private Mock<IStatsService> _statsServiceMock;
    private Mock<IGovernmentService> _governmentServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IReportService> _reportServiceMock;
    private Mock<IPlotService> _plotServiceMock;
    private Mock<IDecisionService> _decisionServiceMock;
    private Mock<IAudienceService> _audienceServiceMock;
    private Mock<INewsService> _newsServiceMock;
    private Mock<IRevolutionService> _revolutionServiceMock;
    private Mock<IScoreService> _scoreServiceMock;
    private Mock<IEscapeService> _escapeServiceMock;
    private Mock<IAssassinationService> _assassinationServiceMock;
    private Mock<ILoanService> _loanServiceMock;
    private Mock<IWarService> _warServiceMock;
    private Engine _engine;

    [SetUp]
    public void SetUp()
    {
        _gameStateMock = new Mock<IGameState>();
        _stateManagementServiceMock = new Mock<IStateManagementService>();
        _accountServiceMock = new Mock<IAccountService>();
        _statsServiceMock = new Mock<IStatsService>();
        _governmentServiceMock = new Mock<IGovernmentService>();
        _groupServiceMock = new Mock<IGroupService>();
        _reportServiceMock = new Mock<IReportService>();
        _plotServiceMock = new Mock<IPlotService>();
        _decisionServiceMock = new Mock<IDecisionService>();
        _audienceServiceMock = new Mock<IAudienceService>();
        _newsServiceMock = new Mock<INewsService>();
        _revolutionServiceMock = new Mock<IRevolutionService>();
        _scoreServiceMock = new Mock<IScoreService>();
        _escapeServiceMock = new Mock<IEscapeService>();
        _assassinationServiceMock = new Mock<IAssassinationService>();
        _loanServiceMock = new Mock<ILoanService>();
        _warServiceMock = new Mock<IWarService>();

        _engine = new Engine(
            _gameStateMock.Object,
            _stateManagementServiceMock.Object,
            _accountServiceMock.Object,
            _statsServiceMock.Object,
            _governmentServiceMock.Object,
            _groupServiceMock.Object,
            _reportServiceMock.Object,
            _plotServiceMock.Object,
            _decisionServiceMock.Object,
            _audienceServiceMock.Object,
            _newsServiceMock.Object,
            _revolutionServiceMock.Object,
            _scoreServiceMock.Object,
            _escapeServiceMock.Object,
            _assassinationServiceMock.Object,
            _loanServiceMock.Object,
            _warServiceMock.Object);
    }

    [Test]
    public void Initialise_ResetsAllStateAndServicesAndLoadsCatalogs()
    {
        _engine.Initialise();

        _gameStateMock.Verify(g => g.Initialise(), Times.Once);
        _stateManagementServiceMock.Verify(s => s.Initialise(), Times.Once);
        _statsServiceMock.Verify(s => s.Initialise(), Times.Once);
        _governmentServiceMock.Verify(g => g.Initialise(), Times.Once);
        _groupServiceMock.Verify(g => g.Initialise(), Times.Once);
    }

    [Test]
    public void ShouldNewsHappen_WhenNewsServiceTriggersAndUnusedNewsExists_ReturnsTrue()
    {
        _newsServiceMock.Setup(n => n.ShouldNewsHappen()).Returns(true);
        _newsServiceMock.Setup(n => n.DoesUnusedNewsExist()).Returns(true);

        bool result = _engine.ShouldNewsHappen();

        Assert.IsTrue(result);
    }

    [Test]
    public void ShouldNewsHappen_WhenNewsServiceDoesNotTrigger_ReturnsFalse()
    {
        _newsServiceMock.Setup(n => n.ShouldNewsHappen()).Returns(false);

        bool result = _engine.ShouldNewsHappen();

        Assert.IsFalse(result);
        _newsServiceMock.Verify(n => n.DoesUnusedNewsExist(), Times.Never);
    }

    [Test]
    public void GetDecisionsByType_DelegatesToDecisionService()
    {
        _engine.GetDecisionsByType(DecisionType.ImproveYourChances);

        _decisionServiceMock.Verify(
            d => d.GetDecisionsByType(DecisionType.ImproveYourChances),
            Times.Once);
    }

    [Test]
    public void SelectRandomUnusedAudienceRequest_DelegatesToAudienceService()
    {
        _engine.SelectRandomUnusedAudienceRequest();

        _audienceServiceMock.Verify(
            a => a.SelectRandomUnusedAudienceRequest(),
            Times.Once);
    }
}
