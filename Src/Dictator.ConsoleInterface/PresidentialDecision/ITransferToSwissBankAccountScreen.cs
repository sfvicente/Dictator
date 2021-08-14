using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface ITransferToSwissBankAccountScreen
    {
        public void Show(SwissBankAccountTransfer swissBankAccountTransfer, Account account);
    }
}
