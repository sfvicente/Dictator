using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class NewsFlashScreen
    {
        public void Draw()
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(24, 10, "NEWSFLASH");
            ConsoleEx.WriteAt(24, 4, "{newsStats.text}");
        }
    }
}
