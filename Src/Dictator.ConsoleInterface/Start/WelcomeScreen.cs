using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Start
{
    /// <summary>
    ///     Represents the screen that is displayed when welcoming the player to the game.
    /// </summary>
    public interface IWelcomeScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="highscore">The game's current highest score.</param>
        public void Show(int highscore);
    }

    /// <summary>
    ///     Represents the screen that is displayed when welcoming the player to the game.
    /// </summary>
    public class WelcomeScreen : BaseScreen, IWelcomeScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WelcomeScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public WelcomeScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="highscore">The game's current highest score.</param>
        public void Show(int highscore)
        {
            _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.WriteAt(8, 4, "WELCOME to OFFICE", ConsoleColor.Black, ConsoleColor.Cyan);
            _consoleService.WriteAt(1, 7, "The best DICTATOR of our beloved");
            _consoleService.WriteAt(1, 9, "country of RITIMBA had a final  ");
            _consoleService.WriteAt(1, 11, $"rating of {highscore}");

            if (highscore > 0)
            {
                _consoleService.WriteAt(1, 14, $"You can always try for {highscore + 1} !");
            }
            else
            {
                _consoleService.WriteAt(1, 14, "As this is your first attempt   ");
                _consoleService.WriteAt(1, 16, "you will no doubt do BETTER !   ");
            }

            _consoleService.WriteAt(1, 18, "Start with a TREASURY REPORT    ");
            _consoleService.WriteAt(1, 20, "and a POLICE Report. (FREE)     ");          
            pressAnyKeyControl.Show();
        }
    }
}
