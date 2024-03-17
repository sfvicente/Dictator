using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.Decisions;

public interface IAudienceDecisionDialog
{
    DialogResult Show(Audience audience);
}

public class AudienceDecisionDialog : BaseScreen, IAudienceDecisionDialog
{
    private readonly IPressAnyKeyWithYesControl _pressAnyKeyWithYesControl;

    public AudienceDecisionDialog(IConsoleService consoleService, IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        : base(consoleService)
    {
        _pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
    }

    public DialogResult Show(Audience audience)
    {
        _consoleService.Clear(ConsoleColor.DarkYellow);
        _consoleService.WriteAt(11, 2, "  DECISION  ", ConsoleColor.Black, ConsoleColor.White);
        _consoleService.WriteAt(5, 4, $"The {audience.Requester} ask you to", ConsoleColor.Magenta, ConsoleColor.Gray);
        _consoleService.WriteAt(1, 6, $"{audience.Text}", ConsoleColor.Yellow, ConsoleColor.Black);
        _consoleService.WriteEmptyLineAt(8, ConsoleColor.Blue);

        Console.BackgroundColor = ConsoleColor.DarkYellow;

        if (audience.NoMoneyInvolved)
        {
            _consoleService.WriteAt(1, 11, "        NO MONEY INVOLVED       ", ConsoleColor.Black);
        }
        else
        {
            _consoleService.WriteAt(2, 10, "This decision would", ConsoleColor.Black);

            if (audience.Cost != 0)
            {
                string addOrTake = audience.Cost > 0 ? "ADD to" : "TAKE from";

                _consoleService.WriteAt(2, 12, $"{addOrTake} the TREASURY ${Math.Abs(audience.Cost)},000", ConsoleColor.Black);
            }

            if (audience.Cost != 0 && audience.MonthlyCost != 0)
            {
                _consoleService.WriteAt(2, 14, "and", ConsoleColor.Black);
            }

            if (audience.MonthlyCost != 0)
            {
                string raiseOrLower = audience.MonthlyCost < 0 ? "RAISE" : "LOWER";

                _consoleService.WriteAt(2, 16, $"{raiseOrLower} MONTHLY COSTS by ${Math.Abs(audience.MonthlyCost)},000", ConsoleColor.Black);
            }
        }

        return _pressAnyKeyWithYesControl.Show();
    }
}
