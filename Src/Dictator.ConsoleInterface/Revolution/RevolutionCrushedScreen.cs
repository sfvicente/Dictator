using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionCrushedScreen : IRevolutionCrushedScreen
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public RevolutionCrushedScreen(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show()
        {
            ConsoleEx.WriteAt(1, 10, "  The REVOLT has been CRUSHED   ");
            ConsoleEx.WriteAt(1, 12, "  PUNISH the REVOLUTIONARIES ?  ");
            return pressAnyKeyWithYesControl.Show();
        }
    }
}
