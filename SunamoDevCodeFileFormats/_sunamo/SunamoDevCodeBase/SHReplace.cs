namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Replacing in strings.
/// </summary>
internal class SHReplace
{

    /// <summary>
    /// Replaces all search values with the replacement.
    /// </summary>
    internal static string ReplaceAll(string text, string replacement, params string[] searchValues)
    {
        foreach (var item in searchValues)
        {
            if (string.IsNullOrEmpty(item))
            {
                return text;
            }
        }

        foreach (var item in searchValues)
        {
            text = text.Replace(item, replacement);
        }
        return text;
    }

    /// <summary>
    /// Replaces only the first occurrence of the pattern.
    /// </summary>
    internal static string ReplaceOnce(string input, string pattern, string replacement)
    {
        return new Regex(pattern).Replace(input, replacement, 1);
    }
}
