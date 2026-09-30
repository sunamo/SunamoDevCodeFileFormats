namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Splitting of strings.
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// Splits the text by the delimiters.
    /// </summary>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    /// <summary>
    /// Splits input in the replace-many format to two parts.
    /// </summary>
    internal static Tuple<string, string> SplitFromReplaceManyFormat(string input)
    {
        StringBuilder to = new StringBuilder();
        StringBuilder from = new StringBuilder();

        if (input.Contains("->"))
        {
            var lines = SHGetLines.GetLines(input);

            lines = lines.ConvertAll(line => line.Trim());

            foreach (var item in lines)
            {
                var parts = SHSplit.Split(item, "->");
                from.AppendLine(parts[0]);
                to.AppendLine(parts[1]);
            }
        }
        else
        {
            from.AppendLine(input);
        }

        return new Tuple<string, string>(from.ToString(), to.ToString());
    }

    /// <summary>
    /// Splits input in the replace-many format to two lists.
    /// </summary>
    internal static Tuple<List<string>, List<string>> SplitFromReplaceManyFormatList(string input)
    {
        var temp = SplitFromReplaceManyFormat(input);
        return new Tuple<List<string>, List<string>>(SHGetLines.GetLines(temp.Item1), SHGetLines.GetLines(temp.Item2));
    }
}
