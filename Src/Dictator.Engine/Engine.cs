using Dictator.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core
{
    public class Engine : IEngine
    {
        private readonly IAccountService accountService;
        private readonly IGovernmentStats governmentStats;
        private readonly IGroupStats groupStats;
        private readonly IPlotService plotService;
        private readonly IDecisionStats decisionStats;
        private readonly IAudienceStats audienceStats;
        private readonly INewsService newsService;
        private Group revolutionCurrentGroup;
        private int revolutionPlayerStrength;
        private int revolutionGroupStrength;

        public Engine(
            IAccountService accountService,
            IGovernmentStats governmentStats,
            IGroupStats groupStats,
            IPlotService plotService,
            IDecisionStats decisionStats,
            IAudienceStats audienceStats,
            INewsService newsService)
        {
            this.accountService = accountService;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
            this.plotService = plotService;
            this.decisionStats = decisionStats;
            this.audienceStats = audienceStats;
            this.newsService = newsService;
        }

        public void Initialise()
        {
            accountService.Initialise();
            governmentStats.Initialise();
            groupStats.Initialise();
            audienceStats.Initialise();
            newsService.Initialise();
        }

        public void SetGroupStrength(GroupType groupType, int strength)
        {
            groupStats.SetStrength(groupType, strength);
        }

        public void AdvanceMonth()
        {
            governmentStats.AdvanceMonth();
        }

        public bool IsGovernmentBankrupt()
        {
            return accountService.GetTreasuryBalance() <= 0;
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

        public void Plot()
        {
            plotService.Plot();
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

        public bool DoesUnusedNewsExist()
        {
            IEnumerable<News> news = newsService.GetNews().Where(x => !x.HasBeenUsed);

            if (news.Any())
            {
                return true;
            }

            return false;
        }

        public News GetRandomUnusedNews()
        {
            IEnumerable<News> unusedNews = newsService.GetNews().Where(x => !x.HasBeenUsed);

            if (unusedNews.Any())
            {
                var rand = new Random();
                var randomUnusedNews = unusedNews.ElementAt(rand.Next(unusedNews.Count()));

                return randomUnusedNews;
            }

            throw new InvalidOperationException("There are unused news items in the collection.");
        }

        public void ApplyNewsEffects(News news)
        {
            if(news == null)
            {
                throw new ArgumentNullException();
            }

            groupStats.ApplyPopularityChange(news.GroupPopularityChanges);
            groupStats.ApplyStrengthChange(news.GroupStrengthChanges);
            accountService.ApplyTreasuryChanges(news.Cost, news.MonthlyCost);
        }

        public void AcceptAudienceRequest()
        {
            Audience currentAudience = audienceStats.CurrentAudienceRequest;

            groupStats.ApplyPopularityChange(currentAudience.GroupPopularityChanges);
            groupStats.ApplyStrengthChange(currentAudience.GroupStrengthChanges);
            accountService.ApplyTreasuryChanges(currentAudience.Cost, currentAudience.MonthlyCost);
        }

        /// <summary>
        ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
        /// </summary>
        public void RefuseAudienceRequest()
        {
            Audience currentAudience = audienceStats.CurrentAudienceRequest;
            char requesterPopularityChange = currentAudience.GroupPopularityChanges[(int)currentAudience.Requester];

            // Decrease the player's popularity with the petitioners
            groupStats.DecreasePopularity(currentAudience.Requester, requesterPopularityChange - 'M');
        }

        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber)
        {
            Decision[] decisions = decisionStats.GetDecisionsByType(decisionType);

            if (optionNumber > decisions.Length)
            {
                return false;
            }

            if (decisions[optionNumber - 1].HasBeenUsed)
            {
                return false;
            }

            return true;
        }

        public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionNumber)
        {
            if (optionNumber < 0)
            {
                throw new ArgumentException(nameof(optionNumber));
            }

            Decision[] decisions = decisionStats.GetDecisionsByType(decisionType);

            if (optionNumber > decisions.Length)
            {
                throw new ArgumentException(nameof(optionNumber));
            }

            return decisions[optionNumber - 1];
        }

        public void IncreaseBodyguard()
        {
            governmentStats.IncreasePlayerStrength(2);
        }

        /// <summary>
        ///     Asks for foreign aid in the form of a loan to either America or Russia.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public LoanRequest AskForLoan(Country country)
        {
            LoanRequest loanRequest = new LoanRequest();

            // TODO: Check if it's not to yearly for a loan

            // TODO: Check if loans have been used

            // TODO: Check if popularity is above minimal

            // TODO: If accepted, calculate amount

            return loanRequest;
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

        /// <summary>
        ///     Determines if there is conflict between the republic of Ritimba and Leftoto. A conflict does not exist if the player is popular
        ///     with the leftotans or leftotans are weak.
        /// </summary>
        /// <returns></returns>
        public bool DoesConflictExist()
        {
            Group leftotans = groupStats.GetGroupByType(GroupType.Leftotans);

            if(leftotans.Popularity > governmentStats.MonthlyMinimalPopularityAndStrength ||
                leftotans.Strength < governmentStats.MonthlyMinimalPopularityAndStrength)
            {
                return false;
            }

            return true;
        }

        public bool ShouldWarHappen()
        {
            Random random = new Random();
            int number = random.Next(0, 3);

            if(number == 0)
            {
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
                    revolutionCurrentGroup = groups[number];  // As the group has been triggered, set the group as the current revolutionary
                    return true;
                }
            }

            return false;
        }

        public void InitialiseRevolution()
        {
            // TODO: Define possible allies

            // TODO: Determine the player combined strength

            // TODO: Determine the current revolutionary combined strength
        }

        public bool HasPlayerPurchasedHelicopter()
        {
            return governmentStats.HasHelicopter;
        }

        public bool IsPlayerAbleToEscapeByHelicopter()
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

        /// <summary>
        ///     Determines if the player is able to escape after the war is lost. There is a 2/3 chances that the player is able to escape.
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerAbleToEscapeAfterLosingWar()
        {
            Random random = new Random();
            int number = random.Next(0, 3);

            // The player has a 2 in 3 chances that he manages to escape
            if (number < 2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Checks if the player is captured by gurrilas when attempting to escape through leftoto.
        /// </summary>
        /// <returns></returns>
        public bool DoesGuerrilaCatchPlayerEscaping()
        {
            int guerrilasStrength = groupStats.GetGroupByType(GroupType.Guerillas).Strength;
            int upperLimit = (guerrilasStrength / 3) + 2;

            Random random = new Random();
            int number = random.Next(0, upperLimit);

            // There is a chance on 1 in 2..5 depending on the guerrilas strength
            if (number == 0)
            {
                return true;
            }

            return false;
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

        /// <summary>
        /// Applies the bankruptcy state effects which consists of a decrease in popularity with the Army and Secret Police and also decrease the strength
        /// of the Player and the Secret Police.
        /// </summary>
        public void ApplyBankruptcyEffects()
        {
            groupStats.DecreasePopularity(GroupType.Army);
            groupStats.DecreasePopularity(GroupType.SecretPolice);
            groupStats.DecreaseStrength(GroupType.SecretPolice);
            governmentStats.DecreasePlayerStrength();
        }
    }
}
