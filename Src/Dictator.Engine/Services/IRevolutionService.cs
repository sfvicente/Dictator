using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IRevolutionService
    {
        public void SetRevolutionaryGroup(Group revolutionaryGroup);
        public void PunishRevolutionaries();

        /// <summary>
        ///     Applies the resulting effects when the player crushes the revolution.
        /// </summary>
        public void ApplyRevolutionCrushedEffects();
    }
}
