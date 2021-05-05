using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionAskForHelpDialog : IRevolutionAskForHelpDialog
    {
        public int Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "           REVOLUTION           ");

            // TODO: add groups and allies

            ConsoleEx.WriteAt(1, 9, "  WHO are you ASKING for HELP ? ");

            return 0;
        }
    }
}
