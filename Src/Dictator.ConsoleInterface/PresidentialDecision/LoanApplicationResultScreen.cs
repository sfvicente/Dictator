﻿using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class LoanApplicationResultScreen : ILoanApplicationResultScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public LoanApplicationResultScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(LoanApplicationResult loanApplicationResult)
        {
            if(loanApplicationResult.IsAccepted)
            {
                ConsoleEx.WriteAt(1, 12, $"<groupName> will let you have");
                ConsoleEx.WriteAt(8, 14, $"{loanApplicationResult.Amount},000 DOLLARS");
            }
            else
            {
                // TODO: check country
                ConsoleEx.WriteAt(1, 12, "             NIET !             ");
                ConsoleEx.WriteAt(1, 12, "            \"nuts !\"            ");
            }

            pressAnyKeyControl.Show();
        }
    }
}