using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PressAnyKeyWithYesControl : IPressAnyKeyWithYesControl
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public PressAnyKeyWithYesControl(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.WriteAt(11, 21, " \"Y\"= KEY ", ConsoleColor.White, ConsoleColor.Black); // TODO: the label needs to be flashing
            pressAnyKeyControl.Show();
        }
    }
}
