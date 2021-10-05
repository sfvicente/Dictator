using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Assassination
{
    /// <summary>
    ///     Represents the screen that is displayed when an assassination attempt by one of the groups 
    ///     against the player happens.
    /// </summary>
    public interface IAssassinationScreen
    {
        public void Show(string groupName);
    }
}
