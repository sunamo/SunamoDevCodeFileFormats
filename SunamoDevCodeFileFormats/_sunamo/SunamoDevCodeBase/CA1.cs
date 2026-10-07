namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

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
        for (int index = 0; index < list.Count; index++)
        {
            list[index] = prefixText + list[index] + suffixText;
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
        for (int index = 0; index < list.Count; index++)
        {
            if (!list[index].StartsWith(prefix))
            {
                list[index] = prefix + list[index];
            }
        }

        return list;
    }
}
