namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Generator of text output.
/// </summary>
internal class TextOutputGenerator
{
    // EN: During NuGet conversion, I changed this to TextBuilderDC StringBuilder = TextBuilder.Create();
    // but that was probably a mistake, now in _sunamo I have Create() which returns null instead of using ctor
    // so I'm reverting it back.
    internal StringBuilder StringBuilder = new StringBuilder();
    /// <summary>
    /// Returns the generated text.
    /// </summary>
    public override string ToString()
    {
        var result = StringBuilder.ToString();
        return result;
    }
    /// <summary>
    /// Appends the paragraph with the header.
    /// </summary>
    internal void Paragraph(StringBuilder wrongNumberOfParts, string header)
    {
        string text = wrongNumberOfParts.ToString().Trim();
        Paragraph(text, header);
    }

    /// <summary>
    /// Appends the paragraph with the header.
    /// </summary>
    internal void Paragraph(string text, string header)
    {
        if (text != string.Empty)
        {
            StringBuilder.AppendLine(header + ":");
            StringBuilder.AppendLine(text);
            StringBuilder.AppendLine();
        }
    }
}
