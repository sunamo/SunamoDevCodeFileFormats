namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Arguments for removing translation units with empty target or source.
/// </summary>
public class RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs
{
    public static RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs Default { get; set; } = new RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs();

    public bool RemoveWholeTransUnit { get; set; } = true;

    public bool Save { get; set; } = true;
}
