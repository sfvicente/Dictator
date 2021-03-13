using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AssassinationSuccededScreen : IAssassinationSuccededScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AssassinationSuccededScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {

        }
    }
}
