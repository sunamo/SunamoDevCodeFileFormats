namespace SunamoDevCodeFileFormats.Tests.FileFormats;

using SunamoDevCode.FileFormats;

public class XmlLocalisationInterchangeFileFormatTests
{
    /// <summary>
    /// Working perfectly
    /// </summary>
    [Fact]
    public void ReplaceRlDataToSessionI18nTest()
    {
        var RLDataEn = ""; // SunamoNotTranslateAble.RLDataEn;
        var SessI18nShort = ""; //  SunamoNotTranslateAble.SessI18nShort;
        var RLDataCs = ""; //  SunamoNotTranslateAble.RLDataCs;

        var input = "abc sess.i18n(XlfKeys.a) def sess.i18n(XlfKeys.abc) ghi sess.i18n(XlfKeys.a) jkl";
        var expected = "abc sess.i18n(XlfKeys.a) def sess.i18n(XlfKeys.abc) ghi sess.i18n(XlfKeys.a) jkl";

        var actual = XmlLocalisationInterchangeFileFormat.ReplaceRlDataToSessionI18n(input, RLDataEn, SessI18nShort);
        Assert.Equal(expected, actual);

        input = "MSStoredProceduresI.ci.a(XlfKeys.dotEn)";
        //        input = @"abc

        //MSStoredProceduresI.ci.a(XlfKeys.dotEn);

        //def
        //klm";
        expected = "MSStoredProceduresI.ci.a(XlfKeys.dotEn)";

        actual = XmlLocalisationInterchangeFileFormat.ReplaceRlDataToSessionI18n(input, RLDataEn, SessI18nShort);
        Assert.Equal(expected, actual);

        input = "jkl sess.i18n(XlfKeys.AddAsRsvp) mno";
        expected = "jkl sess.i18n(XlfKeys.AddAsRsvp) mno";

        actual = XmlLocalisationInterchangeFileFormat.ReplaceRlDataToSessionI18n(input, RLDataEn, SessI18nShort);
        Assert.Equal(expected, actual);


        expected = "abc sess.i18n(XlfKeys.a) def sess.i18n(XlfKeys.abc) ghi sess.i18n(XlfKeys.a) jkl sess.i18n(XlfKeys.AddAsRsvp) mno";
        input = "abc sess.i18n(XlfKeys.a) def sess.i18n(XlfKeys.abc) ghi sess.i18n(XlfKeys.a) jkl sess.i18n(XlfKeys.AddAsRsvp) mno";
        actual = XmlLocalisationInterchangeFileFormat.ReplaceRlDataToSessionI18n(input, RLDataEn, SessI18nShort);
        Assert.Equal(expected, actual);


        input = "abc sess.i18n(XlfKeys.a) def sess.i18n(XlfKeys.abc) ghi sess.i18n(XlfKeys.a) jkl sess.i18n(XlfKeys.AddAsRsvp) mno";
        expected = "abc RLData.cs[XlfKeys.a] def RLData.cs[XlfKeys.abc] ghi RLData.cs[XlfKeys.a] jkl RLData.cs[XlfKeys.AddAsRsvp] mno";
        actual = XmlLocalisationInterchangeFileFormat.ReplaceRlDataToSessionI18n(input, SessI18nShort, RLDataCs);
        Assert.Equal(expected, actual);
    }


}