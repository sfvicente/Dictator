using Dictator.Common.Extensions;
using Dictator.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Dictator.Core
{
    public class Engine : IEngine
    {
        private readonly IAccountService accountService;
        private readonly IGovernmentService governmentStats;
        private readonly IGroupService groupStats;
        private readonly IPlotService plotService;
        private readonly IDecisionService decisionStats;
        private readonly IAudienceService audienceStats;
        private readonly INewsService newsService;
        private readonly IRevolutionService revolutionService;
        private readonly IScoreService scoreService;

        public Engine(
            IAccountService accountService,
            IGovernmentService governmentStats,
            IGroupService groupStats,
            IPlotService plotService,
            IDecisionService decisionStats,
            IAudienceService audienceStats,
            INewsService newsService,
            IRevolutionService revolutionService,
            IScoreService scoreService)
        {
            this.accountService = accountService;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
            this.plotService = plotService;
            this.decisionStats = decisionStats;
            this.audienceStats = audienceStats;
            this.newsService = newsService;
            this.revolutionService = revolutionService;
            this.scoreService = scoreService;
        }

        public void Initialise()
        {
            accountService.Initialise();
            governmentStats.Initialise();
            groupStats.Initialise();
            audienceStats.Initialise();
            newsService.Initialise();
        }

        public Account GetAccount()
        {
            return accountService.GetAccount();
        }

        public void SetGroupStrength(GroupType groupType, int strength)
        {
            groupStats.SetStrength(groupType, strength);
        }

        /// <summary>
        ///     Gets the current treasury balance.
        /// </summary>
        /// <returns>The amount of dollars currently in the treasury.</returns>
        public int GetTreasuryBalance()
        {
            return accountService.GetTreasuryBalance();
        }

        public PoliceReportRequest RequestPoliceReport()
        {
            PoliceReportRequest policeReportRequest = new PoliceReportRequest
            {
                HasEnoughBalance = GetTreasuryBalance() > 0,
                HasEnoughPopularityWithPolice = HasEnoughPopularityWithPolice(),
                HasPoliceEnoughStrength = HasPoliceEnoughStrength(),
                PolicePopularity = groupStats.GetPopularityByGroupType(GroupType.SecretPolice),
                PoliceStrength = groupStats.GetStrengthByGroupType(GroupType.SecretPolice)
            };

            return policeReportRequest;
        }

        public PoliceReport GetPoliceReport()
        {
            PoliceReport policeReport = new PoliceReport
            {
                Month = governmentStats.Month,
                Groups = groupStats.GetGroups().AsReadOnly(),
                PlayerStrength = governmentStats.PlayerStrength,
                MonthlyRevolutionStrength = governmentStats.MonthlyRevolutionStrength
            };

            return policeReport;
        }

        public void AdvanceMonth()
        {
            governmentStats.AdvanceMonth();
        }

        /// <summary>
        ///     Takes the required funds from treasury to pay for the monthly expenses.
        /// </summary>
        public void PayMonthlyCosts()
        {
            if(accountService.GetTreasuryBalance() > 0)
            {
                accountService.ChangeTreasuryBalance(-accountService.GetMonthlyCosts());
            }
        }

        public int GetMonth()
        {
            return governmentStats.Month;
        }

        public bool IsGovernmentBankrupt()
        {
            return accountService.GetTreasuryBalance() <= 0;
        }

        public void PayFromTreasury(int amount)
        {
            accountService.ChangeTreasuryBalance(-amount);
        }

        public SwissBankAccountTransfer TransferToSwissBankAccount()
        {
            int treasuryPreviousBalance = accountService.GetTreasuryBalance();
            int amountStolen = treasuryPreviousBalance / 2;

            if(amountStolen > 0)
            {
                if(!accountService.HasSwissBankAccount())
                {
                    accountService.OpenSwissBankAccount();
                }

                accountService.ChangeTreasuryBalance(-amountStolen);
                accountService.DepositToSwissBankAccount(amountStolen);
            }

            var swissBankAccountTransfer = new SwissBankAccountTransfer()
            {
                AmountStolen = amountStolen,
                TreasuryPreviousBalance = treasuryPreviousBalance
            };

            return swissBankAccountTransfer;
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

        public void Plot()
        {
            plotService.Plot();
        }

        /// <summary>
        ///     Determines if a random news event should happen in the current month.
        /// </summary>
        /// <returns><c>true</c> if a random news event should happen in the current month; otherwise, <c>false</c>.</returns>
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
            News[] unusedNews = newsService.GetUnusedNews();

            if (unusedNews.Any())
            {
                return true;
            }

            return false;
        }

        public News GetRandomUnusedNews()
        {
            News[] unusedNews = newsService.GetUnusedNews();

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

        public Audience SelectRandomUnusedAudienceRequest()
        {
            Audience audience = audienceStats.SelectRandomUnusedAudienceRequest();

            return audience;
        }

        /// <summary>
        ///     Accepts an audience request with the associated modifications to group popularity and strength and also the changes to the treasury.
        /// </summary>
        public void AcceptAudienceRequest(Core.Audience audience)
        {
            groupStats.ApplyPopularityChange(audience.GroupPopularityChanges);
            groupStats.ApplyStrengthChange(audience.GroupStrengthChanges);
            accountService.ApplyTreasuryChanges(audience.Cost, audience.MonthlyCost);
        }

        /// <summary>
        ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
        /// </summary>
        public void RefuseAudienceRequest(Core.Audience audience)
        {
            char requesterPopularityChange = audience.GroupPopularityChanges[(int)audience.Requester];

            // Decrease the player's popularity with the petitioners
            groupStats.DecreasePopularity(audience.Requester, requesterPopularityChange - 'M');
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

        public Decision[] GetDecisionsByType(DecisionType decisionType)
        {
            Decision[] decisions = decisionStats.GetDecisionsByType(decisionType);

            return decisions;
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
        public LoanApplicationResult AskForLoan(Country country)
        {
            LoanApplicationResult loanApplicationResult = new LoanApplicationResult();

            // TODO: Check if it's not to yearly for a loan

            // TODO: Check if loans have been used

            GroupType groupType;

            switch(country)
            {
                case Country.America:
                    groupType = GroupType.Americans;
                    break;

                case Country.Russia:
                    groupType = GroupType.Russians;
                    break;

                default:
                    throw new InvalidEnumArgumentException(nameof(country), (int)country, country.GetType());
            }

            Group group = groupStats.GetGroupByType(groupType);

            if(group.Popularity <= governmentStats.MonthlyMinimalPopularityAndStrength)
            {
                loanApplicationResult.IsAccepted = false;
                loanApplicationResult.RefusalType = LoanApplicationRefusalType.NotPopularEnough;
            }
            else
            {
                loanApplicationResult.IsAccepted = true;
                loanApplicationResult.RefusalType = LoanApplicationRefusalType.None;
                loanApplicationResult.Amount = CalculateLoanAmount(group);
                accountService.ChangeTreasuryBalance(loanApplicationResult.Amount);
            }

            return loanApplicationResult;
        }

        /// <summary>
        ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
        /// </summary>
        /// <param name="decision"></param>
        public void ApplyDecisionEffects(Decision decision)
        {
            if (decision == null)
            {
                throw new ArgumentNullException();
            }

            groupStats.ApplyPopularityChange(decision.GroupPopularityChanges);
            groupStats.ApplyStrengthChange(decision.GroupStrengthChanges);
            accountService.ApplyTreasuryChanges(decision.Cost, decision.MonthlyCost);
        }

        public void MarkDecisionAsUsed(string text)
        {
            decisionStats.MarkDecisionAsUsed(text);
        }

        /// <summary>
        ///     Determines if an assassination attempt should be performed by one of the following groups: army, 
        ///     peasants, landowners and guerrilas.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
        /// </summary>
        /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        ///     Applies the effects of a threat of war with leftoto, which results in an increase of the player's popularity amongst the army,
        ///     peasants, landowners and secret police.
        /// </summary>
        public void ApplyThreatOfWarEffects()
        {
            groupStats.IncreasePopularity(GroupType.Army);
            groupStats.IncreasePopularity(GroupType.Peasants);
            groupStats.IncreasePopularity(GroupType.Landowners);
            groupStats.DecreasePopularity(GroupType.SecretPolice);
        }

        /// <summary>
        ///     Determines if an assassination attempt on the player is successful.
        /// </summary>
        /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
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

        public WarStats BeginInvasion()
        {
            var warStats = new WarStats
            {
                RitimbanStrength = CalculateRitimbaStrength(),
                LeftotanStrength = CalculateLeftotoStrength()
            };

            return warStats;
        }

        /// <summary>
        ///     Executes and calculates the outcome for the war scenario between the Ritimba and Leftoto.
        /// </summary>
        /// <param name="warStats">The stats required for the war to calculate who wins.</param>
        /// <returns><c>true</c> if Ritimba wins the war; otherwise, <c>false</c>.</returns>
        public bool ExecuteWar(WarStats warStats)
        {
            Random random = new Random();
            int number = random.Next(0, 3);
            int modifiedLeftotanStrength = warStats.LeftotanStrength + number - 1;

            if (warStats.RitimbanStrength > modifiedLeftotanStrength)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Attempts to assign a revolt group in a scenario of revolution.
        /// </summary>
        /// <returns><c>true</c> if one of the groups becomes a group responsible for initiating a revolution; otherwise, <c>false</c>.</returns>
        public bool TryTriggerRevoltGroup()
        {
            Random random = new Random();

            for (int guess = 0; guess < 3; guess++)     // Perform 3 tries to guess the revolt group
            {
                int number = random.Next(0, 3);
                Group[] groups = groupStats.GetGroups();

                if (groups[number].Status == GroupStatus.Revolution)
                {
                    revolutionService.SetRevolutionaryGroup(groups[number]);  // As the group has been triggered, set the group as the current revolutionary
                    return true;
                }
            }

            return false;
        }

        public void InitialiseRevolution()
        {
            Dictionary<int, Group> possibleAllies = FindPossibleAllies();

            // TODO: Determine the player combined strength

            // TODO: Determine the current revolutionary combined strength
        }

        public void PunishRevolutionaries()
        {
            revolutionService.PunishRevolutionaries();
        }

        public void ApplyRevolutionCrushedEffects()
        {
            revolutionService.BoostAllyPopularity();
            governmentStats.PlotBonus = governmentStats.Month + 2;  // Prevent revolutions for the next two months
            groupStats.ResetStatusAndAllies();
            // TODO: reset player's ally and revolution properties?

        }

        /// <summary>
        ///     Purchases an helicopter for a possible escape in revolution or war scenarios.
        /// </summary>
        public void PurchasedHelicopter()
        {
            governmentStats.PurchaseHelicopter();
        }

        /// <summary>
        ///     Determines if the player has purchased an helicopter.
        /// </summary>
        /// <returns></returns>
        public bool HasPlayerPurchasedHelicopter()
        {
            return governmentStats.HasHelicopter;
        }

        /// <summary>
        ///     Determines if the player is able to escape by helicopter. There is a 1/4 chances that the helicopter won't start.
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerAbleToEscapeByHelicopter()
        {
            Random random = new Random();
            int number = random.Next(0, 4);

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

        public int GetHighscore()
        {
            var score = scoreService.GetCurrentHighscore();

            return score;
        }

        public Score GetCurrentScore()
        {
            var score = scoreService.GetCurrentScore();

            return score;
        }

        public void SaveHighScore()
        {
            scoreService.SaveHighScore();
        }

        /// <summary>
        ///     Determines if the popularity with the secret police is less or equal to the minimum required monthly popularity.
        /// </summary>
        /// <returns><c>true</c> if the player is not popular enough with the secret police; otherwise, <c>false</c>.</returns>
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
        ///     Calculates the amount of a loan based on the popularity of a group and a random component.
        /// </summary>
        /// <param name="group">The group from which the popularity will be used as a component to calculate the amount.</param>
        /// <returns>The calculated amount of the loan.</returns>
        private int CalculateLoanAmount(Group group)
        {
            Random random = new Random();
            int randomAdditionalAmount = random.Next(0, 200);
            int amount = group.Popularity * 30 + randomAdditionalAmount;

            return amount;
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

        private bool HasEnoughPopularityWithPolice()
        {
            int policePopularity = groupStats.GetPopularityByGroupType(GroupType.SecretPolice);

            return policePopularity > governmentStats.MonthlyMinimalPopularityAndStrength;
        }

        private Dictionary<int, Group> FindPossibleAllies()
        {
            Group[] groups = groupStats.GetGroups();
            Dictionary<int, Group> possibleAllies = new Dictionary<int, Group>();

            for (int i = 0; i < 6; i++)
            {
                if (groups[i].Popularity > governmentStats.MonthlyMinimalPopularityAndStrength)
                {
                    possibleAllies.Add(i, groups[i]);
                }
            }

            return possibleAllies;
        }

        private bool HasPoliceEnoughStrength()
        {
            int policeStrength = groupStats.GetStrengthByGroupType(GroupType.SecretPolice);

            return policeStrength > governmentStats.MonthlyMinimalPopularityAndStrength;
        }

        /// <summary>
        ///     Calculates the strength of the Ritimba republic in a scenario of war.
        /// </summary>
        /// <returns>The total strength of Ritimba in a scenario of war.</returns>
        private int CalculateRitimbaStrength()
        {
            int totalStrength = 0;
            Group[] groups = groupStats.GetGroups();

            // Sum the strength of the army, peasants and landowners if they have minimal popularity
            for (int i = 0; i < 3; i++)
            {
                if (groups[i].Popularity > governmentStats.MonthlyMinimalPopularityAndStrength)
                {
                    totalStrength += groups[i].Strength;
                }
            }

            Group secretPoliceGroup = groupStats.GetGroupByType(GroupType.SecretPolice);

            // Add the strength of the secret police strength if they have minimal popularity
            if (secretPoliceGroup.Popularity > governmentStats.MonthlyMinimalPopularityAndStrength)
            {
                totalStrength += secretPoliceGroup.Strength;
            }
                
            // Add the strength of the player to the total
            totalStrength += governmentStats.PlayerStrength;

            return totalStrength;
        }

        /// <summary>
        ///     Calculates the strength of Leftoto in a scenario of war.
        /// </summary>
        /// <returns>The total strength of Leftoto.</returns>
        public int CalculateLeftotoStrength()
        {
            int totalStrength = 0;
            Group[] groups = groupStats.GetGroups();

            // Add the strength of all groups except Russians and Americans that are equal or below the minimal popularity
            for(int i = 0; i < 6; i++)
            {
                if (groups[i].Popularity <= governmentStats.MonthlyMinimalPopularityAndStrength)
                {
                    totalStrength += groups[i].Strength;
                }
            }

            return totalStrength;
        }
    }
}
