using Dictator.Core.Models;
using Dictator.Core.Services;
using System.Collections.Generic;

namespace Dictator.Core
{
    public class Engine : IEngine
    {
        private readonly IGameState _gameState;
        private readonly IAccountService accountService;
        private readonly IGovernmentService governmentService;
        private readonly IGroupService groupService;
        private readonly IReportService reportService;
        private readonly IPlotService plotService;
        private readonly IDecisionService decisionService;
        private readonly IAudienceService audienceService;
        private readonly INewsService newsService;
        private readonly IRevolutionService revolutionService;
        private readonly IScoreService scoreService;
        private readonly IEscapeService escapeService;
        private readonly IAssassinationService assassinationService;
        private readonly ILoanService loanService;
        private readonly IWarService warService;

        private News[] _news;
        private Decision[] _decisions;
        private Audience[] _audiences;

        public GroupType _assassinGroupType;

        public Engine(
            IGameState gameState,
            IAccountService accountService,
            IGovernmentService governmentService,
            IGroupService groupService,
            IReportService reportService,
            IPlotService plotService,
            IDecisionService decisionService,
            IAudienceService audienceService,
            INewsService newsService,
            IRevolutionService revolutionService,
            IScoreService scoreService,
            IEscapeService escapeService,
            IAssassinationService assassinationService,
            ILoanService loanService,
            IWarService warService)
        {
            _gameState = gameState;
            this.accountService = accountService;
            this.governmentService = governmentService;
            this.groupService = groupService;
            this.reportService = reportService;
            this.plotService = plotService;
            this.decisionService = decisionService;
            this.audienceService = audienceService;
            this.newsService = newsService;
            this.revolutionService = revolutionService;
            this.scoreService = scoreService;
            this.escapeService = escapeService;
            this.assassinationService = assassinationService;
            this.loanService = loanService;
            this.warService = warService;
        }

        /// <summary>
        ///     Initializes the game engine by initializing all of the required service components.
        /// </summary>
        public void Initialise()
        {
            governmentService.Initialise();
            groupService.Initialise();
            LoadAudienceData();
            LoadDecisionsData();
            LoadNewsData();
        }

        public void LoadAudienceData()
        {
            _audiences = [
                new Audience(GroupType.Army, 0, -5/*H*/, "QJLMMMMM", "PKLMMM", "     INTRODUCE CONSCRIPTION     "),
                new Audience(GroupType.Army, 0, 0, "PMJMMMMM", "NMLMMM", " REQUISITION LAND for TRAINING  "),
                new Audience(GroupType.Army, -10/*C*/, 0, "PLNMLMLM", "NMNIMM", "   ATTACK ALL GUERILLA BASES    "),
                new Audience(GroupType.Army, -8/*E*/, 0, "PLMMIMLM", "NMNKMM", "ATTACK GUERILLA BASES in LEFTOTO"),
                new Audience(GroupType.Army, 0, 0, "QONMMIMM", "NMNMMJ", "  SACK the SECRET POLICE CHIEF  "),
                new Audience(GroupType.Army, 0, 0, "PMMMLMIO", "MMMMMM", "EXPEL RUSSIAN MILITARY ADVISORS "),
                new Audience(GroupType.Army, 0, -9/*D*/, "QMLMMMMM", "OLLLMM", " INCREASE the PAY of the TROOPS "),
                new Audience(GroupType.Army, -12/*A*/, 0, "QLLMLLMM", "PLLKLM", "  BUY MORE ARMS and AMMUNITION  "),
                new Audience(GroupType.Peasants, 0, 3/*P*/, "LONMMMMM", "LMMLMM", "   STOP ARMY SIGN-UP COERCION   "),
                new Audience(GroupType.Peasants, 0, 0, "MQIMNMMM", "MOLMMM", "INCREASE the BASIC MINIMUM WAGE "),
                new Audience(GroupType.Peasants, 0, 0, "NQOMMIMM", "NNNNMJ", " CUT the POWERS of the S.POLICE "),
                new Audience(GroupType.Peasants, 0, 0, "MPKMKMMM", "MOKMMM", "STOP LEFTOTAN IMMIGRANT WORKERS "),
                new Audience(GroupType.Peasants, -10/*C*/, -8/*E*/, "LQKMOLNM", "MNLLMM", "INTRODUCE FREE EDUCATION for ALL"),
                new Audience(GroupType.Peasants, 0, 0, "MQJMNLNM", "MPJMML", "LEGALISE the FORMATION of UNIONS"),
                new Audience(GroupType.Peasants, 0, 0, "LQKMNLMM", "MOLLMM", "  FREE their IMPRISONED LEADER  "),
                new Audience(GroupType.Peasants, 0, 6/*S*/, "MPLMMMMM", "MMMLMM", "     START a PUBLIC LOTTERY     "),
                new Audience(GroupType.Landowners, 0, 0, "KMPMMMMM", "LMMMMM", "STOP MILITARY USE of THEIR LAND "),
                new Audience(GroupType.Landowners, 0, 0, "MIQMLMLM", "MKONMM", "  LOWER the BASIC MINIMUM WAGE  "),
                new Audience(GroupType.Landowners, 10/*W*/, -5/*H*/, "MMPMNMOI", "MMNMMM", "NATIONALISE AMERICAN BUSINESSES "),
                new Audience(GroupType.Landowners, 0, 5/*R*/, "MMPMJMLM", "MNOMLM", "LEVY DUTY on ALL LEFTOTO IMPORTS"),
                new Audience(GroupType.Landowners, 0, 4/*Q*/, "NNPMMIMM", "NMNNMK", " CUT SPENDING on the S. POLICE  "),
                new Audience(GroupType.Landowners, 0, -5/*H*/, "MMQMMMMM", "MMOMMM", "  DECREASE HEAVY LAND TAXATION  "),
                new Audience(GroupType.Landowners, 0, 0, "KLPMMMMM", "LLNNMM", "RELEASE TROOPS to WORK the LAND "),
                new Audience(GroupType.Landowners, -12/*A*/, -10/*C*/, "NNPMJMON", "MMPMKM", "BUILD a LARGE IRRIGATION SYSTEM "),
            ];
        }

        public void LoadDecisionsData()
        {
            _decisions = [
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "QLLMMLMM", "NMMLML", "MAKE ARMY CHIEF \"VICE-PRESIDENT\""),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, -1/*L*/, -4/*I*/, "LQNMOMNM", "MMMLMM", "SET UP FREE CLINICS for WORKERS "),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "LKQMMLLM", "LLOMML", "GIVE LANDOWNERS REGIONAL POWERS "),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 5/*R*/, 0, "KMMMQMKN", "LMMLPM", "SELL AMERICAN ARMS to LEFTOTO   "),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 12/*Y*/, 0, "MMLMLMKP", "MMMMMM", "SELL MINING RIGHTS to U.S. FIRMS"),
                new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 10/*W*/, "KMMMMMPJ", "MMMMMM", "RENT the RUSSIANS a NAVAL BASE  "),

                new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, 0, -8/*E*/, "NPPMMMMM", "LMMLMM", "DECREASE GENERAL TAXATION LEVEL "),
                new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, -8/*E*/, 0, "PPPMMMMM", "MMMLMM", "STAGE a BIG POPULARITY CAMPAIGN "),
                new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, 0, 8/*U*/, "PPPMMDMM", "ONNNMD", "CUT S.POLICE POWERS COMPLETELY  "),

                new Decision(DecisionType.ImproveYourChanges, DecisionSubType.None, 0, -6/*G*/, "JJJMMUMM", "LLLLMU", "INCREASE S.POLICE POWERS a LOT  "),
                new Decision(DecisionType.ImproveYourChanges, DecisionSubType.IncreaseBodyGuard, -4/*I*/, 0, "KLLMMLMM", "KMMMML", "INCREASE YOUR BODYGUARD        *"),
                new Decision(DecisionType.ImproveYourChanges, DecisionSubType.PurchaseHelicopter, -12/*A*/, 0, "IIJMMKMM", "MMMMMM", "BUY an ESCAPE HELICOPTER        "),
                new Decision(DecisionType.ImproveYourChanges, DecisionSubType.TransferToSwissAccount, 0, 0, "MMMMMMMM", "MMMMMM", "SEE TO YOUR SWISS BANK ACCOUNT *"),

                new Decision(DecisionType.RaiseSomeCash, DecisionSubType.AskForRussianLoan, 0, 0, "MMMMMMMM", "MMMMMM", "ASK the RUSSIANS for a \"LOAN\"  *"),
                new Decision(DecisionType.RaiseSomeCash, DecisionSubType.AskForAmericanLoan, 0, 0, "MMMMMMMM", "MMMMMM", "ASK AMERICANS for FOREIGN \"AID\"*"),
                new Decision(DecisionType.RaiseSomeCash, DecisionSubType.None, 12/*Z*/, 0, "NNPMGMKM", "MMMMMM", "NATIONALISE LEFTOTAN BUSINESSES "),

                new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None, -5/*H*/, 0, "PMMMJMLM", "RMMKKL", "BUY HEAVY ARTILLERY for THE ARMY"),
                new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None, 0, 0, "MPLMMLMM", "MRLPML", "ALLOW PEASANTS FREE MOVEMENT    "),
                new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None, 0, 0, "LLPMMLMM", "LLRLML", "ALLOW LANDOWNERS PRIVATE MILITIA")
            ];
        }

        public void LoadNewsData()
        {
            _news = [
                new News(0, 0, "MMMMMIMM", "MMMQMI", " PRESIDENT LOSES S.POLICE FILES "),
                new News(0, 0, "MMMMMMMM", "LMMVMM", " CUBANS ARM and TRAIN GUERILLAS "),
                new News(0, 0, "MMMMMMMM", "IMMOMN", "ACCIDENT. ARMY BARRACKS BLOWS UP"),
                new News(0, 0, "MMMMMMMM", "MMJMKM", "   BANANA PRICES FALL by 98%    "),
                new News(0, 0, "MMMMMMMM", "MMOMIM", "  MAJOR EARTHQUAKE in LEFTOTO   "),
                new News(0, 0, "MMMMMMMM", "MILKMM", "A PLAGUE SWEEPS through PEASANTS"),
            ];
        }

        /// <summary>
        ///     Retrieves the current account aggregated properties.
        /// </summary>
        /// <returns>The account information including Swiss bank account information, treasury balance
        /// and monthly costs.</returns>
        public Account GetAccount()
        {
            return _gameState.GetAccount();
        }

        /// <summary>
        ///     Sets the level of strength for a specific group.
        /// </summary>
        /// <param name="groupType">The type of the group to set the strength.</param>
        /// <param name="strength">The strength level which will be set.</param>
        public void SetGroupStrength(GroupType groupType, int strength)
        {
            groupService.SetStrength(groupType, strength);
        }

        /// <summary>
        ///     Retrieves a request for a detailed account or statement from police on the state of the groups and government.
        /// </summary>
        /// <returns>A police report request with information on the popularity and strength requirements.</returns>
        public PoliceReportRequest RequestPoliceReport()
        {
            return reportService.RequestPoliceReport();
        }

        /// <summary>
        ///     Retrieves the police report with the current month, revolution strength, player strength and group information.
        /// </summary>
        /// <returns>The police report with the current month, revolution strength, player strength and group information.</returns>
        public PoliceReport GetPoliceReport()
        {
            return reportService.GetPoliceReport();
        }

        /// <summary>
        ///     Advances the month number of the current game.
        /// </summary>
        public void AdvanceMonth()
        {
            governmentService.AdvanceMonth();
        }

        /// <summary>
        ///     Takes the required funds from treasury to pay for the monthly expenses.
        /// </summary>
        public void PayMonthlyCosts()
        {
            accountService.PayMonthlyCosts();
        }

        /// <summary>
        ///     Retrieves the month number of the current game.
        /// </summary>
        /// <returns>The current month number.</returns>
        public int GetMonth()
        {
            return governmentService.GetMonth();
        }

        /// <summary>
        ///     Determines if the government is bankrupt, which means that the treasury contains no funds.
        /// </summary>
        /// <returns><c>true</c> if government is bankrupt; otherwise, <c>false</c>.</returns>
        public bool IsGovernmentBankrupt()
        {
            return accountService.IsGovernmentBankrupt();
        }

        /// <summary>
        ///     Removes the specified amount of funds from the treasury.
        /// </summary>
        /// <param name="amount">The amount to spend from the treasury.</param>
        public void PayFromTreasury(int amount)
        {
            accountService.PayFromTreasury(amount);
        }

        /// <summary>
        ///     Steals half of the current treasury funds to the players private Swiss bank account.
        /// </summary>
        /// <returns>The details of the bank transfer consisting of the previous treasury balance and the amount stolen.</returns>
        public SwissBankAccountTransfer TransferToSwissBankAccount()
        {
            return accountService.TransferToSwissBankAccount();
        }

        /// <summary>
        ///     Sets the minimal monthly level requirement for popularity and strength which is used for a diverse
        ///     number of game logic when interacting with groups, such as requesting external financial aid or 
        ///     finding allies in a revolution.
        /// </summary>
        public void SetMonthlyMinimalPopularityAndStrength()
        {
            governmentService.SetMonthlyMinimalPopularityAndStrength();
        }

        /// <summary>
        ///     Sets the level of strength of a possible revolution for the current turn.
        /// </summary>
        public void SetMonthlyRevolutionStrength()
        {
            governmentService.SetMonthlyRevolutionStrength();
        }

        public void Plot()
        {
            plotService.Plot();
        }

        public bool ShouldNewsHappen()
        {
            if (newsService.ShouldNewsHappen())
            {
                if (newsService.DoesUnusedNewsExist(_news))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Selects a random news event that hasn't been used in the current game.
        /// </summary>
        /// <returns>A random unused news event.</returns>
        public News SelectRandomUnusedNews()
        {
            return newsService.SelectRandomUnusedNews(_news);
        }

        /// <summary>
        ///     Applies the effects of a specific news event on the groups and treasury-
        /// </summary>
        /// <param name="news">The news whose effect will be applied.</param>
        public void ApplyNewsEffects(News news)
        {
            newsService.ApplyNewsEffects(news);
        }

        /// <summary>
        ///     Retrieves an unused audience request from the list of audience requests and
        ///     marks it as used.
        /// </summary>
        /// <returns>An unused audience request.</returns>
        public Audience SelectRandomUnusedAudienceRequest()
        {
            return audienceService.SelectRandomUnusedAudienceRequest();
        }

        /// <summary>
        ///     Accepts an audience request with the associated modifications to group popularity and strength and also the changes to the treasury.
        /// </summary>
        /// <param name="audience">The audience to be accepted.</param>
        public void AcceptAudienceRequest(Audience audience)
        {
            audienceService.AcceptAudienceRequest(audience);
        }

        /// <summary>
        ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
        /// </summary>
        public void RefuseAudienceRequest(Audience audience)
        {
            audienceService.RefuseAudienceRequest(audience);
        }

        /// <summary>
        ///     Determines if a presidential option exists and is available for selection.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <param name="optionNumber">The option number within the type group of the decision.</param>
        /// <returns><c>true</c> if the presidential decision exists and is available for selection; otherwise, <c>false</c>.</returns>
        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber)
        {
            return decisionService.DoesPresidentialOptionExistAndIsAvailable(_decisions, decisionType, optionNumber);
        }

        /// <summary>
        ///     Retrieves all the decisions of a specific type.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <returns>An array of decisions of the specified type.</returns>
        public Decision[] GetDecisionsByType(DecisionType decisionType)
        {
            return decisionService.GetDecisionsByType(_decisions, decisionType);
        }

        /// <summary>
        ///     Retrieves a decisions with the specified type and index.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <param name="optionSelected">The position of the decision within the group of decisions with the specified type.</param>
        /// <returns>The position that matches the specified type and at the specific position.</returns>
        public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionNumber)
        {
            return decisionService.GetDecisionByTypeAndIndex(_decisions, decisionType, optionNumber);
        }

        /// <summary>
        ///     Increases bodyguard, resulting in an increment of the player's strength by 2.
        /// </summary>
        public void IncreaseBodyguard()
        {
            governmentService.IncreaseBodyguard();
        }

        /// <summary>
        ///     Asks for foreign aid in the form of a loan to either America or Russia.
        /// </summary>
        /// <param name="country">The country to which the loan request will be made.</param>
        /// <returns>The loan application result that includes if the loan has been approved or refused.</returns>
        public LoanApplicationResult AskForLoan(LenderCountry country)
        {
            return loanService.AskForLoan(country);
        }

        /// <summary>
        ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
        /// </summary>
        /// <param name="decision">The decision whose effects will be apply.</param>
        public void ApplyDecisionEffects(Decision decision)
        {
            decisionService.ApplyDecisionEffects(decision);
        }

        /// <summary>
        ///     Marks a presidential decision option as used.
        /// </summary>
        /// <param name="text">The text of the presidential decision which will be marked as used.</param>
        public void MarkDecisionAsUsed(string text)
        {
            decisionService.MarkDecisionAsUsed(_decisions, text);
        }

        /// <summary>
        ///     Determines if an assassination attempt on the player should happen by one of the following groups: army, 
        ///     peasants, landowners and guerrilas.
        /// </summary>
        /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldAssassinationAttemptHappen()
        {
            return groupService.ShouldAssassinationAttemptHappen();
        }

        /// <summary>
        ///     Retrieves the name of the group responsible for the assassination attempt.
        /// </summary>
        /// <returns>The name of the assassination group.</returns>
        public string GetAssassinationGroupName()
        {
            return assassinationService.GetAssassinationGroupName(_assassinGroupType);
        }

        /// <summary>
        ///     Determines if there is conflict between the republic of Ritimba and Leftoto. A conflict does not exist if the player is popular
        ///     with the leftotans or leftotans are weak.
        /// </summary>
        /// <returns><c>true</c> if a conflict exist with Leftoto; otherwise, <c>false</c>.</returns>
        public bool DoesConflictExist()
        {
            return warService.DoesConflictExist();
        }

        /// <summary>
        ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
        /// </summary>
        /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldWarHappen()
        {
            return warService.ShouldWarHappen();
        }

        /// <summary>
        ///     Applies the effects of a threat of war with leftoto, which results in an increase of the player's popularity amongst the army,
        ///     peasants, landowners and secret police.
        /// </summary>
        public void ApplyThreatOfWarEffects()
        {
            warService.ApplyThreatOfWarEffects();
        }

        /// <summary>
        ///     Determines if an assassination attempt on the player is successful.
        /// </summary>
        /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
        public bool IsAssassinationSuccessful()
        {
            return assassinationService.IsAssassinationSuccessful();
        }

        /// <summary>
        ///     Initiates an invasion of the Ritimba Republic by Leftoto, which calculates teh strength of both countries in a
        ///     scenario of war.
        /// </summary>
        /// <returns>The war statistics which are composed of the strength of each country.</returns>
        public WarStats BeginInvasion()
        {
            return warService.BeginInvasion();
        }

        /// <summary>
        ///     Executes and calculates the outcome for the war scenario between the Ritimba and Leftoto.
        /// </summary>
        /// <param name="warStats">The stats required for the war to calculate who wins.</param>
        /// <returns><c>true</c> if Ritimba wins the war; otherwise, <c>false</c>.</returns>
        public bool ExecuteWar(WarStats warStats)
        {
            return warService.ExecuteWar(warStats);
        }

        /// <summary>
        ///     Attempts to assign a revolt group in a scenario of revolution.
        /// </summary>
        /// <returns><c>true</c> if one of the groups becomes a group responsible for initiating a revolution; otherwise, <c>false</c>.</returns>
        public bool TryTriggerRevoltGroup()
        {
            return revolutionService.TryTriggerRevoltGroup();
        }

        /// <summary>
        ///     Gets the revolutionary group and ally that initiated the revolution.
        /// </summary>
        /// <returns>The revolutionary group, their ally and combined strength.</returns>
        public Revolutionary GetRevolutionary()
        {
            return revolutionService.GetRevolutionary();
        }

        /// <summary>
        ///     Finds the groups that can be possible allies of a player in a revolution.
        /// </summary>
        /// <returns>A dictionary containing the groups that can be possible allies with their respective ids.</returns>
        public Dictionary<int, Group> FindPossibleAlliesForPlayer()
        {
            return revolutionService.FindPossibleAllies();
        }

        /// <summary>
        ///     Determines if a group accepts to be an ally of the player during a revolution.
        /// </summary>
        /// <param name="groupId">The id of the group to accept or refuse an alliance.</param>
        /// <returns><c>true</c> if the group accepts to be an ally; otherwise, <c>false</c>.</returns>
        public bool DoesGroupAcceptAllianceInRevolution(int groupId)
        {
            return revolutionService.DoesGroupAcceptAllianceInRevolution(groupId);
        }

        /// <summary>
        ///     Sets the specified group as the ally of the player in a scenario of a revolution.
        /// </summary>
        /// <param name="selectedAllyGroupId">The id of the group to set as the ally of the player.</param>
        public void SetPlayerAllyForRevolution(int selectedAllyGroupId)
        {
            revolutionService.SetPlayerAllyForRevolution(selectedAllyGroupId);
        }

        /// <summary>
        ///     Determines if a revolution has succeeded in overthrowing the player.
        /// </summary>
        /// <returns><c>true</c> if the revolution has succeeded; otherwise, <c>false</c>.</returns>
        public bool DoesRevolutionSucceed()
        {
            return revolutionService.DoesRevolutionSucceed();
        }

        /// <summary>
        ///     Punishes the groups that have taken part in a revolution that has been offset by the player. It
        ///     sets the revolutionaries strength and popularity to zero.
        /// </summary>
        public void PunishRevolutionaries()
        {
            revolutionService.PunishRevolutionaries();
        }

        /// <summary>
        ///     Applies the effects that result of a player crushing a revolution. It boosts ally popularity, prevents revolutions
        ///     for the next two months and resets all group status and allies from previous plots.
        /// </summary>
        public void ApplyRevolutionCrushedEffects()
        {
            revolutionService.ApplyRevolutionCrushedEffects();
        }

        /// <summary>
        ///     Purchases an helicopter for a possible escape in revolution or war scenarios.
        /// </summary>
        public void PurchasedHelicopter()
        {
            governmentService.PurchaseHelicopter();
        }

        /// <summary>
        ///     Determines if the player has purchased an helicopter for a possible escape.
        /// </summary>
        /// <returns><c>true</c> if the player is has purchased the helicopter; otherwise, <c>false</c>.</returns>
        public bool HasPlayerPurchasedHelicopter()
        {
            return governmentService.HasPlayerPurchasedHelicopter();
        }

        /// <summary>
        ///     Determines if the player is able to escape by helicopter. There is a 1/4 chances that the helicopter won't start.
        /// </summary>
        /// <returns><c>true</c> if the player is able to escape by helicopter; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAbleToEscapeByHelicopter()
        {
            return escapeService.IsPlayerAbleToEscapeByHelicopter();
        }

        /// <summary>
        ///     Determines if the player is able to escape after the war is lost. There is a 2/3 chances that the player is able to escape.
        /// </summary>
        /// <returns><c>true</c> if manages to escape after losing the war; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAbleToEscapeAfterLosingWar()
        {
            return escapeService.IsPlayerAbleToEscapeAfterLosingWar();
        }

        /// <summary>
        ///     Checks if the player is captured by the guerrillas when attempting to escape through leftoto.
        /// </summary>
        /// <returns><c>true</c> if the guerrilas capture the player while attempting escape; otherwise, <c>false</c>.</returns>
        public bool DoesGuerrillaCatchPlayerEscaping()
        {
            return escapeService.DoesGuerrillaCatchPlayerEscaping();
        }

        /// <summary>
        ///     Marks the player as dead.
        /// </summary>
        public void KillPlayer()
        {
            governmentService.KillPlayer();
        }

        /// <summary>
        ///     Retrieves the currently highest achieved saved score.
        /// </summary>
        /// <returns>The current high score.</returns>
        public int GetHighScore()
        {
            return scoreService.GetCurrentHighScore();
        }

        /// <summary>
        ///     Retrieves the score of the current game state.
        /// </summary>
        /// <returns>The score value.</returns>
        public Score GetCurrentScore()
        {
            return scoreService.GetCurrentScore();
        }

        /// <summary>
        ///     Records the current score as the new high score if it is greater than the previous score.
        /// </summary>
        public void SaveHighScore()
        {
            scoreService.SaveHighScore();
        }

        /// <summary>
        ///     Applies the bankruptcy state effects which consists of a decrease in popularity with the Army and Secret Police and also decrease the strength
        ///     of the Player and the Secret Police.
        /// </summary>
        public void ApplyBankruptcyEffects()
        {
            accountService.ApplyBankruptcyEffects();
        }
    }
}
