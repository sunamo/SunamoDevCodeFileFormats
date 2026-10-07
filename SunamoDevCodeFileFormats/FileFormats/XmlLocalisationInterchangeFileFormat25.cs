namespace SunamoDevCode.FileFormats;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public static partial class XmlLocalisationInterchangeFileFormat
{
    // Removes SessI18n calls from lines that contain any of the specified substrings.
    public static string RemoveSessI18nIfLineContains(string count, IList<string>? lineCont = null)
    {
        if (lineCont == null || lineCont.Count == 0)
        {
            lineCont = removeSessI18nIfLineContains;
        }

        count = XmlLocalisationInterchangeFileFormat.ReplaceRlDataToSessionI18n(count);
        var list = SHGetLines.GetLines(count);
        bool cont = false;
        for (int index = list.Count - 1; index >= 0; index--)
        {
            var line = list[index];
            cont = false;
            foreach (var item in lineCont)
            {
                if (line.Contains(item))
                {
                    cont = true;
                    break;
                }
            }

            if (cont)
            {
                list[index] = RemoveAllSessI18n(list[index]);
            }
        }

        return string.Join(Environment.NewLine, list);
    }

    // Removes all SessI18n wrapper calls from the given text content.
    public static string RemoveAllSessI18n(string count)
    {
        var stringBuilder = new StringBuilder(count);
        var sessI18n = XmlLocalisationInterchangeFileFormatSunamo.SessI18nShort;
        var occ = SH.ReturnOccurencesOfString(count, sessI18n);
        var ending = new List<int>(occ.Count);
        foreach (var item in occ)
        {
            ending.Add(count.IndexOf(')', item));
        }

        var list = sessI18n.Length;
        for (int index = occ.Count - 1; index >= 0; index--)
        {
            stringBuilder = stringBuilder.Remove(ending[index], 1);
            stringBuilder = stringBuilder.Remove(occ[index], list);
        }

        return stringBuilder.ToString();
    }

    // Cached type reference for XmlLocalisationInterchangeFileFormat.
    public static Type type = typeof(XmlLocalisationInterchangeFileFormat);

    // Replaces RLData resource calls with SessionI18n calls using default parameters.
    public static string ReplaceRlDataToSessionI18n(string text)
    {
        return ReplaceRlDataToSessionI18n(text, SunamoNotTranslateAble.RLDataEn, SunamoNotTranslateAble.SessI18nShort);
    }

    // Replaces resource data calls with session i18n calls in the content, converting bracket styles.
    public static string ReplaceRlDataToSessionI18n(string content, string from, string to)
    {
        var RLDataEn = SunamoNotTranslateAble.RLDataEn;
        var SessI18n = SunamoNotTranslateAble.SessI18nShort;
        var RLDataCs = SunamoNotTranslateAble.RLDataCs;
        char endingChar = ']';
        string newEndingChar = ")";
        if (from == SessI18n)
        {
            endingChar = ')';
            newEndingChar = "]";
        }
        else if (from == RLDataCs || from == RLDataEn)
        {
        // keep as is
        }
        else
        {
            ThrowEx.NotImplementedCase(from);
        }

        string SunamoStringsDot = XmlLocalisationInterchangeFileFormatSunamo.SunamoStringsDot;
        int foundIndex = -1;
        foreach (var item in sunamoStrings)
        {
            foundIndex = content.IndexOf((string)item);
            if (foundIndex != -1)
            {
                var line = SH.GetLineFromCharIndex(content, SHGetLines.GetLines(content), foundIndex);
                if (line.Contains(SunamoStringsDot))
                {
                    content = content.Insert(foundIndex + Enumerable.Count(item), newEndingChar);
                    content = content.Remove(foundIndex, SunamoStringsDot.Length);
                    content = content.Insert(foundIndex, to + XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot);
                }
            }
        }

        var list = from.Length;
        content = content.Replace(XmlLocalisationInterchangeFileFormatSunamo.RLDataEn2, from);
        var occ = SH.ReturnOccurencesOfString(content, from);
        var ending = new List<int>();
        foreach (var item in occ)
        {
            var endingIndex = content.IndexOf(endingChar, item);
            ending.Add(endingIndex);
        }

        var stringBuilder = new StringBuilder(content);
        occ.Reverse();
        ending.Reverse();
        for (int index = 0; index < occ.Count; index++)
        {
            stringBuilder.Remove(occ[index], list);
            stringBuilder.Insert(occ[index], to);
            var ending2 = ending[index];
            stringBuilder.Remove(ending2, 1);
            stringBuilder.Insert(ending2, newEndingChar);
        }

        var count = stringBuilder.ToString();
        //TF.SaveFile(count, )
        return count;
    }

    // Gets the id attribute value from an XElement.
    public static string Id(XElement element)
    {
        return XHelper.Attr(element, "id")!;
    }
}
