namespace Dictator.Core.Models;

/// <summary>
///     Represents the strength breakdown displayed before a revolution starts.
/// </summary>
public class RevolutionState
{
    public int PlayerStrength { get; set; }
    public string AllyName { get; set; }
    public int AllyStrength { get; set; }
    public int RevolutionaryStrength { get; set; }
}
