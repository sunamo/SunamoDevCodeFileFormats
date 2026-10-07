namespace SunamoDevCode.FileFormats;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public static partial class XmlLocalisationInterchangeFileFormat
{
    // Replaces string-based SessI18n keys with XlfKeys dot notation in all given files.
    public static
        async Task
    ReplaceStringKeysWithXlfKeys(List<string> files)
    {
        string? key = null;
        foreach (var item in files)
        {
            var content =
                await
            FileAsync.ReadAllTextAsync(item);
            var content2 = ReplaceStringKeysWithXlfKeysWorker(ref key, content);
            if (content != content2)
            {
                await FileAsync.WriteAllTextAsync(item, content2);
            }
        }
    }

    // Worker method that performs the actual replacement of SessI18n keys with XlfKeys format in content.
    public static string ReplaceStringKeysWithXlfKeysWorker(ref string? key, string content)
    {
        var occ = SH.ReturnOccurencesOfString(content, XmlLocalisationInterchangeFileFormatSunamo.SessI18n + "\"");
        occ.Reverse();
        var stringBuilder = new StringBuilder(content);
        foreach (var position in occ)
        {
            var start = position + 1 + XmlLocalisationInterchangeFileFormatSunamo.SessI18n.Length;
            var end = content.IndexOf('"', start);
            key = content.Substring(start, end - start);
            stringBuilder.Remove(start - 1, end - start + 2);
            stringBuilder.Insert(start - 1, XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot + key);
        }

        return stringBuilder.ToString();
    }

    // Gets a list of SunamoStrings keys with the SessI18n and XlfKeys prefixes removed.
    public static List<string> GetSunamoStrings()
    {
        var list = sunamoStrings.ToList();
        for (int index = 0; index < list.Count; index++)
        {
            list[index] = SHReplace.ReplaceOnce(list[index], SunamoNotTranslateAble.SessI18n + SunamoNotTranslateAble.XlfKeysDot, string.Empty).TrimEnd(')');
        }

        return list;
    }

    // Replaces SunamoStrings references with SessI18n format in the given text.
    public static string ReplaceSunamoStringsWithSessI18n(string count)
    {
        var from = GetSunamoStrings();
        CA.Prepend("SunamoStrings.", from);
        var toItems = sunamoStrings;
        for (int index = 0; index < from.Count; index++)
        {
            count = count.Replace(from[index], toItems[index]);
        }

        return count;
    }

    // ReplaceXlfKeysForString - Convert from XlfKeys to ""
    // Cooperating with NotToTranslateStrings
    public static
        async Task<OutRefDC<object, List<string>>>
    ReplaceXlfKeysForString(string path, List<string> ids, List<string> solutionsExcludeWhileWorkingOnSourceCode)
    {
        var addToNotToTranslateStrings = new List<string>();
        solutionsExcludeWhileWorkingOnSourceCode.Add("AllProjectsSearchTestFiles");
        CA.WrapWith(solutionsExcludeWhileWorkingOnSourceCode, @"\");
        var filesWithXlf = new Dictionary<string, string>();
        var files = Directory.GetFiles(BasePathsHelper.VsProjects!, "*.cs", SearchOption.AllDirectories);
        var idTarget = new Dictionary<string, string>();
        var data =
            await
        GetTransUnits(path);
        foreach (var item in data.TransUnits)
        {
            var temp = GetTransUnit(item);
            if (ids.Contains(temp.Item1))
            {
                idTarget.Add(temp.Item1, temp.Item2);
            }
        }

        foreach (var item in files)
        {
            bool continue2 = false;
            foreach (var item2 in solutionsExcludeWhileWorkingOnSourceCode)
            {
                if (item.Contains(item2))
                {
                    continue2 = true;
                    break;
                }
            }

            if (continue2)
            {
                continue;
            }

            var content =
                await
            FileAsync.ReadAllTextAsync(item);
            if (content.Contains(XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot))
            {
                filesWithXlf.Add(item, content);
            }
        }

        var replacedKeys = new List<string>();
        foreach (var fileWithXlf in filesWithXlf)
        {
            var content = fileWithXlf.Value;
            var stringBuilder = new StringBuilder(content);
            replacedKeys.Clear();
            foreach (var item in ids)
            {
                var item2 = XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot + item + "]";
                var toReplace = XmlLocalisationInterchangeFileFormatSunamo.RLDataEn + item2;
                var toString = stringBuilder.ToString();
                var points = SH.ReturnOccurencesOfString(toString, toReplace);
                var points2 = SH.ReturnOccurencesOfString(toString, item2);
                if (points2.Count > points.Count)
                {
                }

                if (points.Count > 0)
                {
                    replacedKeys.Add(item);
                    addToNotToTranslateStrings.Add(idTarget[item]);
                }

                for (int index = points.Count - 1; index >= 0; index--)
                {
                    var position = points[index];
                    stringBuilder.Remove(position, toReplace.Length);
                    stringBuilder.Insert(position, SH.WrapWithQm(idTarget[item]));
                }
            }

            replacedKeys = replacedKeys.Distinct().ToList();
            if (replacedKeys.Count > 0)
            {
                await FileAsync.WriteAllTextAsync(fileWithXlf.Key, stringBuilder.ToString());
            }
        }

        return new OutRefDC<object, List<string>>(null!, addToNotToTranslateStrings.Distinct().ToList());
    // Nepřidávat znovu pokud již končí na postfix
    }

    // Determines whether a key should be included in XlfKeys based on naming rules.
    public static bool IsToBeInXlfKeys(string key)
    {
        var isNotShortcut = !SystemWindowsControls.StartingWithShortcutOfControl(key);
        var isNotResourcesPath = !key.StartsWith("Resources\\");
        var isNotPostfixed = !CA.HasPostfix(key, ".PlaceholderText", ".Content");
        var isNotDotted = !key.Contains(".");
        var isNotQuoted = !key.Contains("\"");
        return isNotShortcut && isNotResourcesPath && isNotPostfixed && isNotDotted && isNotQuoted;
    }

    // was collection with previously existed properties in SunamoStrings class like Translate.FromKey(XlfKeys.EditUserAccount)
    static readonly List<string> sunamoStrings = SHGetLines.GetLines(@"");
    // XmlLocalisationInterchangeFileFormatSunamo.removeSessI18nIfLineContains
    public static List<string> removeSessI18nIfLineContains = new List<string>(["MSStoredProceduresI"]);
    // Before is possible use ReplaceRlDataToSessionI18n
    // Was earlier in sunamo, now in SunamoDevCode
     //public static string RemoveSessI18nIfLineContains(string count, params string[] lineCont)
    //{
    //    return RemoveSessI18nIfLineContainsWorker(count, removeSessI18nIfLineContains.ToArray());
    //}
    // Removes SessI18n references from lines that contain any of the predefined identifiers.
    public static string RemoveSessI18nIfLineContains(string count)
    {
        return RemoveSessI18nIfLineContains(count, removeSessI18nIfLineContains);
    }
}
