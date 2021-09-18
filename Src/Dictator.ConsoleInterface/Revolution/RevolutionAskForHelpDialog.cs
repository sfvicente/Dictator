using Dictator.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player must choose a group
    ///     to form an alliance in a revolution scenario.
    /// </summary>
    public class RevolutionAskForHelpDialog : IRevolutionAskForHelpDialog
    {
        public int Show(Revolutionary revolutionary, Dictionary<int, Group> possibleAllies)
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteCenteredAt(5, $"{revolutionary.RevolutionaryGroupName} have joined with");
            ConsoleEx.WriteCenteredAt(6, $"{revolutionary.RevolutionaryGroupAllyName}");
            ConsoleEx.WriteAt(1, 7, $"  Their combined STRENGTH is {revolutionary.CombinedStrength} ");
            ConsoleEx.WriteAt(1, 9, "  WHO are you ASKING for HELP ? ");

            for (int i = 1; i < 7; i++)
            {
                if(possibleAllies.ContainsKey(i))
                {
                    ConsoleEx.WriteAt(6, 14 + i, $"{i} {possibleAllies[i].Name}");
                }
                else
                {
                    ConsoleEx.WriteEmptyLineAt(14 + i);
                }
            }

            return GetOptionSelected(possibleAllies);
        }

        private int GetOptionSelected(Dictionary<int, Group> possibleAllies)
        {
            while (true)
            {
                ConsoleKey keyPressed = Console.ReadKey(true).Key;

                if (keyPressed >= ConsoleKey.D1 && keyPressed <= ConsoleKey.D6)
                {
                    int optionNumber = -1;

                    switch (keyPressed)
                    {
                        case ConsoleKey.D1:
                            optionNumber = 1;
                            break;
                        case ConsoleKey.D2:
                            optionNumber = 2;
                            break;
                        case ConsoleKey.D3:
                            optionNumber = 3;
                            break;
                        case ConsoleKey.D4:
                            optionNumber = 4;
                            break;
                        case ConsoleKey.D5:
                            optionNumber = 5;
                            break;
                        case ConsoleKey.D6:
                            optionNumber = 6;
                            break;
                        default:
                            break;
                    }

                    if (possibleAllies.ContainsKey(optionNumber))
                    {
                        return optionNumber;
                    }
                }
            }
        }
    }
}
