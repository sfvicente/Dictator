using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.End
{
    public class EndScreen : IEndScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IGovernmentStats governmentStats;

        public EndScreen(IPressAnyKeyControl pressAnyKeyControl, IGovernmentStats governmentStats)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.governmentStats = governmentStats;
        }

        public void Show(Score score)
        {
            // TODO: scores values need to be aligned to the right

            ConsoleEx.Clear(ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(5, 3, "Your RATING as PRESIDENT", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 6, $" Total POPULARITY - {score.TotalPopularity}  ", ConsoleColor.DarkYellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 8, $" MONTHS in OFFICE ({governmentStats.Month}x3) - { governmentStats.Month * 3}", ConsoleColor.DarkYellow, ConsoleColor.Black);

            // TODO: count alive score
            ConsoleEx.WriteAt(1, 10, $" For staying alive - {score.PointsForStayingAlive}  ", ConsoleColor.DarkYellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, " For ", ConsoleColor.DarkYellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(6, 12, $"MONEYGRABBING", ConsoleColor.Green, ConsoleColor.White);
            ConsoleEx.WriteAt(6, 13, $"(${score.MoneyGrabbed}.000,000 /00,000) - {score.PointsForMoneyGrabbing}", ConsoleColor.DarkYellow, ConsoleColor.Black);

            int finalScore = 0;

            if (finalScore > governmentStats.LastScore)
            {
                // Save the final score as the new highscore
                governmentStats.LastScore = finalScore;
            }

            ConsoleEx.WriteAt(1, 16, $" Your TOTAL is ", ConsoleColor.DarkYellow, ConsoleColor.Black);
            ConsoleEx.Write($"{score.TotalScore}", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 18, $"[ Highest Score so far is {score.HighestScore} ]", ConsoleColor.DarkYellow, ConsoleColor.Black);
            pressAnyKeyControl.Show();
        }
    }
}
