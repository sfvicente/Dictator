using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when Leftoto invades and initiates the war
    ///     against the republic of Ritimban.
    /// </summary>
    public interface IWarLeftotoInvadesScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show(WarStats warStats);
    }
}
