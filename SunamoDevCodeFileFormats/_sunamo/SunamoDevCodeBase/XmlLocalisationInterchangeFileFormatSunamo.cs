namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

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
    }








}
