using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core
{
    public class Engine : IEngine
    {
        private IAccount account;
        private readonly IGovernmentStats governmentStats;
        private readonly IGroupStats groupStats;
        private readonly IAudienceStats audienceStats;
        private readonly INewsStats newsStats;

        public Engine(
            IAccount account,
            IGovernmentStats governmentStats,
            IGroupStats groupStats,
            IAudienceStats audienceStats,
            INewsStats newsStats)
        {
            this.account = account;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
            this.audienceStats = audienceStats;
            this.newsStats = newsStats;
        }

        public void Initialise()
        {
            account.Initialise();
            governmentStats.Initialise();
            groupStats.Initialise();
            audienceStats.Initialise();
            newsStats.Initialise();
        }

        public void AdvanceMonth()
        {
            governmentStats.Month++;
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

        public void SetRandomAudienceRequest()
        {
            audienceStats.SetRandomAudienceRequest();
        }

        public bool ShouldNewsHappen()
        {
            Random random = new Random();
            int number = random.Next(0, 2);

            if (number == 0)
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

        public bool ShouldAssassinationAttemptHappen()
        {
            Random random = new Random();
            int number = random.Next(0, 3);
            Group[] groups = groupStats.GetGroups();  // Select a random group between the army, peasants, landowners and guerrilas

            if (groups[number].Status == GroupStatus.Assassination)
            {
                groupStats.SetAssassinByGroupType(groups[number].Type);
                return true;
            }

            return false;
        }

        public bool IsAssassinationSuccessful()
        {
            Random random = new Random();
            int number = random.Next(0, 2);

            if (groupStats.DoesMainPopulationHatePlayer() ||
                DoesPoliceHatePlayer() ||
                IsPoliceUnableToProtectPlayer() ||
                number == 0) // Player is just unlucky

            {
                return true;
            }

            return false;
        }

        public bool TryTriggerRevoltGroup()
        {
            Random random = new Random();

            for (int guess = 0; guess < 3; guess++)     // Perform 3 tries to guess the revolt group
            {
                int number = random.Next(0, 3);
                Group[] groups = groupStats.GetGroups();

                if (groups[number].Status == GroupStatus.Revolution)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasPlayerPurchasedHelicopter()
        {
            return governmentStats.HasHelicopter;
        }

        public bool AttemptEscapeByHelicopter()
        {
            Random random = new Random();
            int number = random.Next(0, 3);

            // The player has a 1 in 4 chances that the helicopter won't start
            if (number != 0)
            {
                // The escape by helicopter is successfull
                return true;
            }

            // The escape by helicopoter fails as the helicopter won't start
            return false;
        }

        public void AttemptEscapeThroughLeftoto()
        {
            int guerrilasStrength = groupStats.GetGroupByType(GroupType.Guerillas).Strength;
            int upperLimit = (guerrilasStrength / 3) + 2;

            Random random = new Random();
            int number = random.Next(0, upperLimit);

            // There is a chance on 1 in 2..5 depending on the guerrilas strength
            if(number == 0)
            {
                // TODO: guerrila celebration
            }

            // TODO: guerrila didn't catch player
        }

        public void KillPlayer()
        {
            governmentStats.KillPlayer();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        private bool DoesPoliceHatePlayer()
        {
            if (groupStats.GetGroupByType(GroupType.SecretPolice).Popularity <= governmentStats.MonthlyMinimalPopularityAndStrength)
            {
                return true;
            }

            return false;
        }

        private bool IsPoliceUnableToProtectPlayer()
        {
            if (groupStats.GetGroupByType(GroupType.SecretPolice).Strength <= governmentStats.MonthlyMinimalPopularityAndStrength)
            {
                return true;
            }

            return false;
        }
    }
}
