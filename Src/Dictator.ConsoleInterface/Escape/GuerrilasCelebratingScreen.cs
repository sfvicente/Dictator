using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Escape
{
    public class GuerrilasCelebratingScreen : IGuerrilasCelebratingScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public GuerrilasCelebratingScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
