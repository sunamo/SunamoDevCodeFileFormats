namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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
        //HReplace.ReplaceAll2(formatted, string.Empty, " xmlns=\"\"");
        if (File.Exists(pathOrContent))
        {
            await
            FileAsync.WriteAllTextAsync(pathOrContent, formatted);
            //ThisApp.Success(Translate.FromKey(XlfKeys.ChangesSavedToFile));
            return null!;
        }
        else
        {
            //ThisApp.Success(Translate.FromKey(XlfKeys.ChangesSavedToClipboard));
            return formatted;
        }
    }
}
