using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IRevolutionService
    {
        public Dictionary<int, Group> FindPossibleAllies();
        public void SetPlayerAllyForRevolution(int selectedAllyGroupId);
        public void SetRevolutionaryGroup(Group revolutionaryGroup);
        public void PunishRevolutionaries();
        public void BoostAllyPopularity();
    }
}