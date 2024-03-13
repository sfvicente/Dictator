using Dictator.ConsoleInterface.Assassination;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Escape;
using Dictator.ConsoleInterface.Month;
using Dictator.ConsoleInterface.PresidentialDecision;
using Dictator.ConsoleInterface.Revolution;
using Dictator.ConsoleInterface.Start;
using Dictator.ConsoleInterface.Treasury;
using Dictator.ConsoleInterface.War;
using Dictator.Core;
using Dictator.Core.Models;
using System;
using System.Collections.Generic;

namespace Dictator.ConsoleInterface;

public interface IUserInterface
{
    void Initialise();
    void DisplayIntroScreen();
    void DisplayWelcomeScreen(int highscore);
    void DisplayTitleScreen();
    void DisplayAccountScreen(Account account);
    DialogResult DisplayPoliceReportRequestDialog(PoliceReportRequest policeReportRequest);
    void DisplayPoliceReportScreen(PoliceReport policeReport);
    void DisplayAudienceScreen(Audience audience);
    DialogResult DisplayAudienceDecisionDialog(Audience audience);
    DialogResult DisplayAdviceRequestDialog();
    void DisplayAdviceScreen(Audience audience);
    void DisplayAdviceScreen(Decision decision);
    void DisplayBankuptcyScreen();
    void DisplayNewsScreen(string headline);
    void DisplayMonthScreen(int month);
    DecisionType DisplayPresidentialDecisionMainDialog();
    int DisplayPresidentialDecisionSubDialog(Decision[] decisions);
    DialogResult DisplayPresidentialDecisionActionDialog(Decision decision);
    void DisplayLoanApplicationScreen();
    void DisplayLoanApplicationResultScreen(LoanApplicationResult loanApplicationResult);
    void DisplayTransferToSwissBankAccount(SwissBankAccountTransfer swissBankAccountTransfer, Account account);
    void DisplayAssassinationAttempt(string groupName);
    void DisplayAssassinationFailedScreen();
    void DisplayAssassinationSuccededScreen();
    void DisplayRevolutionScreen();
    void DisplayRevolutionStartedScreen();
    int DisplayRevolutionAskForHelpDialog(Revolutionary revolutionary, Dictionary<int, Group> possibleAllies);
    void DisplayRevolutionNoAlliesScreen();
    void DisplayRevolutionAllyLowPopularityScreen();
    DialogResult DisplayRevolutionCrushedDialog();
    void DisplayRevolutionOverthrownScreen();
    void DisplayWarThreatScreen();
    void DisplayLeftotoInvadesScreen(WarStats warStats);
    void DisplayWarLostScreen();
    void DisplayWarWonScreen();
    DialogResult DisplayEscapeAttemptDialog();
    void DisplayHelicopterEscapeScreen();
    void DisplayHelicopterWontStartScreen();
    void DisplayEscapeToLeftotoScreen();
    void DisplayGuerillasCelebratingScreen();
    void DisplayGuerillasMissedScreen();
    void DisplayHelicopterEngineFailure();
    void DisplayWarExecutionScreen();
    void DisplayEndScreen(Score score);
}

public class UserInterface : IUserInterface
{
    private readonly IIntroScreen _introScreen;
    private readonly IWelcomeScreen _welcomeScreen;
    private readonly ITitleScreen _titleScreen;
    private readonly ITreasuryReportScreen _treasuryReportScreen;
    private readonly IBankruptcyScreen _bankruptcyScreen;
    private readonly IPoliceReportRequestDialog _policeReportRequestScreen;
    private readonly IPoliceReportScreen _policeReportScreen;
    private readonly IAudienceScreen _audienceScreen;
    private readonly IAudienceDecisionDialog _audienceDecisionDialog;
    private readonly IAdviceRequestDialog _adviceRequestDialog;
    private readonly IAdviceScreen _adviceScreen;
    private readonly INewsflashScreen _newsflashScreen;
    private readonly IMonthScreen _monthScreen;
    private readonly IPresidentialDecisionMainDialog _presidentialDecisionMainDialog;
    private readonly IPresidentialDecisionSubDialog _presidentialDecisionSubDialog;
    private readonly IPresidentialDecisionActionDialog _presidentialDecisionActionDialog;
    private readonly ILoanApplicationScreen _loanApplicationScreen;
    private readonly ILoanApplicationResultScreen _loanApplicationResultScreen;
    private readonly ITransferToSwissBankAccountScreen _transferToSwissBankAccountScreen;
    private readonly IAssassinationScreen _assassinationScreen;
    private readonly IAssassinationSuccededScreen _assassinationSuccededScreen;
    private readonly IAssassinationFailedScreen _assassinationFailedScreen;
    private readonly IRevolutionScreen _revolutionScreen;
    private readonly IRevolutionStartedScreen _revolutionStartedScreen;
    private readonly IRevolutionAskForHelpDialog _revolutionAskForHelpDialog;
    private readonly IRevolutionNoAlliesScreen _revolutionNoAlliesScreen;
    private readonly IRevolutionAllyLowPopularityScreen _revolutionAllyLowPopularityScreen;
    private readonly IRevolutionCrushedDialog _revolutionCrushedDialog;
    private readonly IRevolutionOverthrownScreen _revolutionOverthrownScreen;
    private readonly IWarThreatScreen _warThreatScreen;
    private readonly IWarLeftotoInvadesScreen _leftotoInvadesScreen;
    private readonly IWarLostScreen _warLostScreen;
    private readonly IWarWonScreen _warWonScreen;
    private readonly IEscapeAttemptDialog _escapeAttemptDialog;
    private readonly IHelicopterEscapeScreen _helicopterEscapeScreen;
    private readonly IHelicopterWontStartScreen _helicopterWontStartScreen;
    private readonly IEscapeToLeftotoScreen _escapeToLeftotoScreen;
    private readonly IGuerillasCelebratingScreen _guerillasCelebratingScreen;
    private readonly IGuerillasMissedScreen _guerillasMissedScreen;
    private readonly IHelicopterEngineFailureScreen _helicopterEngineFailureScreen;
    private readonly IWarExecutionScreen _warExecutionScreen;
    private readonly IEndScreen _endScreen;

    public UserInterface(
        IIntroScreen introScreen,
        IWelcomeScreen welcomeScreen,
        ITitleScreen titleScreen,
        ITreasuryReportScreen treasuryReportScreen,
        IBankruptcyScreen bankruptcyScreen,
        IPoliceReportRequestDialog policeReportRequestScreen,
        IPoliceReportScreen policeReportScreen,
        IAudienceScreen audienceScreen,
        IAudienceDecisionDialog audienceDecisionDialog,
        IAdviceRequestDialog adviceRequestDialog,
        IAdviceScreen adviceScreen,
        INewsflashScreen newsflashScreen,
        IMonthScreen monthScreen,
        IPresidentialDecisionMainDialog presidentialDecisionMainDialog,
        IPresidentialDecisionSubDialog presidentialDecisionSubDialog,
        IPresidentialDecisionActionDialog presidentialDecisionActionDialog,
        ILoanApplicationScreen loanApplicationScreen,
        ILoanApplicationResultScreen loanApplicationResultScreen,
        ITransferToSwissBankAccountScreen transferToSwissBankAccountScreen,
        IAssassinationScreen assassinationScreen,
        IAssassinationSuccededScreen assassinationSuccededScreen,
        IAssassinationFailedScreen assassinationFailedScreen,
        IRevolutionScreen revolutionScreen,
        IRevolutionStartedScreen revolutionStartedScreen,
        IRevolutionAskForHelpDialog revolutionAskForHelpDialog,
        IRevolutionNoAlliesScreen revolutionNoAlliesScreen,
        IRevolutionAllyLowPopularityScreen revolutionAllyLowPopularityScreen,
        IRevolutionCrushedDialog revolutionCrushedDialog,
        IRevolutionOverthrownScreen revolutionOverthrownScreen,
        IWarThreatScreen warThreatScreen,
        IWarLeftotoInvadesScreen leftotoInvadesScreen,
        IWarLostScreen warLostScreen,
        IWarWonScreen warWonScreen,
        IEscapeAttemptDialog escapeAttemptDialog,
        IHelicopterEscapeScreen helicopterEscapeScreen,
        IHelicopterWontStartScreen helicopterWontStartScreen,
        IEscapeToLeftotoScreen escapeToLeftotoScreen,
        IGuerillasCelebratingScreen guerillasCelebratingScreen,
        IGuerillasMissedScreen guerillasMissedScreen,
        IHelicopterEngineFailureScreen helicopterEngineFailureScreen,
        IWarExecutionScreen warExecutionScreen,
        IEndScreen endScreen)
    {
        _introScreen = introScreen;
        _welcomeScreen = welcomeScreen;
        _titleScreen = titleScreen;
        _treasuryReportScreen = treasuryReportScreen;
        _policeReportRequestScreen = policeReportRequestScreen;
        _policeReportScreen = policeReportScreen;
        _audienceScreen = audienceScreen;
        _audienceDecisionDialog = audienceDecisionDialog;
        _adviceRequestDialog = adviceRequestDialog;
        _adviceScreen = adviceScreen;
        _bankruptcyScreen = bankruptcyScreen;
        _newsflashScreen = newsflashScreen;
        _monthScreen = monthScreen;
        _presidentialDecisionMainDialog = presidentialDecisionMainDialog;
        _presidentialDecisionSubDialog = presidentialDecisionSubDialog;
        _presidentialDecisionActionDialog = presidentialDecisionActionDialog;
        _loanApplicationScreen = loanApplicationScreen;
        _loanApplicationResultScreen = loanApplicationResultScreen;
        _transferToSwissBankAccountScreen = transferToSwissBankAccountScreen;
        _assassinationScreen = assassinationScreen;
        _assassinationSuccededScreen = assassinationSuccededScreen;
        _assassinationFailedScreen = assassinationFailedScreen;
        _revolutionScreen = revolutionScreen;
        _revolutionStartedScreen = revolutionStartedScreen;
        _revolutionAskForHelpDialog = revolutionAskForHelpDialog;
        _revolutionNoAlliesScreen = revolutionNoAlliesScreen;
        _revolutionAllyLowPopularityScreen = revolutionAllyLowPopularityScreen;
        _revolutionCrushedDialog = revolutionCrushedDialog;
        _revolutionOverthrownScreen = revolutionOverthrownScreen;
        _warThreatScreen = warThreatScreen;
        _leftotoInvadesScreen = leftotoInvadesScreen;
        _warLostScreen = warLostScreen;
        _warWonScreen = warWonScreen;
        _escapeAttemptDialog = escapeAttemptDialog;
        _helicopterEscapeScreen = helicopterEscapeScreen;
        _helicopterWontStartScreen = helicopterWontStartScreen;
        _escapeToLeftotoScreen = escapeToLeftotoScreen;
        _guerillasCelebratingScreen = guerillasCelebratingScreen;
        _guerillasMissedScreen = guerillasMissedScreen;
        _helicopterEngineFailureScreen = helicopterEngineFailureScreen;
        _warExecutionScreen = warExecutionScreen;
        _endScreen = endScreen;
    }

    public void Initialise()
    {
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.Clear();
    }

    public void DisplayIntroScreen() => _introScreen.Show();

    public void DisplayWelcomeScreen(int highscore) => _welcomeScreen.Show(highscore);

    public void DisplayTitleScreen() => _titleScreen.Show();

    public void DisplayAccountScreen(Account account) => _treasuryReportScreen.Show(account);

    public DialogResult DisplayPoliceReportRequestDialog(PoliceReportRequest policeReportRequest) => _policeReportRequestScreen.Show(policeReportRequest);

    public void DisplayPoliceReportScreen(PoliceReport policeReport) => _policeReportScreen.Show(policeReport);

    public void DisplayAudienceScreen(Audience audience) => _audienceScreen.Show(audience);

    public DialogResult DisplayAudienceDecisionDialog(Audience audience) => _audienceDecisionDialog.Show(audience);

    public DialogResult DisplayAdviceRequestDialog() => _adviceRequestDialog.Show();

    public void DisplayAdviceScreen(Audience audience) => _adviceScreen.Show(audience);

    public void DisplayAdviceScreen(Decision decision) => _adviceScreen.Show(decision);

    public void DisplayBankuptcyScreen() => _bankruptcyScreen.Show();

    public void DisplayNewsScreen(string headline) => _newsflashScreen.Show(headline);

    public void DisplayMonthScreen(int month) => _monthScreen.Show(month);

    public DecisionType DisplayPresidentialDecisionMainDialog() => _presidentialDecisionMainDialog.Show();

    public int DisplayPresidentialDecisionSubDialog(Decision[] decisions) => _presidentialDecisionSubDialog.Show(decisions);

    public DialogResult DisplayPresidentialDecisionActionDialog(Decision decision) => _presidentialDecisionActionDialog.Show(decision);

    public void DisplayLoanApplicationScreen() => _loanApplicationScreen.Show();

    public void DisplayLoanApplicationResultScreen(LoanApplicationResult loanApplicationResult) => _loanApplicationResultScreen.Show(loanApplicationResult);

    public void DisplayTransferToSwissBankAccount(SwissBankAccountTransfer swissBankAccountTransfer, Account account) => _transferToSwissBankAccountScreen.Show(swissBankAccountTransfer, account);

    public void DisplayAssassinationAttempt(string groupName) => _assassinationScreen.Show(groupName);

    public void DisplayAssassinationFailedScreen() => _assassinationFailedScreen.Show();

    public void DisplayAssassinationSuccededScreen() => _assassinationSuccededScreen.Show();

    public void DisplayRevolutionScreen() => _revolutionScreen.Show();

    public void DisplayRevolutionStartedScreen() => _revolutionStartedScreen.Show();

    public int DisplayRevolutionAskForHelpDialog(Revolutionary revolutionary, Dictionary<int, Group> possibleAllies) => _revolutionAskForHelpDialog.Show(revolutionary, possibleAllies);

    public void DisplayRevolutionAllyLowPopularityScreen() => _revolutionAllyLowPopularityScreen.Show();

    public void DisplayRevolutionNoAlliesScreen() => _revolutionNoAlliesScreen.Show();

    public DialogResult DisplayRevolutionCrushedDialog() => _revolutionCrushedDialog.Show();

    public void DisplayRevolutionOverthrownScreen() => _revolutionOverthrownScreen.Show();

    public void DisplayWarThreatScreen() => _warThreatScreen.Show();

    public void DisplayLeftotoInvadesScreen(WarStats warStats) => _leftotoInvadesScreen.Show(warStats);

    public void DisplayWarLostScreen() => _warLostScreen.Show();

    public void DisplayWarWonScreen() => _warWonScreen.Show();

    public DialogResult DisplayEscapeAttemptDialog() => _escapeAttemptDialog.Show();

    public void DisplayHelicopterEscapeScreen() => _helicopterEscapeScreen.Show();

    public void DisplayHelicopterWontStartScreen() => _helicopterWontStartScreen.Show();

    public void DisplayEscapeToLeftotoScreen() => _escapeToLeftotoScreen.Show();

    public void DisplayGuerillasMissedScreen() => _guerillasMissedScreen.Show();

    public void DisplayGuerillasCelebratingScreen() => _guerillasCelebratingScreen.Show();

    public void DisplayHelicopterEngineFailure() => _helicopterEngineFailureScreen.Show();

    public void DisplayWarExecutionScreen() => _warExecutionScreen.Show();

    public void DisplayEndScreen(Score score) => _endScreen.Show(score);
}
