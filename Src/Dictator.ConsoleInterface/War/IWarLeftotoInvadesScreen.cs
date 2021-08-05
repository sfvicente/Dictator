using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public interface IWarLeftotoInvadesScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show(WarStats warStats);
    }
}
