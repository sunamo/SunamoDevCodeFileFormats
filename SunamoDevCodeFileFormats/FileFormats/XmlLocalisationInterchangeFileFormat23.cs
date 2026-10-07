namespace SunamoDevCode.FileFormats;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public static partial class XmlLocalisationInterchangeFileFormat
{
    // Gets trans-unit IDs or targets from XLF file that contain diacritics.
    public static
        async Task<List<string>>
    FromXlfWithDiacritic(string fileName, XlfParts xlfParts)
    {
        // Dont use, its also non czech with diacritic hats tuồng (hats bôi)
        var data =
            await
        GetTransUnits(fileName);
        var result = new List<string>();
        if (xlfParts == XlfParts.Id)
        {
            foreach (var item in data.TransUnits)
            {
                string? idTransUnit = null;
                GetLastLetter(item, out idTransUnit);
                if (SH.ContainsDiacritic(idTransUnit!))
                {
                    result.Add(idTransUnit!);
                // dont remove, just save ID, coz many strings have diac and is not czech hats tuồng (hats bôi)
                //item.Remove();
                //; break;
                }
            }
        }
        else if (xlfParts == XlfParts.Target)
        {
            foreach (var item in data.TransUnits)
            {
                var target = GetTarget(item).Value;
                string? idTransUnit = null;
                GetLastLetter(item, out idTransUnit);
                if (SH.ContainsDiacritic(target))
                {
                    result.Add(idTransUnit!);
                // dont remove, just save ID, coz many strings have diac and is not czech hats tuồng (hats bôi)
                //item.Remove();
                }
            }
        }

        //if (saveToClipboard)
        //{
        //    ClipboardHelper.SetLines(r);
        //}
        return result;
    }

    // Removes specified trans-units from the XLF file and corresponding constants from XlfKeys.
    public static
        async Task
    RemoveFromXlfAndXlfKeys(string fileName, List<string> idsEndingEnd, XlfParts xlfParts)
    {
        var data =
            await
        GetTransUnits(fileName);
        bool removed = false;
        if (xlfParts == XlfParts.Id)
        {
            for (int index = idsEndingEnd.Count - 1; index >= 0; index--)
            {
                foreach (var item in data.TransUnits)
                {
                    string? idTransUnit = null;
                    GetLastLetter(item, out idTransUnit);
                    var transUnitId = idsEndingEnd[index];
                    if (transUnitId == idTransUnit)
                    {
                        item.Remove();
                        break;
                    }
                }
            }
        }
        else if (xlfParts == XlfParts.Target)
        {
            for (int idIndex = idsEndingEnd.Count - 1; idIndex >= 0; idIndex--)
            {
                removed = false;
                foreach (var item in data.TransUnits)
                {
                    var target = HtmlAssistant.HtmlDecode(GetTarget(item).Value);
                    var endingId = idsEndingEnd[idIndex];
                    if (endingId == target)
                    {
                        try
                        {
                            item.Remove();
                            removed = true;
                        }
                        catch (Exception exception)
                        {
                            ThrowEx.ExcAsArg(exception, "Element can't be removed");
                        // have no parent
                        }

                        break;
                    }
                }

                if (!removed)
                {
                }
            }
        }

        await CSharpParser.RemoveConsts(XmlLocalisationInterchangeFileFormatSunamo.PathXlfKeys, idsEndingEnd);
        data.XmlDocument.Save(fileName);
    }

    // Removes duplicate trans-units from an XLF file, keeping only the first occurrence of each ID.
    public static
        async Task
    RemoveDuplicatesInXlfFile(string xlfPath)
    {
        // There is no way to delete node in xlf file with XlfDocument.
        // XlfDocument is using XDocument but its private
        /*
         1) Use xliffParser in sunamo.notmine
         2) Load in my own XmlDocument and remove throught XPath
         */
        /*
        I HAVE IT IN XDOCUMENT, I WILL USE THEREFORE METHODS OF LINQ
        METHOD REMOVE() IS THERE ISNT FOR FUN!!
         */
        if (false)
        {
        //XlfData data;
        //var ids = GetIds(xlfPath, out data);
        //data.XmlDocument.XPathSelectElement("/xliff/file[original=@'WPF.TESTS/RESOURCES/EN-US.RESX']");
        //List<string> duplicated;
        //CAG.RemoveDuplicitiesList(ids, out duplicated);
        //var b2 = data.XmlDocument.Descendants().Count();
        //foreach (var item in duplicated)
        //{
        //    var elements = data.group.Elements().ToList();
        //    for (int i = 0; i < elements.Count(); i++)
        //    {
        //        var id = XHelper.Attr(elements[i], "id");
        //        if (id == item)
        //        {
        //            elements.Remove(elements[i]);
        //            break;
        //        }
        //    }
        //}
        //var b3 = data.XmlDocument.Descendants().Count();
        //data.XmlDocument.Save(xlfPath);
        }

        var allIds =
            await
        GetIds(xlfPath);
        XlfData xlfData = allIds.Item2;
        List<string> duplicated;
        CAG.RemoveDuplicitiesList<string>(allIds.Item1, out duplicated);
        foreach (var item in duplicated)
        {
            xlfData.TransUnits.First(data => XHelper.Attr(data, "id") == item).Remove();
        }

        xlfData.XmlDocument.Save(xlfPath);
    }

    // Gets all trans-unit IDs from an XLF file along with the parsed XLF data.
    public static
        async Task<OutRefDC<List<string>, XlfData>>
    GetIds(string xlfPath)
    {
        var xlfData =
            await
        XmlLocalisationInterchangeFileFormat.GetTransUnits(xlfPath);
        xlfData.FillIds();
        return new OutRefDC<List<string>, XlfData>(xlfData.AllIds, xlfData);
    }

    // Replaces string keys with XLF keys in all C# files under the given path.
    public static
        async Task
    ReplaceStringKeysWithXlfKeys(string path)
    {
        List<string> files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories).ToList();
        await ReplaceStringKeysWithXlfKeys(files);
    }
}
