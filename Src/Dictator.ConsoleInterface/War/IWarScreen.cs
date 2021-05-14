using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public interface IWarScreen
{
        public void Show(WarStats warStats);
    }
}
