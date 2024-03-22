using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player crushes the revolution
    ///     and is given a choice to punish the revolutionaries.
    /// </summary>
    public interface IRevolutionCrushedDialog
    {
        public DialogResult Show();
    }

    /// <summary>
    ///     Represents the dialog that is displayed when the player crushes the revolution
    ///     and is given a choice to punish the revolutionaries.
    /// </summary>
    public class RevolutionCrushedDialog : BaseScreen, IRevolutionCrushedDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public RevolutionCrushedDialog(IConsoleService consoleService, IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
            : base(consoleService)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show()
        {
            _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.WriteAt(1, 10, "  The REVOLT has been CRUSHED   ");
            _consoleService.WriteAt(1, 12, "  PUNISH the REVOLUTIONARIES ?  ");

            return pressAnyKeyWithYesControl.Show();
        }
    }
}
