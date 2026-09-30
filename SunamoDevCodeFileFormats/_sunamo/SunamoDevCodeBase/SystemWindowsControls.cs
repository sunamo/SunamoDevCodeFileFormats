namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

// Helper class for System.Windows controls shortcuts and names
/// <summary>
/// Helpers for names of Windows controls.
/// </summary>
internal static class SystemWindowsControls
{
    private static readonly Dictionary<string, List<string>> s_controls = new();

    /// <summary>
    /// Checks whether the text starts with the shortcut of a control.
    /// </summary>
    public static bool StartingWithShortcutOfControl(string text)
    {
        foreach (var item in s_controls)
        foreach (var shortcut in item.Value)
            if (shortcut.Length > 2)
                if (text.StartsWith(shortcut))
                    return true;

        return false;
    }
}
