using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IRevolutionService
    {
        public void SetRevolutionaryGroup(Group revolutionaryGroup);
        public void PunishRevolutionaries();
        public void ApplyRevolutionCrushedEffects();
    }
}
