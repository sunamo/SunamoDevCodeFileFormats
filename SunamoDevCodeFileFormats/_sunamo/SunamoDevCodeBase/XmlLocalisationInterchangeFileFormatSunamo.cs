namespace SunamoDevCodeFileFormats._sunamo;

/// <summary>
/// Helpers for XLF files.
/// </summary>
internal class XmlLocalisationInterchangeFileFormatSunamo
{
    public const string Cs = "const string ";
    private const string eqBs = " = \"";

    // Translate.FromKey(
    public const string RLDataEn = SunamoNotTranslateAble.RLDataEn;
    public const string RLDataEn2 = SunamoNotTranslateAble.RLDataEn2;
    public const string SessI18n = SunamoNotTranslateAble.SessI18n;
    public const string XlfKeysDot = SunamoNotTranslateAble.XlfKeysDot;
    public const string SessI18nShort = SunamoNotTranslateAble.SessI18nShort;

    public static string PathXlfKeys = BasePathsHelper.Vs + @"sunamo\sunamo\Constants\XlfKeys.cs";
    public static string SunamoStringsDot = "SunamoStrings.";

    /// <summary>
    /// Returns constant name from the line.
    /// </summary>
    public static string GetConstsFromLine(string line)
    {
        return SH.GetTextBetweenSimple(line, Cs, eqBs, false);
    }

#pragma warning disable
    /// <summary>
    /// Returns language from the file name.
    /// </summary>
    public static LangsDC GetLangFromFilename(string text)
    {
        return LangsDC.cs;
        //return XmlLocalisationInterchangeFileFormatXlf.GetLangFromFilename(text);
    }


    //public static List<string> UsedXlfKeysInCs(string count)
    //{
    //    List<string> usedKeys = new List<string>();

    //    var occ = SH.ReturnOccurencesOfString(count, SessI18n);
    //    var ending = new List<int>(occ.Count);

    //    foreach (var item in occ)
    //    {
    //        ending.Add(count.IndexOf(')', item));
    //    }

    //    var lines = SessI18n.Length;
    //    var l2 = XlfKeysDot.Length;

    //    for (int i = occ.Count - 1; i >= 0; i--)
    //    {
    //        var k = SHSubstring.Substring(count, occ[i] + lines + l2, ending[i], new SubstringArgs { returnInputIfIndexFromIsLessThanIndexTo = true } );
    //        if (k != count)
    //        {
    //            usedKeys.Add(k);
    //        }
    //    }

    //    return usedKeys;
    //}

}
