using Dictator.Core.Models;
using System;
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

    /// <summary>
    ///     Represents the dialog that is displayed when the player must choose a group
    ///     to form an alliance in a revolution scenario.
    /// </summary>
    public class RevolutionAskForHelpDialog : BaseScreen, IRevolutionAskForHelpDialog
    {
        public RevolutionAskForHelpDialog(IConsoleService consoleService)
            : base(consoleService)
        {

        }

        public int Show(Revolutionary revolutionary, Dictionary<int, Group> possibleAllies)
        {
            _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.WriteCenteredAt(5, $"{revolutionary.RevolutionaryGroupName} have joined with");
            _consoleService.WriteCenteredAt(6, $"{revolutionary.RevolutionaryGroupAllyName}");
            _consoleService.WriteAt(1, 7, $"  Their combined STRENGTH is {revolutionary.CombinedStrength} ");
            _consoleService.WriteAt(1, 9, "  WHO are you ASKING for HELP ? ");

            for (int i = 1; i < 7; i++)
            {
                if (possibleAllies.TryGetValue(i, out Group value))
                {
                    _consoleService.WriteAt(6, 14 + i, $"{i} {value.Name}");
                }
                else
                {
                    _consoleService.WriteEmptyLineAt(14 + i);
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
