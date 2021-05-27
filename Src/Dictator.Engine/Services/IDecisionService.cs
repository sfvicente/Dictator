using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IDecisionService
    {
        public void Initialise();
        public Decision[] GetDecisionsByType(DecisionType decisionType);
        public void MarkDecisionAsUsed(string text);

        /// <summary>
        ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
        /// </summary>
        /// <param name="decision">The decision whose effects will be apply.</param>
        void ApplyDecisionEffects(Decision decision);
    }
}
