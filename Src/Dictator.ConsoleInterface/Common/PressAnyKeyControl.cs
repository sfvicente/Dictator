using System;

namespace Dictator.ConsoleInterface.Common
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
