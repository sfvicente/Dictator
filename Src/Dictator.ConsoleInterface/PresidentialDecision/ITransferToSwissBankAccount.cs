using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface ITransferToSwissBankAccount
    {
        public void Show(int amountStolen, int treasuryBalance);
    }
}
