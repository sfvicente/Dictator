using Dictator.Common;
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
        private readonly IGovernmentService governmentService;
        private readonly IGroupService groupService;
        private readonly IPlotService plotService;
        private readonly IDecisionService decisionService;
        private readonly IAudienceService audienceService;
        private readonly INewsService newsService;
        private readonly IRevolutionService revolutionService;
        private readonly IScoreService scoreService;
        private readonly IEscapeService escapeService;
        private readonly ILoanService loanService;
        private readonly IWarService warService;

        public Engine(
            IAccountService accountService,
            IGovernmentService governmentService,
            IGroupService groupService,
            IPlotService plotService,
            IDecisionService decisionService,
            IAudienceService audienceService,
            INewsService newsService,
            IRevolutionService revolutionService,
            IScoreService scoreService,
            IEscapeService escapeService,
            ILoanService loanService,
            IWarService warService)
        {
            this.accountService = accountService;
            this.governmentService = governmentService;
            this.groupService = groupService;
            this.plotService = plotService;
            this.decisionService = decisionService;
            this.audienceService = audienceService;
            this.newsService = newsService;
            this.revolutionService = revolutionService;
            this.scoreService = scoreService;
            this.escapeService = escapeService;
            this.loanService = loanService;
            this.warService = warService;
        }

        public void Initialise()
        {
            accountService.Initialise();
            governmentService.Initialise();
            groupService.Initialise();
            audienceService.Initialise();
            decisionService.Initialise();
            newsService.Initialise();
        }

        public Account GetAccount()
        {
            return accountService.GetAccount();
        }

        public void SetGroupStrength(GroupType groupType, int strength)
        {
            groupService.SetStrength(groupType, strength);
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
                IsPlayerPopularWithSecretPolice = IsPlayerPopularWithSecretPolice(),
                HasPoliceEnoughStrength = HasPoliceEnoughStrength(),
                PolicePopularity = groupService.GetPopularityByGroupType(GroupType.SecretPolice),
                PoliceStrength = groupService.GetStrengthByGroupType(GroupType.SecretPolice)
            };

            return policeReportRequest;
        }

        public PoliceReport GetPoliceReport()
        {
            PoliceReport policeReport = new PoliceReport
            {
                Month = governmentService.GetMonth(),
                Groups = groupService.GetGroups().AsReadOnly(),
                PlayerStrength = governmentService.GetPlayerStrength(),
                MonthlyRevolutionStrength = governmentService.GetMonthlyRevolutionStrength()
            };

            return policeReport;
        }

        public void AdvanceMonth()
        {
            governmentService.AdvanceMonth();
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
            return governmentService.GetMonth();
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

            governmentService.SetMonthlyMinimalPopularityAndStrength(random.Next(2, 5));
        }

        public void SetMonthlyRevolutionStrength()
        {
            Random random = new Random();

            governmentService.SetMonthlyRevolutionStrength(random.Next(10, 13));
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
            return newsService.ShouldNewsHappen();
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

        public News SelectRandomUnusedNews()
        {
            return newsService.SelectRandomUnusedNews();
        }

        public void ApplyNewsEffects(News news)
        {
            if(news == null)
            {
                throw new ArgumentNullException();
            }

            groupService.ApplyPopularityChange(news.GroupPopularityChanges);
            groupService.ApplyStrengthChange(news.GroupStrengthChanges);
            accountService.ApplyTreasuryChanges(news.Cost, news.MonthlyCost);
        }

        public Audience SelectRandomUnusedAudienceRequest()
        {
            return audienceService.SelectRandomUnusedAudienceRequest();
        }

        /// <summary>
        ///     Accepts an audience request with the associated modifications to group popularity and strength and also the changes to the treasury.
        /// </summary>
        public void AcceptAudienceRequest(Audience audience)
        {
            audienceService.AcceptAudienceRequest(audience);
        }

        /// <summary>
        ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
        /// </summary>
        public void RefuseAudienceRequest(Audience audience)
        {
            audienceService.RefuseAudienceRequest(audience);
        }

        public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber)
        {
            Decision[] decisions = decisionService.GetDecisionsByType(decisionType);

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
            Decision[] decisions = decisionService.GetDecisionsByType(decisionType);

            return decisions;
        }

        public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionNumber)
        {
            if (optionNumber < 0)
            {
                throw new ArgumentException(nameof(optionNumber));
            }

            Decision[] decisions = decisionService.GetDecisionsByType(decisionType);

            if (optionNumber > decisions.Length)
            {
                throw new ArgumentException(nameof(optionNumber));
            }

            return decisions[optionNumber - 1];
        }

        /// <summary>
        ///     Increases bodyguard, resulting in an increment of the player's strength by 2.
        /// </summary>
        public void IncreaseBodyguard()
        {
            governmentService.IncreaseBodyguard();
        }

        /// <summary>
        ///     Asks for foreign aid in the form of a loan to either America or Russia.
        /// </summary>
        /// <param name="country">The country to which the loan request will be made.</param>
        /// <returns>The loan application result that includes if the loan has been approved or refused.</returns>
        public LoanApplicationResult AskForLoan(Country country)
        {
            return loanService.AskForLoan(country);
        }

        /// <summary>
        ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
        /// </summary>
        /// <param name="decision">The decision whose effects will be apply.</param>
        public void ApplyDecisionEffects(Decision decision)
        {
            decisionService.ApplyDecisionEffects(decision);
        }

        /// <summary>
        ///     Marks a presidential decision option as used.
        /// </summary>
        /// <param name="text">The text of the presidential decision which will be marked as used.</param>
        public void MarkDecisionAsUsed(string text)
        {
            decisionService.MarkDecisionAsUsed(text);
        }

        /// <summary>
        ///     Determines if an assassination attempt should be performed by one of the following groups: army, 
        ///     peasants, landowners and guerrilas.
        /// </summary>
        /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldAssassinationAttemptHappen()
        {
            return groupService.ShouldAssassinationAttemptHappen();
        }

        /// <summary>
        ///     Retrieves the name of the group responsible for the assassination attempt.
        /// </summary>
        /// <returns>The name of the assassination group.</returns>
        public string GetAssassinationGroupName()
        {
            GroupType assassinGroupType = groupService.AssassinGroupType;
            string groupName = groupService.GetGroupByType(assassinGroupType).Name;

            return groupName;
        }

        /// <summary>
        ///     Determines if there is conflict between the republic of Ritimba and Leftoto. A conflict does not exist if the player is popular
        ///     with the leftotans or leftotans are weak.
        /// </summary>
        /// <returns></returns>
        public bool DoesConflictExist()
        {
            Group leftotans = groupService.GetGroupByType(GroupType.Leftotans);

            if(leftotans.Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength() ||
                leftotans.Strength < governmentService.GetMonthlyMinimalPopularityAndStrength())
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
            return warService.ShouldWarHappen();
        }

        /// <summary>
        ///     Applies the effects of a threat of war with leftoto, which results in an increase of the player's popularity amongst the army,
        ///     peasants, landowners and secret police.
        /// </summary>
        public void ApplyThreatOfWarEffects()
        {
            warService.ApplyThreatOfWarEffects();
        }

        /// <summary>
        ///     Determines if an assassination attempt on the player is successful.
        /// </summary>
        /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
        public bool IsAssassinationSuccessful()
        {
            Random random = new Random();
            int number = random.Next(0, 2);

            if (groupService.DoesMainPopulationHatePlayer() ||
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
            return revolutionService.TryTriggerRevoltGroup();
        }

        public Dictionary<int, Group> FindPossibleAlliesForPlayer()
        {
            return revolutionService.FindPossibleAllies();
        }

        /// <summary>
        ///     Determines if a group accepts to be an ally of the player during a revolution.
        /// </summary>
        /// <param name="groupId">The id of the group to accept or refuse an alliance.</param>
        /// <returns><c>true</c> if the group accepts to be an ally; otherwise, <c>false</c>.</returns>
        public bool DoesGroupAcceptAllianceInRevolution(int groupId)
        {
            return revolutionService.DoesGroupAcceptAllianceInRevolution(groupId);
        }

        public void SetPlayerAllyForRevolution(int selectedAllyGroupId)
        {
            revolutionService.SetPlayerAllyForRevolution(selectedAllyGroupId);
        }

        /// <summary>
        ///     Determines if a revolution has succeeded in overthrowing the player.
        /// </summary>
        /// <returns><c>true</c> if the revolution has succeeded; otherwise, <c>false</c>.</returns>
        public bool DoesRevolutionSucceed()
        {
            return revolutionService.DoesRevolutionSucceed();
        }

        public void PunishRevolutionaries()
        {
            revolutionService.PunishRevolutionaries();
        }

        public void ApplyRevolutionCrushedEffects()
        {
            revolutionService.ApplyRevolutionCrushedEffects();
        }

        /// <summary>
        ///     Purchases an helicopter for a possible escape in revolution or war scenarios.
        /// </summary>
        public void PurchasedHelicopter()
        {
            governmentService.PurchaseHelicopter();
        }

        /// <summary>
        ///     Determines if the player has purchased an helicopter.
        /// </summary>
        /// <returns></returns>
        public bool HasPlayerPurchasedHelicopter()
        {
            return governmentService.HasPlayerPurchasedHelicopter();
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
        /// <returns><c>true</c> if manages to escape after losing the war; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAbleToEscapeAfterLosingWar()
        {
            return escapeService.IsPlayerAbleToEscapeAfterLosingWar();
        }

        /// <summary>
        ///     Checks if the player is captured by the guerrillas when attempting to escape through leftoto.
        /// </summary>
        /// <returns><c>true</c> if the guerrilas capture the player while attempting escape; otherwise, <c>false</c>.</returns>
        public bool DoesGuerrillaCatchPlayerEscaping()
        {
            int guerrilasStrength = groupService.GetGroupByType(GroupType.Guerillas).Strength;
            int upperLimit = (guerrilasStrength / 3) + 2;

            Random random = new Random();
            int number = random.Next(0, upperLimit);

            // There is a chance of 1 in 2..5 that player is captured, which depends on the guerrilas strength
            if (number == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Marks the player as dead.
        /// </summary>
        public void KillPlayer()
        {
            governmentService.KillPlayer();
        }

        /// <summary>
        ///     Retrieves the current highest score of the game.
        /// </summary>
        /// <returns>The high-score value.</returns>
        public int GetHighscore()
        {
            return scoreService.GetCurrentHighscore();
        }

        /// <summary>
        ///     Retrieves the score of the current game state.
        /// </summary>
        /// <returns>The score value.</returns>
        public Score GetCurrentScore()
        {
            return scoreService.GetCurrentScore();
        }

        /// <summary>
        ///     Stores the score of the current game state as the highest score.
        /// </summary>
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
            if (groupService.GetGroupByType(GroupType.SecretPolice).Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                return true;
            }

            return false;
        }

        private bool IsPoliceUnableToProtectPlayer()
        {
            if (groupService.GetGroupByType(GroupType.SecretPolice).Strength <= governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Applies the bankruptcy state effects which consists of a decrease in popularity with the Army and Secret Police and also decrease the strength
        ///     of the Player and the Secret Police.
        /// </summary>
        public void ApplyBankruptcyEffects()
        {
            groupService.DecreasePopularity(GroupType.Army, 1);
            groupService.DecreasePopularity(GroupType.SecretPolice, 1);
            groupService.DecreaseStrength(GroupType.SecretPolice);
            governmentService.DecreasePlayerStrength();
        }

        /// <summary>
        ///     Determines if the player is popular with the secret police.
        /// </summary>
        /// <returns><c>true</c> if the player is popular with the secret police; otherwise, <c>false</c>.</returns>
        private bool IsPlayerPopularWithSecretPolice()
        {
            int secretPolicePopularity = groupService.GetPopularityByGroupType(GroupType.SecretPolice);

            return secretPolicePopularity > governmentService.GetMonthlyMinimalPopularityAndStrength();
        }

        private bool HasPoliceEnoughStrength()
        {
            int policeStrength = groupService.GetStrengthByGroupType(GroupType.SecretPolice);

            return policeStrength > governmentService.GetMonthlyMinimalPopularityAndStrength();
        }

        /// <summary>
        ///     Calculates the strength of the Ritimba republic in a scenario of war.
        /// </summary>
        /// <returns>The total strength of Ritimba in a scenario of war.</returns>
        private int CalculateRitimbaStrength()
        {
            int totalStrength = 0;
            Group[] groups = groupService.GetGroups();

            // Sum the strength of the army, peasants and landowners if they have minimal popularity
            for (int i = 0; i < 3; i++)
            {
                if (groups[i].Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength())
                {
                    totalStrength += groups[i].Strength;
                }
            }

            Group secretPoliceGroup = groupService.GetGroupByType(GroupType.SecretPolice);

            // Add the strength of the secret police strength if they have minimal popularity
            if (secretPoliceGroup.Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                totalStrength += secretPoliceGroup.Strength;
            }
                
            // Add the strength of the player to the total
            totalStrength += governmentService.GetPlayerStrength();

            return totalStrength;
        }

        /// <summary>
        ///     Calculates the strength of Leftoto in a scenario of war.
        /// </summary>
        /// <returns>The total strength of Leftoto.</returns>
        private int CalculateLeftotoStrength()
        {
            int totalStrength = 0;
            Group[] groups = groupService.GetGroups();

            // Add the strength of all groups except Russians and Americans that are equal or below the minimal popularity
            for(int i = 0; i < 6; i++)
            {
                if (groups[i].Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
                {
                    totalStrength += groups[i].Strength;
                }
            }

            return totalStrength;
        }
    }
}
