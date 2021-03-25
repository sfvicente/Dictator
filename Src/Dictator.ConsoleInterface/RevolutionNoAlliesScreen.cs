using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
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
            throw new NotImplementedException();
        }
    }
}
