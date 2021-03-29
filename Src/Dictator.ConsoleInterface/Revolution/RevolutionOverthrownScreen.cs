﻿using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionOverthrownScreen : IRevolutionOverthrownScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionOverthrownScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.WriteAt(1, 10, "    You have been OVERTHROWN    ");
            ConsoleEx.WriteAt(1, 12, "         and LIQUIDATED         ");
            pressAnyKeyControl.Show();
        }
    }
}
