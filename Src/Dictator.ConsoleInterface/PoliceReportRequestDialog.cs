using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface;

public interface IPoliceReportRequestDialog
{
    public DialogResult Show(PoliceReportRequest policeReportRequest);
}

public class PoliceReportRequestDialog : BaseScreen, IPoliceReportRequestDialog
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;
    private readonly IPressAnyKeyWithYesControl _pressAnyKeyWithYesControl;

    public PoliceReportRequestDialog(
        IConsoleService consoleService,
        IPressAnyKeyControl pressAnyKeyControl,
        IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
        _pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
    }

    public DialogResult Show(PoliceReportRequest policeReportRequest)
    {
        _consoleService.Clear(ConsoleColor.Black, ConsoleColor.White);
        _consoleService.WriteEmptyLineAt(1, ConsoleColor.Yellow);
        _consoleService.WriteEmptyLineAt(2, ConsoleColor.Yellow);
        _consoleService.WriteAt(1, 7, "     SECRET POLICE REPORT ?     ");

        DialogResult dialogResult;

        if (policeReportRequest.HasEnoughBalance && policeReportRequest.IsPlayerPopularWithSecretPolice && policeReportRequest.HasPoliceEnoughStrength)
        {
            _consoleService.WriteAt(1, 13, "         ( costs $1000 )        ");
            dialogResult = _pressAnyKeyWithYesControl.Show();
        }
        else
        {
            _consoleService.WriteAt(1, 10, "          NOT AVAILABLE         ");

            int screenRow = 12;

            if (!policeReportRequest.IsPlayerPopularWithSecretPolice)
            {
                _consoleService.WriteAt(1, screenRow++, $"  Your POPULARITY with us is {policeReportRequest.PolicePopularity}  ");
            }

            if (!policeReportRequest.HasPoliceEnoughStrength)
            {
                _consoleService.WriteAt(1, screenRow++, $"      POLICE strength is {policeReportRequest.PoliceStrength}      ");
            }

            if (!policeReportRequest.HasEnoughBalance)
            {
                _consoleService.WriteAt(1, screenRow++, "    You can't AFFORD a REPORT   ");
            }

            dialogResult = DialogResult.No;
            _pressAnyKeyControl.Show();
        }

        return dialogResult;
    }
}
