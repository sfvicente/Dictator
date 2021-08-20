using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Audience
{
    public interface IAudienceDecisionDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <param name="audience">The audience information to display on the screen.</param>
        /// <returns>The option selected after the dialog has been presented.</returns>
        DialogResult Show(Core.Audience audience);
    }
}
