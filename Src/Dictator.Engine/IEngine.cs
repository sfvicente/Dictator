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
        public void AdvanceMonth();
        public SwissBankAccountTransfer TransferToSwissBankAccount();
        public bool IsGovernmentBankrupt();
        public void PayFromTreasury(int amount);
        public void SetMonthlyMinimalPopularityAndStrength();
        public void SetMonthlyRevolutionStrength();
        public void ApplyBankruptcyEffects();
        public Audience GetRandomUnusedAudienceRequest();
        public void Plot();
        public bool ShouldNewsHappen();
        public void ApplyNewsEffects(News news);
        public void AcceptAudienceRequest(Audience audience);
        public void RefuseAudienceRequest(Audience audience);
        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber);
        public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionSelected);
        public void IncreaseBodyguard();
        public LoanRequest AskForLoan(Country country);
        public void ApplyDecisionEffects(Decision decision);
        public void MarkDecisionAsUsed(string text);
        public bool DoesUnusedNewsExist();
        public News GetRandomUnusedNews();
        public bool ShouldAssassinationAttemptHappen();
        public bool IsAssassinationSuccessful();
        public bool DoesConflictExist();
        public bool ShouldWarHappen();
        public void ApplyThreatOfWarEffects();
        public void End();
        public bool TryTriggerRevoltGroup();
        public void InitialiseRevolution();
        public bool HasPlayerPurchasedHelicopter();
        public bool IsPlayerAbleToEscapeByHelicopter();
        public bool IsPlayerAbleToEscapeAfterLosingWar();
        public bool DoesGuerrilaCatchPlayerEscaping();
        public void KillPlayer();
        public Score GetCurrentScore();
    }
}
