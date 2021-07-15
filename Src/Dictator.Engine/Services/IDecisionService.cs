﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IDecisionService
    {
        public void Initialise();
        public Decision[] GetDecisionsByType(DecisionType decisionType);
        public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionNumber);

        /// <summary>
        ///     Determines if a presidential option exists and is available for selection.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <param name="optionNumber">The option number within the type group of the decision.</param>
        /// <returns><c>true</c> if the presidential decision exists and is available for selection; otherwise, <c>false</c>.</returns>
        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber);

        public void MarkDecisionAsUsed(string text);

        /// <summary>
        ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
        /// </summary>
        /// <param name="decision">The decision whose effects will be apply.</param>
        void ApplyDecisionEffects(Decision decision);
    }
}
