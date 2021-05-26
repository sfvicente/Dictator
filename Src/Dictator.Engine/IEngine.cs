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
        public int GetMonth();
        public void AdvanceMonth();
        public void PayMonthlyCosts();
        public SwissBankAccountTransfer TransferToSwissBankAccount();
        public bool IsGovernmentBankrupt();
        public void PayFromTreasury(int amount);
        public void SetMonthlyMinimalPopularityAndStrength();
        public void SetMonthlyRevolutionStrength();
        public void ApplyBankruptcyEffects();
        public Audience SelectRandomUnusedAudienceRequest();
        public void Plot();
        public bool ShouldNewsHappen();
        public void ApplyNewsEffects(News news);
        public void AcceptAudienceRequest(Audience audience);
        public void RefuseAudienceRequest(Audience audience);
        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber);
        public Decision[] GetDecisionsByType(DecisionType decisionType);
        public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionSelected);
        public void IncreaseBodyguard();
        public LoanApplicationResult AskForLoan(Country country);
        public void ApplyDecisionEffects(Decision decision);
        public void MarkDecisionAsUsed(string text);
        public bool DoesUnusedNewsExist();
        public News SelectRandomUnusedNews();
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
        public bool DoesRevolutionSucceed(int playerStrength, int revolutionStrength);
        public void PunishRevolutionaries();
        public void ApplyRevolutionCrushedEffects();
        public void PurchasedHelicopter();
        public bool HasPlayerPurchasedHelicopter();
        public bool IsPlayerAbleToEscapeByHelicopter();

        /// <summary>
        ///     Determines if the player is able to escape after the war is lost. There is a 2/3 chances that the player is able to escape.
        /// </summary>
        /// <returns><c>true</c> if manages to escape after losing the war; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAbleToEscapeAfterLosingWar();

        public bool DoesGuerrillaCatchPlayerEscaping();
        public void KillPlayer();
        public int GetHighscore();
        public Score GetCurrentScore();
        public void SaveHighScore();
    }
}