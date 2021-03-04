using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core
{
    public class Engine : IEngine
    {
        //public int TreasuryBalance { get; set; }
        //public int MonthlyCosts { get; set; }
        //public int SwissBankAccountBalance { get; set; }

        //public int PlayerStrength { get; set; }
        //public bool IsPlayerAlive { get; set; }
        //public int Month { get; set; }
        //public int PlotBonus { get; set; }
        //public int RevolutionStrength { get; set; }
        //public int Minimal { get; set; }

        private IAccount account;
        private readonly IGovernmentStats governmentStats;
        private readonly IGroupStats groupStats;
        private readonly INewsStats newsStats;

        public Engine(
            IAccount account,
            IGovernmentStats governmentStats,
            IGroupStats groupStats,
            INewsStats newsStats)
        {
            this.account = account;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
            this.newsStats = newsStats;
            Initialise();
        }

        public void Initialise()
        {
            this.account.Initialise();
            this.governmentStats.Initialise();
            this.groupStats.Initialise();
            newsStats.Initialise();
        }

        public void AdvanceMonth()
        {
            this.governmentStats.Month++;
        }

        public bool IsGovernmentBankrupt()
        {
            return account.TreasuryBalance <= 0;
        }

        public void SetMonthlyMinimalPopularityAndStrength()
        {
            Random random = new Random();

            governmentStats.MonthlyMinimalPopularityAndStrength = random.Next(2, 5);
        }

        public void SetMonthlyRevolutionStrength()
        {
            Random random = new Random();

            governmentStats.MonthlyRevolutionStrength = random.Next(10, 13);
        }

        public bool ShouldNewsHappen()
        {
            Random random = new Random();

            int number = random.Next(0, 2);

            if(number == 0)
            {
                return true;
            }

            return false;
        }

        public bool TryTriggerRandomUnusedNews()
        {
            IEnumerable<News> unusedNews = newsStats.GetNews().Where(x => !x.HasBeenUsed);

            if (unusedNews.Any())
            {
                var rand = new Random();
                var randomUnusedNews = unusedNews.ElementAt(rand.Next(unusedNews.Count()));

                newsStats.CurrentNews = randomUnusedNews;
                newsStats.MarkNewsAsUsed(randomUnusedNews.Text);

                return true;
            }

            return false;         
        }

        public void End()
        {
            throw new NotImplementedException();
        }
    }
}
