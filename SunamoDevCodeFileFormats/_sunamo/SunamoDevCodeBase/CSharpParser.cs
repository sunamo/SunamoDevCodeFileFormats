namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Simple parser of C# source code.
/// </summary>
internal class CSharpParser
{

    public static
        async Task
        RemoveConsts(string file, List<string> remove)
    {
        remove.Insert(0, null!);

        // EN: Inlined from CAIndexesWithNull.IndexesWithNull - gets indexes of null values in collection
        // CZ: Inlined from CAIndexesWithNull.IndexesWithNull - získává indexy null hodnot v kolekci
        List<int> nullIndexes = [];
        int index = 0;
        foreach (var item in remove)
        {
            if (item == null)
            {
                nullIndexes.Add(index);
            }
            index++;
        }

        var lines = SHGetLines.GetLines(
            await
                FileAsync.ReadAllTextAsync(file)).ToList();

        for (var lineIndex = lines.Count - 1; lineIndex >= 0; lineIndex--)
        {
            var text = lines[lineIndex].Trim();
            if (text.Contains(XmlLocalisationInterchangeFileFormatSunamo.Cs))
            {
                var key = XmlLocalisationInterchangeFileFormatSunamo.GetConstsFromLine(text);
                var keyIndex = remove.IndexOf(key);
                if (keyIndex != -1)
                {
                    lines.RemoveAt(lineIndex);
                    remove.RemoveAt(keyIndex);
                }
            }
        }

        await FileAsync.WriteAllLinesAsync(file, lines);
        if (remove.Count > 0)
        {
            throw new Exception("Cant be deleted in XlfKeys: " + string.Join(",", remove));
        }
    }
}
