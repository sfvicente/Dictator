using Dictator.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionAskForHelpDialog : IRevolutionAskForHelpDialog
    {
        public int Show(Dictionary<int, Group> possibleAllies)
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);

            int revolutionaryStrength = 0;
            string revolutionaryGroupName = "revolutionaryGroupName";
            string revolutionaryGroupAllyName = "revolutionaryGroupAllyName";

            ConsoleEx.WriteCenteredAt(5, $"{revolutionaryGroupName} have joined with");
            ConsoleEx.WriteCenteredAt(6, $"{revolutionaryGroupAllyName}");
            ConsoleEx.WriteAt(1, 7, $"  Their combined STRENGTH is {revolutionaryStrength} ");
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
