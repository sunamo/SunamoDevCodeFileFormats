namespace SunamoDevCodeFileFormats._sunamo;

/// <summary>
/// Helpers for working with collections.
/// </summary>
internal partial class CA
{

    /// <summary>
    /// Wraps the value(s) with the given prefix and suffix.
    /// </summary>
    internal static List<string> WrapWith(List<string> list, string wrapText)
        => WrapWith(list, wrapText, wrapText);

    /// <summary>
    /// Wraps the value(s) with the given prefix and suffix.
    /// </summary>
    internal static List<string> WrapWith(List<string> list, string prefixText, string suffixText)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = prefixText + list[i] + suffixText;
        }

        return list;
    }

    /// <summary>
    /// Checks whether the key ends with any of the suffixes.
    /// </summary>
    internal static bool HasPostfix(string key, params string[] suffixes)
    {
        foreach (var item in suffixes)
        {
            if (key.EndsWith(item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Prepends the prefix to every element of the list.
    /// </summary>
    internal static List<string> Prepend(string prefix, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].StartsWith(prefix))
            {
                list[i] = prefix + list[i];
            }
        }

        return list;
    }
}
