using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IEngine
    {
        public void Initialise();
        public Account GetAccount();
        public void SetGroupStrength(GroupType groupType, int strength);
        public int GetTreasuryBalance();
        public PoliceReportRequest RequestPoliceReport();
        public PoliceReport GetPoliceReport();

        /// <summary>
        ///     Retrieves the month number of the current game.
        /// </summary>
        /// <returns>The current month number.</returns>
        public int GetMonth();

        public void AdvanceMonth();
        public void PayMonthlyCosts();

        /// <summary>
        ///     Steals half of the current treasury funds to the players private Swiss bank account.
        /// </summary>
        /// <returns>The details of the bank transfer consisting of the previous treasury balance and the amount stolen.</returns>
        public SwissBankAccountTransfer TransferToSwissBankAccount();

        /// <summary>
        ///     Determines if the government is bankrupt, which means that the treasury contains no funds.
        /// </summary>
        /// <returns><c>true</c> if government is bankrupt; otherwise, <c>false</c>.</returns>
        public bool IsGovernmentBankrupt();
        public void PayFromTreasury(int amount);
        public void SetMonthlyMinimalPopularityAndStrength();
        public void SetMonthlyRevolutionStrength();
        public void ApplyBankruptcyEffects();
        public Audience SelectRandomUnusedAudienceRequest();
        public void Plot();

        /// <summary>
        ///     Determines if a random news event should happen in the current month.
        /// </summary>
        /// <returns><c>true</c> if a random news event should happen in the current month; otherwise, <c>false</c>.</returns>
        public bool ShouldNewsHappen();

        /// <summary>
        ///     Determines if at least one news event exists that hasn't been used in the current game.
        /// </summary>
        /// <returns><c>true</c> if an unused news event exists; otherwise, <c>false</c>.</returns>
        public bool DoesUnusedNewsExist();

        /// <summary>
        ///     Selects a random news event that hasn't been used in the current game.
        /// </summary>
        /// <returns>A random unused news event.</returns>
        public News SelectRandomUnusedNews();

        /// <summary>
        ///     Applies the effects of a specific news event on the groups and treasury-
        /// </summary>
        /// <param name="news">The news whose effect will be applied.</param>
        public void ApplyNewsEffects(News news);

        public void AcceptAudienceRequest(Audience audience);
        public void RefuseAudienceRequest(Audience audience);
        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber);
        public Decision[] GetDecisionsByType(DecisionType decisionType);
        public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionSelected);

        /// <summary>
        ///     Increases bodyguard, resulting in an increment of the player's strength by 2.
        /// </summary>
        public void IncreaseBodyguard();

        /// <summary>
        ///     Asks for foreign aid in the form of a loan to either America or Russia.
        /// </summary>
        /// <param name="country">The country to which the loan request will be made.</param>
        /// <returns>The loan application result that includes if the loan has been approved or refused.</returns>
        public LoanApplicationResult AskForLoan(Country country);

        public void ApplyDecisionEffects(Decision decision);
        public void MarkDecisionAsUsed(string text);
        public bool ShouldAssassinationAttemptHappen();
        public string GetAssassinationGroupName();
        public bool IsAssassinationSuccessful();
        public bool DoesConflictExist();
        public bool ShouldWarHappen();
        public void ApplyThreatOfWarEffects();
        public WarStats BeginInvasion();
        public bool ExecuteWar(WarStats warStats);
        public bool TryTriggerRevoltGroup();
        public Dictionary<int, Group> FindPossibleAlliesForPlayer();
        public bool DoesGroupAcceptAllianceInRevolution(int groupId);
        public void SetPlayerAllyForRevolution(int selectedAllyGroupId);
        public bool DoesRevolutionSucceed();
        public void PunishRevolutionaries();
        public void ApplyRevolutionCrushedEffects();

        /// <summary>
        ///     Purchases an helicopter to have another route to escape in case of a war or revolution event.
        /// </summary>
        public void PurchasedHelicopter();

        public bool HasPlayerPurchasedHelicopter();
        public bool IsPlayerAbleToEscapeByHelicopter();

        /// <summary>
        ///     Determines if the player is able to escape after the war is lost. There is a 2/3 chances that the player is able to escape.
        /// </summary>
        /// <returns><c>true</c> if manages to escape after losing the war; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAbleToEscapeAfterLosingWar();

        /// <summary>
        ///     Checks if the player is captured by the guerrillas when attempting to escape through leftoto.
        /// </summary>
        /// <returns><c>true</c> if the guerrilas capture the player while attempting escape; otherwise, <c>false</c>.</returns>
        public bool DoesGuerrillaCatchPlayerEscaping();

        /// <summary>
        ///     Marks the player as dead.
        /// </summary>
        public void KillPlayer();

        public int GetHighscore();
        public Score GetCurrentScore();
        public void SaveHighScore();
    }
}