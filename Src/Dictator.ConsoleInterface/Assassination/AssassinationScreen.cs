using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Assassination;

/// <summary>
///     Represents the screen that is displayed when an assassination attempt by one of the groups 
///     against the player happens.
/// </summary>
public interface IAssassinationScreen
{
    public void Show(string groupName);
}

/// <summary>
///     Represents the screen that is displayed when an assassination attempt by one of the groups 
///     against the player happens.
/// </summary>
public class AssassinationScreen : BaseScreen, IAssassinationScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AssassinationScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public AssassinationScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show(string groupName)
    {
        _consoleService.Clear();
        _consoleService.WriteAt(1, 11, "      ASSASSINATION ATTEMPT     ");
        _consoleService.WriteCenteredAt(12, $"by one of {groupName}");
        _pressAnyKeyControl.Show();
    }
}
