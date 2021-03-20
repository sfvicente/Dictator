﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IEngine
    {
        public void Initialise();
        public void AdvanceMonth();
        public bool IsGovernmentBankrupt();
        public void SetMonthlyMinimalPopularityAndStrength();
        public void SetMonthlyRevolutionStrength();
        public void SetRandomAudienceRequest();
        public void Plot();
        public bool ShouldNewsHappen();
        public void ApplyNewsEffects();
        public void ApplyAudienceEffects();
        public bool TryTriggerRandomUnusedNews();
        public bool ShouldAssassinationAttemptHappen();
        public bool IsAssassinationSuccessful();
        public void End();
        public bool TryTriggerRevoltGroup();
        public bool HasPlayerPurchasedHelicopter();
        public bool AttemptEscapeByHelicopter();
        public void KillPlayer();
    }
}
