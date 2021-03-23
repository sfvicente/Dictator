using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PressAnyKeyControl : IPressAnyKeyControl
    {
        private readonly IKeyPanel keyPanel;

        public PressAnyKeyControl(IKeyPanel keyPanel)
        {
            this.keyPanel = keyPanel;
        }

        public void Show()
        {
            keyPanel.Show();
            Console.ReadKey(true);
        }
    }
}
