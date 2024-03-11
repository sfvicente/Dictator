namespace Dictator.Core.Services;

public interface IEscapeService
{
    /// <summary>
    ///     Determines if the player is able to escape after the war is lost. There is a 2/3 chances that the player is able to escape.
    /// </summary>
    /// <returns><c>true</c> if manages to escape after losing the war; otherwise, <c>false</c>.</returns>
    public bool IsPlayerAbleToEscapeAfterLosingWar();

    /// <summary>
    ///     Determines if the player is able to escape by helicopter. There is a 1/4 chances that the helicopter won't start.
    /// </summary>
    /// <returns><c>true</c> if the player is able to escape by helicopter; otherwise, <c>false</c>.</returns>
    public bool IsPlayerAbleToEscapeByHelicopter();

    /// <summary>
    ///     Checks if the player is captured by the guerrillas when attempting to escape through leftoto.
    /// </summary>
    /// <returns><c>true</c> if the guerrilas capture the player while attempting escape; otherwise, <c>false</c>.</returns>
    public bool DoesGuerrillaCatchPlayerEscaping();
}

public class EscapeService : IEscapeService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;

    public EscapeService(
        IRandomService randomService,
        IGroupService groupService)
    {
        _randomService = randomService;
        _groupService = groupService;
    }

    /// <summary>
    ///     Determines if the player is able to escape after the war is lost. There is a 2/3 chances that the player is able to escape.
    /// </summary>
    /// <returns><c>true</c> if manages to escape after losing the war; otherwise, <c>false</c>.</returns>
    public bool IsPlayerAbleToEscapeAfterLosingWar()
    {
        int number = _randomService.Next(3);

        // The player has a 2 in 3 chances that he manages to escape
        if (number < 2)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Determines if the player is able to escape by helicopter. There is a 1/4 chances that the helicopter won't start.
    /// </summary>
    /// <returns><c>true</c> if the player is able to escape by helicopter; otherwise, <c>false</c>.</returns>
    public bool IsPlayerAbleToEscapeByHelicopter()
    {
        int number = _randomService.Next(4);

        // The player has a 1 in 4 chances that the helicopter won't start
        if (number != 0)
        {
            // The escape by helicopter is successfull
            return true;
        }

        // The escape by helicopter fails as the helicopter won't start
        return false;
    }

    /// <summary>
    ///     Checks if the player is captured by the guerrillas when attempting to escape through leftoto.
    /// </summary>
    /// <returns><c>true</c> if the guerrilas capture the player while attempting escape; otherwise, <c>false</c>.</returns>
    public bool DoesGuerrillaCatchPlayerEscaping()
    {
        int guerrilasStrength = _groupService.GetGroupByType(GroupType.Guerillas).Strength;
        int upperLimit = (guerrilasStrength / 3) + 2;
        int number = _randomService.Next(0, upperLimit);

        // There is a chance of 1 in 2..5 that player is captured, which depends on the guerrilas strength
        if (number == 0)
        {
            return true;
        }

        return false;
    }
}
