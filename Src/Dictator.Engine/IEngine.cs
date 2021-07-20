using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IEngine
    {
        public void Initialise();
        public Account GetAccount();

        /// <summary>
        ///     Sets the level of strength for a specific group.
        /// </summary>
        /// <param name="groupType">The type of the group to set the strength.</param>
        /// <param name="strength">The strength level which will be set.</param>
        public void SetGroupStrength(GroupType groupType, int strength);

        public PoliceReportRequest RequestPoliceReport();
        public PoliceReport GetPoliceReport();

        /// <summary>
        ///     Retrieves the month number of the current game.
        /// </summary>
        /// <returns>The current month number.</returns>
        public int GetMonth();

        public void AdvanceMonth();

        /// <summary>
        ///     Takes the required funds from treasury to pay for the monthly expenses.
        /// </summary>
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

        /// <summary>
        ///     Sets the minimal monthly level requirement for popularity and strength which is used for a diverse
        ///     number of game logic when interacting with groups, such as requesting external financial aid or 
        ///     finding allies in a revolution.
        /// </summary>
        public void SetMonthlyMinimalPopularityAndStrength();

        /// <summary>
        ///     Sets the level of strength of a possible revolution for the current turn.
        /// </summary>
        public void SetMonthlyRevolutionStrength();        

        /// <summary>
        ///     Applies the bankruptcy state effects which consists of a decrease in popularity with the Army and Secret Police and also decrease the strength
        ///     of the Player and the Secret Police.
        /// </summary>
        public void ApplyBankruptcyEffects();

        /// <summary>
        ///     Retrieves an unused audience request from the list of audience requests and
        ///     marks it as used.
        /// </summary>
        /// <returns>An unused audience request.</returns>
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

        /// <summary>
        ///     Determines if a presidential option exists and is available for selection.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <param name="optionNumber">The option number within the type group of the decision.</param>
        /// <returns><c>true</c> if the presidential decision exists and is available for selection; otherwise, <c>false</c>.</returns>
        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber);

        /// <summary>
        ///     Retrieves all the decisions of a specific type.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <returns>An array of decisions of the specified type.</returns>
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

        /// <summary>
        ///     Marks a presidential decision option as used.
        /// </summary>
        /// <param name="text">The text of the presidential decision which will be marked as used.</param>
        public void MarkDecisionAsUsed(string text);

        /// <summary>
        ///     Determines if an assassination attempt on the player should happen by one of the following groups: army, 
        ///     peasants, landowners and guerrilas.
        /// </summary>
        /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        ///     Applies the effects that result of a player crushing a revolution. It boosts ally popularity, prevents revolutions
        ///     for the next two months and resets all group status and allies from previous plots.
        /// </summary>
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

        /// <summary>
        ///     Retrieves the currently highest achieved saved score.
        /// </summary>
        /// <returns>The current high score.</returns>
        public int GetHighScore();

        public Score GetCurrentScore();

        /// <summary>
        ///     Records the current score as the new high score if it is greater than the previous score.
        /// </summary>
        public void SaveHighScore();
    }
}