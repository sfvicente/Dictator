using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    /// <summary>
    ///     Provides functionality related to the presidential decision mechanic.
    /// </summary>
    public interface IDecisionService
    {
        public void Initialise();

        /// <summary>
        ///     Retrieves all the decisions of a specific type.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <returns>An array of decisions of the specified type.</returns>
        public Decision[] GetDecisionsByType(DecisionType decisionType);

        /// <summary>
        ///     Retrieves a decisions with the specified type and index.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <param name="optionSelected">The position of the decision within the group of decisions with the specified type.</param>
        /// <returns>The position that matches the specified type and at the specific position.</returns>
        public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionNumber);

        /// <summary>
        ///     Determines if a presidential option exists and is available for selection.
        /// </summary>
        /// <param name="decisionType">The type of decision.</param>
        /// <param name="optionNumber">The option number within the type group of the decision.</param>
        /// <returns><c>true</c> if the presidential decision exists and is available for selection; otherwise, <c>false</c>.</returns>
        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber);

        /// <summary>
        ///     Marks a presidential decision option as used.
        /// </summary>
        /// <param name="text">The text of the presidential decision which will be marked as used.</param>
        public void MarkDecisionAsUsed(string text);

        /// <summary>
        ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
        /// </summary>
        /// <param name="decision">The decision whose effects will be apply.</param>
        void ApplyDecisionEffects(Decision decision);
    }
}
