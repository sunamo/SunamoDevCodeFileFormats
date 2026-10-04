

//namespace SunamoDevCodeFileFormats.Tests.FileFormats;
//public class XmlLocalisationInterchangeFileFormatManipulationWithoutMockTests : XmlTestsBase
//{
//    /// <summary>
//    /// zkouška přidávání do xlf. z tohoto důvodu soubor musí existovat
//    /// </summary>
//    //[Fact]
//    public void AppendTest()
//    {
//        var file = @"D:\_Test\sunamo\SunamoDevCode\ParseAndRemoveNamespacesTest\sunamo.en-US.xlf";
//        XmlLocalisationInterchangeFileFormat.Append("Hello", "Ahoj", "HelloID", file);
//    }

//    //[Fact]
//    public
//#if ASYNC
//    async Task
//#else
//void
//#endif
//RemoveDuplicatesInXlfFileTest()
//    {
//        var xlfData =
//#if ASYNC
//    await
//#endif
// XmlLocalisationInterchangeFileFormat.GetTransUnits(base.pathXlf);
//        //XmlLocalisationInterchangeFileFormatSH.RemoveDuplicatesInXlfFile(base.pathXlf);

//        // Dont know how this is possible but this is working
//        foreach (var item in xlfData.trans_units)
//        {
//            item.Remove();
//        }

//        var outer = xlfData.xd.ToString();

//    }

//    //[Fact]
//    public void RemoveSessI18nIfLineContainsTest()
//    {



//        //MSStoredProceduresI.ci.a(sess.i18n(XlfKeys.sess.i18n));
//        /*
//        MSStoredProceduresI.ci.a(XlfKeys.DotCs);

//        Cs se nikde nevyužívá, proto jej nemusím ani nahrazovat
//        MSStoredProceduresI.ci.a(RLData.cs[XlfKeys.DotCs]);
//        */
//        var input = @"abc

//MSStoredProceduresI.ci.a(XlfKeys.dotEn);
//MSStoredProceduresI.ci.a(XlfKeys.ab);
//MSStoredProceduresI.ci.a(XlfKeys.DataEn);

//def
//klm";

//        var expected = @"abc

//MSStoredProceduresI.ci.a(XlfKeys.dotEn);
//MSStoredProceduresI.ci.a(XlfKeys.ab);
//MSStoredProceduresI.ci.a(XlfKeys.DataEn);


//def
//klm";



//        var actual = XmlLocalisationInterchangeFileFormat.RemoveSessI18nIfLineContains(input);
//        Assert.Equal(expected, actual);
//    }

//    //[Fact]
//    public
//#if ASYNC
//    async Task
//#else
//void
//#endif
//ReplaceStringKeysWithXlfKeysWorkerTest()
//    {
//        string key = null;
//        var content =
//#if ASYNC
//    await
//#endif
// TF.ReadAllText(@"D:\_Test\sunamo\SunamoDevCode\FileFormats\a.cs");
//        var output = XmlLocalisationInterchangeFileFormat.ReplaceStringKeysWithXlfKeysWorker(ref key, content);
//    }
//}
