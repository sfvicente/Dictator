using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionCrushedDialog : IRevolutionCrushedDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public RevolutionCrushedDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 10, "  The REVOLT has been CRUSHED   ");
            ConsoleEx.WriteAt(1, 12, "  PUNISH the REVOLUTIONARIES ?  ");

            return pressAnyKeyWithYesControl.Show();
        }
    }
}
