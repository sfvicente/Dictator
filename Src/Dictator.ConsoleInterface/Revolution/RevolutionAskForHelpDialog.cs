using Dictator.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionAskForHelpDialog : IRevolutionAskForHelpDialog
    {
        public int Show(Dictionary<int, Group> possibleAllies)
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "           REVOLUTION           ");

            // TODO: add groups and allies

            ConsoleEx.WriteAt(1, 9, "  WHO are you ASKING for HELP ? ");

            int line = 12;

            for (int i = 1; i < 7; i++)
            {
                if(possibleAllies.ContainsKey(i))
                {
                    ConsoleEx.WriteAt(line, 6, $"{i} {possibleAllies.GetValueOrDefault(i)}");
                }
                else
                {
                    ConsoleEx.WriteEmptyLineAt(line);
                }

                line++;
            }

            // TODO: detect number pressed

            return 0;
        }
    }
}
