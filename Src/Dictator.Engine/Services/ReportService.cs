using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IAccountService accountService;
        private readonly IGroupService groupService;
        private readonly IGovernmentService governmentService;

        public ReportService(IAccountService accountService, IGroupService groupService, IGovernmentService governmentService)
        {
            this.accountService = accountService;
            this.groupService = groupService;
            this.governmentService = governmentService;
        }

        public PoliceReportRequest RequestPoliceReport()
        {
            PoliceReportRequest policeReportRequest = new PoliceReportRequest
            {
                HasEnoughBalance = accountService.GetTreasuryBalance() > 0,
                IsPlayerPopularWithSecretPolice = IsPlayerPopularWithSecretPolice(),
                HasPoliceEnoughStrength = HasPoliceEnoughStrength(),
                PolicePopularity = groupService.GetPopularityByGroupType(GroupType.SecretPolice),
                PoliceStrength = groupService.GetStrengthByGroupType(GroupType.SecretPolice)
            };

            return policeReportRequest;
        }

        /// <summary>
        ///     Retrieves the police report with the current month, revolution strength, player strength and group information.
        /// </summary>
        /// <returns>The police report with the current month, revolution strength, player strength and group information.</returns>
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

        /// <summary>
        ///     Determines if the player is popular with the secret police.
        /// </summary>
        /// <returns><c>true</c> if the player is popular with the secret police; otherwise, <c>false</c>.</returns>
        private bool IsPlayerPopularWithSecretPolice()
        {
            int secretPolicePopularity = groupService.GetPopularityByGroupType(GroupType.SecretPolice);

            return secretPolicePopularity > governmentService.GetMonthlyMinimalPopularityAndStrength();
        }

        /// <summary>
        ///     Determines if the level of the police police strength is greater than the minimal requirement for the current month.
        /// </summary>
        /// <returns><c>true</c> if police has enough; otherwise, <c>false</c>.</returns>
        private bool HasPoliceEnoughStrength()
        {
            int policeStrength = groupService.GetStrengthByGroupType(GroupType.SecretPolice);

            return policeStrength > governmentService.GetMonthlyMinimalPopularityAndStrength();
        }
    }
}
