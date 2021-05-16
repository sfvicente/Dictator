using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionNoAlliesScreen : IRevolutionNoAlliesScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionNoAlliesScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.WriteAt(1, 12, "      You're ON YOUR OWN !      ");
            pressAnyKeyControl.Show();
        }
    }
}
