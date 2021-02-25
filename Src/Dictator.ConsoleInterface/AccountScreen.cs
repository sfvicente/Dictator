using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AccountScreen
    {
        public IAccount account;

        public AccountScreen(IAccount account)
        {
            this.account = account;
        }
    }
}
