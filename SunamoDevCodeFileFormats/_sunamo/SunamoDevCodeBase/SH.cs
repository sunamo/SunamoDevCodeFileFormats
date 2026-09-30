namespace SunamoDevCodeFileFormats._sunamo;

/// <summary>
/// Helpers for working with strings.
/// </summary>
internal class SH
{

    /// <summary>
    /// Returns the line on which the character index is.
    /// </summary>
    internal static string GetLineFromCharIndex(string content, List<string> lines, int characterIndex)
    {
        var lineIndex = GetLineIndexFromCharIndex(content, characterIndex);
        return lines[lineIndex];
    }

    // EN: Return index, therefore x-1
    // CZ: Vrátí index, proto x-1
    /// <summary>
    /// Returns index of the line on which the character index is.
    /// </summary>
    internal static int GetLineIndexFromCharIndex(string text, int characterPosition)
    {
        var lineNumber = text.Take(characterPosition).Count(character => character == '\n') + 1;
        return lineNumber - 1;
    }

    /// <summary>
    /// Returns the text between the delimiters.
    /// </summary>
    internal static string GetTextBetweenSimple(string text, string afterDelimiter, string beforeDelimiter, bool isThrowExceptionIfNotContains = true)
    {
        int foundIndex = int.MinValue;
        var result = GetTextBetween(text, afterDelimiter, beforeDelimiter, out foundIndex, 0, isThrowExceptionIfNotContains);
        return result!;
    }

    /// <summary>
    /// Returns the text between the delimiters or null.
    /// </summary>
    internal static string? GetTextBetween(string text, string afterDelimiter, string beforeDelimiter, out int foundIndex, int startSearchingAt, bool isThrowExceptionIfNotContains = true)
    {
        string? result = null;
        foundIndex = text.IndexOf(afterDelimiter, startSearchingAt);
        int beforeIndex = text.IndexOf(beforeDelimiter, foundIndex + afterDelimiter.Length);
        bool isAfterFound = foundIndex != -1;
        bool isBeforeFound = beforeIndex != -1;
        if (isAfterFound && isBeforeFound)
        {
            foundIndex += afterDelimiter.Length;
            beforeIndex -= 1;
            // When I return between ( ), there must be +1
            var length = beforeIndex - foundIndex + 1;
            if (length < 1)
            {
                // EN: This was here before but logically it's nonsense
                // CZ: Takhle to tu bylo předtím ale logicky je to nesmysl
            }
            result = text.Substring(foundIndex, length).Trim();
        }
        else
        {
            if (isThrowExceptionIfNotContains)
            {
                ThrowEx.NotContains(text, afterDelimiter, beforeDelimiter);
            }
            else
            {
                // 24-1-21 return null instead of text
                return null;
                //result = text;
            }
        }

        return result;
    }

    /// <summary>
    /// Wraps the value with quotation marks.
    /// </summary>
    internal static string WrapWithQm(string value)
    {
        var wrapper = "\"";
        return wrapper + value + wrapper;
    }


    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    internal static (string, string) GetPartsByLocationNoOut(string text, char delimiter)
    {
        GetPartsByLocation(out var before, out var after, text, delimiter);
        return (before, after);
    }

    /// <summary>
    /// Splits the text by the delimiter to the parts before and after.
    /// </summary>
    internal static void GetPartsByLocation(out string before, out string after, string text, char delimiter)
    {
        int index = text.IndexOf(delimiter);
        GetPartsByLocation(out before, out after, text, index);
    }

    /// <summary>
    /// Splits the text by the delimiter to the parts before and after.
    /// </summary>
    internal static void GetPartsByLocation(out string before, out string after, string text, int position)
    {
        if (position == -1)
        {
            before = text;
            after = "";
        }
        else
        {
            before = text.Substring(0, position);
            if (text.Length > position + 1)
            {
                after = text.Substring(position + 1);
            }
            else
            {
                after = string.Empty;
            }
        }
    }

    /// <summary>
    /// Returns indexes of all occurrences of the searched text.
    /// </summary>
    internal static List<int> ReturnOccurencesOfString(string text, string searchText)
    {

        List<int> results = new();
        for (int index = 0; index < (text.Length - searchText.Length) + 1; index++)
        {
            var substring = text.Substring(index, searchText.Length);
            ////////DebugLogger.Instance.WriteLine(substring);
            // non-breaking space. &nbsp; code 160
            // 32 space
            _ = substring[0];
            _ = searchText[0];
            if (substring == "")
            {
            }
            if (substring == searchText)
                results.Add(index);
        }
        return results;
    }

    /// <summary>
    /// Checks whether the text contains diacritics.
    /// </summary>
    internal static bool ContainsDiacritic(string target)
    {
        return target.HasDiacritics();
    }
}
