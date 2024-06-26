﻿namespace Dictator.Core.Models;

/// <summary>
///     Represents a group element responsible for starting a revolution.
/// </summary>
public class Revolutionary
{
    /// <summary>
    ///     Gets or sets the revolutionary group name.
    /// </summary>
    public string RevolutionaryGroupName { get; set; }

    /// <summary>
    ///     Gets or sets the revolutionary group ally name.
    /// </summary>
    public string RevolutionaryGroupAllyName { get; set; }

    /// <summary>
    ///     Gets or sets the combined strength of the revolutionary group and their ally.
    /// </summary>
    public int CombinedStrength { get; set; }
}
