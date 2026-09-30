namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Helpers for texts that are not translatable.
/// </summary>
internal class SHNotTranslateAble
{
    /// <summary>
    /// Decodes the slash-encoded string.
    /// </summary>
    internal static string DecodeSlashEncodedString(string value)
    {
        // was added ; after 1,2 line and  after 2,3
        // keep as was writte
        value = SHReplace.ReplaceAll(value, "\\", "\\\\");
        value = SHReplace.ReplaceAll(value, "\"", "\\\"");
        value = SHReplace.ReplaceAll(value, "\'", "\\\'");
        return value;
    }
}