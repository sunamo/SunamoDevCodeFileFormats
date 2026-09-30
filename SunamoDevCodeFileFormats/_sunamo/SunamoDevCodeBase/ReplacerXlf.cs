namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

// Dictionary as cache is good in database but not in ordinal c# app!
/// <summary>
/// Replaces characters in XLF texts.
/// </summary>
internal class ReplacerXlf
{
    private static ReplacerXlf? instance;
    private readonly List<string> values;
    public Dictionary<string, string> WithWithoutUnderscore { get; set; } = new();

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    private ReplacerXlf()
    {
        AllLists.InitHtmlEntitiesFullNames();

        values = AllLists.htmlEntitiesFullNames.Values.ToList();

        //values.Sort(SunamoComparer.StringLength.Instance.Desc);
        CA.Prepend("_", values);
    }

    public static ReplacerXlf Instance
    {
        get
        {
            if (instance is null) instance = new ReplacerXlf();

            return instance;
        }
    }

    /// <summary>
    /// Replaces underscores in the text.
    /// </summary>
    public string WithoutUnderscore(string text)
    {
        foreach (var item in values) text = text.Replace(item, string.Empty);
        return text;
    }

    //public static void AddKeys(List<string> k)
    //{
    //}

    //public static void AddKeysXlfKeysIds()
    //{
    //    List<string> ids = null; // XlfResourcesH.PathToXlfSunamo(Langs.en);
    //    var ids2 = new List<string>(ids);

    //    AddKeys(ids);
    //}
}
