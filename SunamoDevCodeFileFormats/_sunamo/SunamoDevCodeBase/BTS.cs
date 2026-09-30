namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Conversions of bool to string.
/// </summary>
internal class BTS
{

    private const string Yes = "Yes";
    private const string No = "No";

    /// <summary>
    /// Converts bool to text, optionally lower-cased.
    /// </summary>
    internal static string BoolToString(bool value, bool isLowerCase = false)
    {
        string result = value ? Yes : No;

        if (isLowerCase)
        {
            return result.ToLower();
        }
        return result;
    }
}
