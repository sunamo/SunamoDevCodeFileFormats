namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Arguments for removing translation units with empty target or source.
/// </summary>
public class RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs
{
    /// <summary>
    /// Gets or sets the default.
    /// </summary>
    public static RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs Default { get; set; } = new RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs();

    /// <summary>
    /// Gets or sets the remove whole trans unit.
    /// </summary>
    public bool RemoveWholeTransUnit { get; set; } = true;

    /// <summary>
    /// Gets or sets the save.
    /// </summary>
    public bool Save { get; set; } = true;
}
