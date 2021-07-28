using System.Collections.Generic;

namespace Dictator.Core
{
    public interface IEngine
    {
        /// <summary>
        ///     Initializes the game engine by initializing all of the required service components.
        /// </summary>
        public void Initialise();

        /// <summary>
        ///     Retrieves the current account aggregated properties.
        /// </summary>
        /// <returns>The account information including Swiss bank account information, treasury balance
        /// and monthly costs</returns>
        public Account GetAccount();

        /// <summary>
        ///     Sets the level of strength for a specific group.
        /// </summary>
        /// <param name="groupType">The type of the group to set the strength.</param>
        /// <param name="strength">The strength level which will be set.</param>
        public void SetGroupStrength(GroupType groupType, int strength);

        /// <summary>
        ///     Retrieves a request for a detailed account or statement from police on the state of the groups and government.
        /// </summary>
        /// <returns>A police report request with information on the popularity and strength requirements.</returns>
        public PoliceReportRequest RequestPoliceReport();

        /// <summary>
        ///     Retrieves the police report with the current month, revolution strength, player strength and group information.
        /// </summary>
        /// <returns>The police report with the current month, revolution strength, player strength and group information.</returns>
        public PoliceReport GetPoliceReport();

        /// <summary>
        ///     Retrieves the month number of the current game.
        /// </summary>
        /// <returns>The current month number.</returns>
        public int GetMonth();

        /// <summary>
        ///     Advances the month number of the current game.
        /// </summary>
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

        /// <summary>
        ///     Removes the specified amount of funds from the treasury.
        /// </summary>
        /// <param name="amount">The amount to spend from the treasury.</param>
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

        /// <summary>
        ///     Accepts an audience request with the associated modifications to group popularity and strength and also the changes to the treasury.
        /// </summary>
        /// <param name="audience">The audience to be accepted.</param>
        public void AcceptAudienceRequest(Audience audience);

        /// <summary>
        ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
        /// </summary>
        /// <param name="audience">The audience to be accepted.</param>
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

        /// <summary>
        ///     Retrieves a decisions with the specified type and index.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <param name="optionSelected">The position of the decision within the group of decisions with the specified type.</param>
        /// <returns>The position that matches the specified type and at the specific position.</returns>
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

        /// <summary>
        ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
        /// </summary>
        /// <param name="decision">The decision whose effects will be apply.</param>
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

        /// <summary>
        ///     Retrieves the name of the group responsible for the assassination attempt.
        /// </summary>
        /// <returns>The name of the assassination group.</returns>
        public string GetAssassinationGroupName();

        /// <summary>
        ///     Determines if an assassination attempt on the player is successful.
        /// </summary>
        /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
        public bool IsAssassinationSuccessful();

        /// <summary>
        ///     Determines if there is conflict between the republic of Ritimba and Leftoto. A conflict does not exist if the player is popular
        ///     with the leftotans or leftotans are weak.
        /// </summary>
        /// <returns><c>true</c> if a conflict exist with Leftoto; otherwise, <c>false</c>.</returns>
        public bool DoesConflictExist();

        /// <summary>
        ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
        /// </summary>
        /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldWarHappen();

        /// <summary>
        ///     Applies the effects of a threat of war with leftoto, which results in an increase of the player's popularity amongst the army,
        ///     peasants, landowners and secret police.
        /// </summary>
        public void ApplyThreatOfWarEffects();

        public WarStats BeginInvasion();
        public bool ExecuteWar(WarStats warStats);
        public bool TryTriggerRevoltGroup();
        public Dictionary<int, Group> FindPossibleAlliesForPlayer();
        public bool DoesGroupAcceptAllianceInRevolution(int groupId);
        public void SetPlayerAllyForRevolution(int selectedAllyGroupId);

        /// <summary>
        ///     Determines if a revolution has succeeded in overthrowing the player.
        /// </summary>
        /// <returns><c>true</c> if the revolution has succeeded; otherwise, <c>false</c>.</returns>
        public bool DoesRevolutionSucceed();

        /// <summary>
        ///     Punishes the groups that have taken part in a revolution that has been offset by the player. It
        ///     sets the revolutionaries strength and popularity to zero.
        /// </summary>
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

        /// <summary>
        ///     Determines if the player has purchased an helicopter for a possible escape.
        /// </summary>
        /// <returns><c>true</c> if the player is has purchased the helicopter; otherwise, <c>false</c>.</returns>
        public bool HasPlayerPurchasedHelicopter();

        /// <summary>
        ///     Determines if the player is able to escape by helicopter. There is a 1/4 chances that the helicopter won't start.
        /// </summary>
        /// <returns><c>true</c> if the player is able to escape by helicopter; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        ///     Retrieves the score of the current game state.
        /// </summary>
        /// <returns>The score value.</returns>
        public Score GetCurrentScore();

        /// <summary>
        ///     Records the current score as the new high score if it is greater than the previous score.
        /// </summary>
        public void SaveHighScore();
    }
}