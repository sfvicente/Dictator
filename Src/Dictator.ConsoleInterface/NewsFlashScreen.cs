using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class NewsflashScreen : INewsflashScreen
    {
        private readonly INewsStats newsStats;

        public NewsflashScreen(INewsStats newsStats)
        {
            this.newsStats = newsStats;
        }

        public void Show()
        {
            
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(24, 10, "NEWSFLASH");
            ConsoleEx.WriteAt(24, 14, newsStats.CurrentNews.Text);

            Console.ReadKey(true);
        }
    }
}
