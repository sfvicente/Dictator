using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IEngine
    {
        Audience CurrentAudience { get; }

        public void Initialise();
        public void AdvanceMonth();
        public bool IsGovernmentBankrupt();
        public void SetMonthlyMinimalPopularityAndStrength();
        public void SetMonthlyRevolutionStrength();
        void SelectAudienceRequest();
        public bool ShouldNewsHappen();
        public bool TryTriggerRandomUnusedNews();
        public bool ShouldAssassinationAttemptHappen();
        bool IsAssassinationSuccessful();
        public void End();
        bool TryTriggerRevoltGroup();
        bool HasPlayerPurchasedHelicopter();
        bool AttemptEscapeByHelicopter();
        void KillPlayer();
    }
}
