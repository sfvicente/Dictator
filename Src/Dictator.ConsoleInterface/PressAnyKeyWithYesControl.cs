﻿using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PressAnyKeyWithYesControl : IPressAnyKeyWithYesControl
    {
        private readonly IKeyPanel keyPanel;

        public PressAnyKeyWithYesControl(IKeyPanel keyPanel)
        {
            this.keyPanel = keyPanel;
        }



        public void Show()
        {
            ConsoleEx.WriteAt(11, 21, " \"Y\"= KEY ", ConsoleColor.White, ConsoleColor.Black); // TODO: the label needs to be flashing
            keyPanel.Show();
        }
    }
}
