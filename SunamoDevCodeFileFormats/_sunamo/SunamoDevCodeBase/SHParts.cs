namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Getting parts of strings.
/// </summary>
internal class SHParts
{
    // Metoda KeepAfterFirst byla odstraněna - inlined v TypeScriptHelper.cs:79

    // Metoda RemoveAfterLast byla odstraněna - inlined v SolutionFolderSerialize.cs:57

    /// <summary>
    /// Removes the text after the first occurrence of the searched text.
    /// </summary>
    internal static string RemoveAfterFirst(string text, char searchChar)
    {
        int index = text.IndexOf(searchChar);
        return index == -1 || index == text.Length - 1 ? text : text.Substring(0, index);
    }

}
