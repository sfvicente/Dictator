using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AudienceScreen : IAudienceScreen
    {
        public void Show()
        {
            Console.Clear();

            ConsoleEx.WriteAt(24, 3, "           AN AUDIENCE          ");

            ConsoleEx.WriteAt(24, 5, "A request from {Group}");
            ConsoleEx.WriteAt(24, 9, "  Will YOUR EXCELLENCY agree to ");
            ConsoleEx.WriteAt(24, 11, "<petition text>");

            // TODO: Display option to ask for advice

            Console.ReadKey();
        }
    }
}
