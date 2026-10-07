namespace SunamoDevCode.FileFormats;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public static partial class XmlLocalisationInterchangeFileFormat
{
    // Into A1 insert XlfResourcesH.PathToXlfSunamo
    // Completely IUN
    // Remove completely whole Trans-unit
    public static
        async Task<string>
    RemoveFromXlfWhichHaveEmptyTargetOrSource(string fileName, XlfParts xlfParts, RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs? args = null)
    {
        if (args == null)
        {
            args = RemoveFromXlfWhichHaveEmptyTargetOrSourceArgs.Default;
        }

        var data =
            await
        GetTransUnits(fileName);
        //string source =
        for (int index = data.TransUnits.Count - 1; index >= 0; index--)
        {
            var item = data.TransUnits[index];
            var sourceTargetElements = SourceTarget(item);
            if (xlfParts == XlfParts.Source)
            {
                if (sourceTargetElements.Item1 != null)
                {
                    if (sourceTargetElements.Item1.Value.Trim() == string.Empty)
                    {
                        if (args.RemoveWholeTransUnit)
                        {
                            sourceTargetElements.Item1.Remove();
                        }
                        else
                        {
                            throw new Exception("Instead of this use <source>.*</source> in VS!");
                        }
                    }
                }
            }
            else if (xlfParts == XlfParts.Target)
            {
                if (sourceTargetElements.Item2 != null)
                {
                    if (sourceTargetElements.Item2.Value.Trim() == string.Empty)
                    {
                        if (args.RemoveWholeTransUnit)
                        {
                            sourceTargetElements.Item2.Remove();
                        }
                        else
                        {
                            throw new Exception("Instead of this use <source>.*</source> in VS!");
                        }
                    }
                }
            }
        }

        if (args.Save)
        {
            data.XmlDocument.Save(fileName);
        }

        return data.XmlDocument.ToString();
    }

    // Trim whitespaces from start/end on source / target
    // A1 is possible to obtain with XmlLocalisationInterchangeFileFormat.GetLangFromFilename
    public static
        async Task
    TrimStringResources(string fileName)
    {
        var data =
            await
        GetTransUnits(fileName);
        foreach (XElement item in data.TransUnits)
        {
            var temp = SourceTarget(item);
            var source = temp.Item1;
            var target = temp.Item2;
            TrimValueIfNot(source);
            TrimValueIfNot(target);
        }

        data.XmlDocument.Save(fileName);
    }

    // A1 is possible to obtain with XlfResourcesH.PathToXlfSunamo
    public static
        async Task<XlfData>
    GetTransUnits(string fileName)
    {
        LangsDC toL = XmlLocalisationInterchangeFileFormatSunamo.GetLangFromFilename(fileName);
        string enS =
            await
        FileAsync.ReadAllTextAsync(fileName);
        var data = new XlfData();
        data.Path = fileName;
        var namespacesHolder = new XmlNamespacesHolder();
        namespacesHolder.ParseAndRemoveNamespacesXmlDocument(enS);
        data.XmlDocument =
            await
        XHelper.CreateXDocument(fileName);
        XHelper.AddXmlNamespaces(namespacesHolder.NamespaceManager);
        XElement xliff = XHelper.GetElementOfName(data.XmlDocument, "xliff")!;
        var allElements = XHelper.GetElementsOfNameWithAttrContains(xliff!, "file", "target-language", toL.ToString());
        var resources = allElements.Where(element => XHelper.Attr(element, "original")!.Contains("/" + "RESOURCES" + "/"));
        XElement file = resources.First();
        XElement body = XHelper.GetElementOfName(file, "body")!;
        data.Group = XHelper.GetElementOfName(body!, "group")!;
        data.TransUnits = XHelper.GetElementsOfName(data.Group!, TransUnit.TransUnitTagName);
        return data;
    }

    public static
        async Task
    Append(string target, string pascal, string fileName)
    {
        var data =
            await
        GetTransUnits(fileName);
        var exists = XHelper.GetElementOfNameWithAttr(data.Group, TransUnit.TransUnitTagName, "id", pascal);
        if (exists != null)
        {
            return;
        }

        Append( /*source,*/target, pascal, data);
        data.XmlDocument.Save(fileName);
        await XHelper.FormatXml(fileName);
    }

    // Appends a new trans-unit element with the specified target text and ID to the XLF data group.
    public static void Append( /*string source, */string target, string pascal, XlfData data)
    {
        var transUnit = new TransUnit();
        transUnit.Id = pascal;
        // Directly set to null due to not inserting into .xlf
        transUnit.Source = null!;
        //tu.translate = true;
        // Inlined from SHTrim.TrimStartAndEnd - ořezává znaky ze začátku a konce podle podmínky
        var trimmedTarget = target;
        // Ořez ze začátku
        for (int index = 0; index < trimmedTarget.Length; index++)
        {
            if (!char.IsLetterOrDigit(trimmedTarget[index]))
            {
                trimmedTarget = trimmedTarget.Substring(1);
                index--;
            }
            else
            {
                break;
            }
        }

        // Ořez z konce
        for (int characterIndex = trimmedTarget.Length - 1; characterIndex >= 0; characterIndex--)
        {
            if (!char.IsLetterOrDigit(trimmedTarget[characterIndex]))
            {
                trimmedTarget = trimmedTarget.Remove(trimmedTarget.Length - 1, 1);
            }
            else
            {
                break;
            }
        }

        transUnit.Target = trimmedTarget;
        var xml = transUnit.ToString()!;
        XElement element = XElement.Parse(xml!);
        element = XHelper.MakeAllElementsWithDefaultNs(element);
        data.Group.Add(element);
    }

    // Removes trans-units from both XLF file and XLF keys by matching IDs.
    public static async Task RemoveFromXlfAndXlfKeys(string fileName, List<string> idsEndingEnd)
    {
        await RemoveFromXlfAndXlfKeys(fileName, idsEndingEnd, XlfParts.Id);
    }
}
