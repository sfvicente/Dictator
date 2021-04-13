using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Audience
{
    public interface IAudienceDecisionDialog
    {
        DialogResult Show(Core.Audience audience);
    }
}
