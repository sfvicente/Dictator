using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Advice
{
    public interface IAdviceScreen
    {
        public void Show(Core.Audience audience);
        public void Show(Decision decision);
    }
}
