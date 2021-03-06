﻿using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Assassination
{
    public class AssassinationScreen : IAssassinationScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AssassinationScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(string groupName)
        {
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 11, "      ASSASSINATION ATTEMPT     ");
            ConsoleEx.WriteCenteredAt(12, $"by one of {groupName}");
            pressAnyKeyControl.Show();
        }
    }
}
