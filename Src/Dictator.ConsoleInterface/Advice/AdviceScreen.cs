using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Advice
{
    public class AdviceScreen : IAdviceScreen
    {
        private readonly IGroupService groupService;
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AdviceScreen(IGroupService groupService, IPressAnyKeyControl pressAnyKeyControl)
        {
            this.groupService = groupService;
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(GameAction gameAction)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 2, $"{gameAction.Text}", ConsoleColor.Black, ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(1, 4, "Your POPULARITY with", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ....", ConsoleColor.Black);
            DisplayPopularityChanges(gameAction.GroupPopularityChanges);
            ConsoleEx.WriteAt(1, Console.CursorTop + 3, "The STRENGTH of", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ...", ConsoleColor.Black);
            DisplayGroupStrengthChanges(gameAction.GroupStrengthChanges);
            pressAnyKeyControl.Show();
        }

        private void DisplayPopularityChanges(string groupPopularityChanges)
        {
            int line = 6;

            for (int i = 0; i < 8; i++)
            {
                if (groupPopularityChanges[i] != 'M')
                {
                    int popularityChange = groupPopularityChanges[i] - 'M';
                    
                    ConsoleEx.WriteAt(1, line, $"  {groupService.GetGroupNameByIndex(i)}", ConsoleColor.Black);
                    ConsoleEx.WriteAt(22, line, $"{GetFormattedChange(popularityChange)}", ConsoleColor.Black);
                    line++;
                }
            }
        }

        private void DisplayGroupStrengthChanges(string groupStrengthChanges)
        {
            int line = Console.CursorTop + 3;

            for (int i = 0; i < 6; i++)
            {
                if (groupStrengthChanges[i] != 'M')
                {
                    int strengthChange = groupStrengthChanges[i] - 'M';

                    ConsoleEx.WriteAt(1, line, $"  {groupService.GetGroupNameByIndex(i)}", ConsoleColor.Black);
                    ConsoleEx.WriteAt(22, line, $"{GetFormattedChange(strengthChange)}", ConsoleColor.Black);
                    line++;
                }
            }
        }

        private string GetFormattedChange(int change)
        {
            if(change > 0)
            {
                return "+" + change;
            }

            return change.ToString();
        }
    }
}
