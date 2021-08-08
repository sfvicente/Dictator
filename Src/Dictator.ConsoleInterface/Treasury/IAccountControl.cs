using Dictator.Core;

namespace Dictator.ConsoleInterface.Treasury
{
    public interface IAccountControl
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="account">The account information regarding the treasury, monthly costs and Swiss account.</param>
        public void Show(Account account);
    }
}
