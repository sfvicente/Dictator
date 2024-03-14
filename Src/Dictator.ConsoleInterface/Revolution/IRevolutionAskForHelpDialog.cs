using Dictator.Core.Models;
using System.Collections.Generic;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player must choose a group
    ///     to form an alliance in a revolution scenario.
    /// </summary>
    public interface IRevolutionAskForHelpDialog
    {
        int Show(Revolutionary revolutionary, Dictionary<int, Group> possibleAllies);
    }
}
