using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class RevolutionCrushedScreen : IRevolutionCrushedScreen
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public RevolutionCrushedScreen(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public void Show()
        {
            ConsoleEx.WriteAt(1, 10, "  The REVOLT has been CRUSHED   ");
            ConsoleEx.WriteAt(1, 12, "  PUNISH the REVOLUTIONARIES ?  ");
            pressAnyKeyWithYesControl.Show();
        }
    }
}
