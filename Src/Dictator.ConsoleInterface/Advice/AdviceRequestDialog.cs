using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Advice
{
    public class AdviceRequestDialog : IAdviceRequestDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public AdviceRequestDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <returns>The option selected after the dialog has been presented.</returns>
        public DialogResult Show()
        {
            ConsoleEx.Clear(ConsoleColor.Green);
            
            for(int row = 2; row < 21; row++)
            {
                ConsoleEx.WriteAt(11, row, "?", ConsoleColor.Black, ConsoleColor.White);
                ConsoleEx.WriteAt(12, row, " ADVICE ", ConsoleColor.Gray, ConsoleColor.Black);
                ConsoleEx.WriteAt(20, row, "?", ConsoleColor.Black, ConsoleColor.White);
            }

            return pressAnyKeyWithYesControl.Show();
        }
    }
}
