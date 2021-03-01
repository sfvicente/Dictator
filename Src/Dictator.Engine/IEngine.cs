using System;
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
        public bool ShouldNewsHappen();
        public bool TryTriggerRandomUnusedNews();
    }
}
