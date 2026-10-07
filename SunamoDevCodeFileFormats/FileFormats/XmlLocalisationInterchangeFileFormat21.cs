namespace SunamoDevCode.FileFormats;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public static partial class XmlLocalisationInterchangeFileFormat
{
    public static
        async Task<OutRefDC<string, List<string>>>
    ReturnEndingOn(string fileName, List<string> list)
    {
        /*

! - always text
. - Always text
( - more often text
) - more often text
* - 50/50
, -  50/50
- Always text

Into A1 insert:
+ - all code
' - alwyas code
/ - always path
         */
        list = CAChangeContent.ChangeContent0(null!, list, temp => SHParts.RemoveAfterFirst(temp, ' '));
        var idsEndingOn = new List<string>();
        var result = new Dictionary<string, StringBuilder>();
        var textOutputGenerator = new TextOutputGenerator();
        var data =
            await
        GetTransUnits(fileName);
        foreach (var item in list)
        {
            result.Add(item, new StringBuilder());
        }

        foreach (var item in data.TransUnits)
        {
            string? transUnitId = null;
            var lastLetter = GetLastLetter(item, out transUnitId).ToString();
            if (list.Any(letter => letter == lastLetter))
            {
                result[lastLetter!].AppendLine(GetTarget(item).Value);
                idsEndingOn.Add(transUnitId!);
            }
        }

        foreach (var item in result)
        {
            textOutputGenerator.Paragraph(item.Value, item.Key);
        }

        return new OutRefDC<string, List<string>>(textOutputGenerator.StringBuilder.ToString(), idsEndingOn);
    }

    // Before mu
    public static
        async Task
    ReplaceForWithoutUnderscore(ILogger logger)
    {
        var withWithoutUnderscore = new Dictionary<string, string>();
        var files = XmlLocalisationInterchangeFileFormat.GetFilesCs(logger);
        await
        ReplaceStringKeysWithXlfKeys(files);
        string key = null!;
        foreach (var item in files)
        {
            withWithoutUnderscore.Clear();
            var content =
                await
            FileAsync.ReadAllTextAsync(item);
            var keys = GetKeysInCsWithRLDataEn(ref key, content);
            if (keys.Count > 0)
            {
                foreach (var keyWithUnderscore in keys)
                {
                    DictionaryHelper.AddOrSet(withWithoutUnderscore, keyWithUnderscore, ReplacerXlf.Instance.WithoutUnderscore(keyWithUnderscore));
                }

                foreach (var item2 in withWithoutUnderscore)
                {
                    content = content.Replace(item2.Key + '[', item2.Value + '[');
                }

                await FileAsync.WriteAllTextAsync(item, content);
            }
        }
    }

    public static List<string> GetFilesCs(ILogger logger, string? path = null)
    {
        return FSGetFiles.GetFiles(logger, path!, "*.cs", System.IO.SearchOption.AllDirectories, new GetFilesArgsDC() { /*excludeWithMethod = SunamoDevCodeHelper.RemoveTemporaryFilesVS*/ });
    }

    // Is calling in XlfManager.WhichStartEndWithNonDigitNumber
    public static
        async Task
    ReplaceInXlfSolutions(ILogger logger, string pairsReplace)
    {
        if (pairsReplace == string.Empty)
        {
            System.Diagnostics.Debugger.Break();
        }

        var temp = SHSplit.SplitFromReplaceManyFormatList(pairsReplace);
        var from = temp.Item1;
        var toItems = temp.Item2;
        foreach (var item in __xlfSolutions)
        {
            var files = GetFilesCs(logger, item);
            foreach (var item2 in files)
            {
                var content =
                    await
                FileAsync.ReadAllTextAsync(item2);
                content = content.Replace("\"-\"+\"-\"", "\"-\"");
                for (int index = 0; index < from.Count; index++)
                {
                    content = content.Replace(from[index], toItems[index]);
                }

                await FileAsync.WriteAllTextAsync(item2, content);
            //break;
            }
        //break;
        }
    }

    //    public static
    //        async Task<XlfData>
    //#else
    //  XlfData
    //#endif
    //        GetTransUnits(LangsDC en)
    //    {
    //        return null;
    //        //        return
    //        //#if ASYNC
    //        //    await
    //        //#endif
    //        //    GetTransUnits(XlfResourcesH.PathToXlfSunamo(en));
    //    }
    // Is used nowhere
    // Was in MainWindow but probably was replaced with GetAllLastLetterFromEnd
    public static
        async Task<List<string>>
    GetAllLastLetterFromEnd(string fileName)
    {
        var ids = new List<string>();
        var allLastLetters = new List<char>();
        var data =
            await
        GetTransUnits(fileName);
        foreach (XElement item in data.TransUnits)
        {
            string? transUnitId = null;
            var lastLetter = GetLastLetter(item, out transUnitId);
            if (lastLetter.HasValue)
            {
                allLastLetters.Add(lastLetter.Value);
            }

            ids.Add(transUnitId!);
        }

        allLastLetters = allLastLetters.Distinct().ToList();
        allLastLetters.Sort();

        return ids;
    }
}
