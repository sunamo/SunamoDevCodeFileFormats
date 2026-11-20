namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Helpers for working with XML.
/// </summary>
internal partial class XHelper
{
    internal static 
    async Task<string>
    FormatXml(string pathOrContent)
    {
        var xmlFormat = pathOrContent;
        if (File.Exists(pathOrContent))
        {
            xmlFormat = 
            await
            FileAsync.ReadAllTextAsync(pathOrContent);
        }

        XmlNamespacesHolder namespacesHolder = new XmlNamespacesHolder();
        XDocument doc = namespacesHolder.ParseAndRemoveNamespacesXDocument(xmlFormat);
        var formatted = doc.ToString();
        formatted = formatted.Replace(" xmlns=\"\"", string.Empty);
        if (File.Exists(pathOrContent))
        {
            await
            FileAsync.WriteAllTextAsync(pathOrContent, formatted);
            return null!;
        }
        else
        {
            return formatted;
        }
    }
}
