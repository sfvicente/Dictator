using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface ITransferToSwissBankAccountScreen
    {
        public void Show(SwissBankAccountTransfer swissBankAccountTransfer, Account account);
    }
}
