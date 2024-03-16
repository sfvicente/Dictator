using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface;

public interface IAdviceRequestDialog
{
    DialogResult Show();
}

public class AdviceRequestDialog : IAdviceRequestDialog
{
    private readonly IPressAnyKeyWithYesControl _pressAnyKeyWithYesControl;

    public AdviceRequestDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
    {
        _pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
    }

    public DialogResult Show()
    {
        ConsoleEx.Clear(ConsoleColor.Green);

        for (int row = 2; row < 21; row++)
        {
            ConsoleEx.WriteAt(11, row, "?", ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(12, row, " ADVICE ", ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(20, row, "?", ConsoleColor.Black, ConsoleColor.White);
        }

        return _pressAnyKeyWithYesControl.Show();
    }
}
