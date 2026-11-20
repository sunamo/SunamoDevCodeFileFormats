namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Helpers for working with HTML text.
/// </summary>
internal class HtmlAssistant
{
    /// <summary>
    /// Trims the inner HTML text.
    /// </summary>
    internal static string TrimInnerHtml(string value)
    {
        throw new Exception("Code without CreateHtmlDocument");
    }
    /// <summary>
    /// Decodes HTML entities in the text.
    /// </summary>
    internal static string HtmlDecode(string encodedText)
    {
        return WebUtility.HtmlDecode(encodedText);
    }
}