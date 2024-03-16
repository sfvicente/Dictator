﻿using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player crushes the revolution
    ///     and is given a choice to punish the revolutionaries.
    /// </summary>
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
