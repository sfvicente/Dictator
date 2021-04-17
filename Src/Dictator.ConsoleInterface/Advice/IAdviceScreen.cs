using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Advice
{
    public interface IAdviceScreen
    {
        public void Show(GameAction gameAction);
    }
}
