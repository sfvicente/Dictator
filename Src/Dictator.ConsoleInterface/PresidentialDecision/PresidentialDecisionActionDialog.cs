using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class PresidentialDecisionActionDialog : IPresidentialDecisionActionDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public PresidentialDecisionActionDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show()
        {
            return pressAnyKeyWithYesControl.Show();
        }
    }
}
