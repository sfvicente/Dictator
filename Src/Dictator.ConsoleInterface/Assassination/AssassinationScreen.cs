using Dictator.Common.Extensions;
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
            int startPosition = (32 - groupName.Length) / 2;

            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 11, "      ASSASSINATION ATTEMPT     ");
            ConsoleEx.WriteAt(startPosition, 12, $"by one of {groupName}");
            pressAnyKeyControl.Show();
        }
    }
}
