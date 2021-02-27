using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PresidentialDecisionRequestScreen
    {
        public void Draw()
        {
            ConsoleEx.Clear(ConsoleColor.Red);

            ConsoleEx.WriteAt(24, 3, "     PRESIDENTIAL DECISION      ");
            ConsoleEx.WriteAt(24, 6, " Try to ...                     ");
            ConsoleEx.WriteAt(24, 8, "    1. PLEASE a GROUP           ");
            ConsoleEx.WriteAt(24, 10, "    2. PLEASE ALL GROUPS        ");
            ConsoleEx.WriteAt(24, 12, "    3. IMPROVE your CHANCES     ");
            ConsoleEx.WriteAt(24, 14, "    4. RAISE some CASH          ");
            ConsoleEx.WriteAt(24, 16, "    5. STRENGTHEN a GROUP       ");
        }
    }
}
