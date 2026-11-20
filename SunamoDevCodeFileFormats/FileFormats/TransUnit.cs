namespace SunamoDevCode.FileFormats;

/// <summary>
/// Represents the trans unit type.
/// </summary>
public class TransUnit
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the translate.
    /// </summary>
    public bool Translate { get; set; }

    /// <summary>
    /// Gets or sets the xml space.
    /// </summary>
    public string XmlSpace { get; set; } = null!;

    private string _source = null!;

    /// <summary>
    /// The source.
    /// </summary>
    public string Source
    {
        get
        {
            return _source;
        }
        set
        {
            if (value != null)
            {
                value = SHNotTranslateAble.DecodeSlashEncodedString(value);
                value = HtmlAssistant.TrimInnerHtml(value);
                value = HtmlDocument.HtmlEncode(value);
            }
            _source = value!;
        }
    }

    private string _target = null!;

    /// <summary>
    /// The target.
    /// </summary>
    public string Target
    {
        get
        {
            return _target;
        }
        set
        {
            value = SHNotTranslateAble.DecodeSlashEncodedString(value);
            value = HtmlAssistant.TrimInnerHtml(value);
            value = HtmlDocument.HtmlEncode(value);
            _target = value;
        }
    }

    /// <summary>
    /// The trans unit tag name constant.
    /// </summary>
    public const string TransUnitTagName = "trans-unit";

    /// <summary>
    /// To string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The to string result.</returns>
    public string ToString(IXmlGeneratorDC generator)
    {
        generator.WriteTagWithAttrs(TransUnitTagName, "id", Id, "translate", BTS.BoolToString(Translate, true), "xml:space", "preserve");
        generator.WriteElement("source", Source);

        generator.WriteTagWithAttr("target", "state", "translated");
        generator.WriteRaw(Target);
        generator.TerminateTag("target");

        generator.TerminateTag(TransUnitTagName);

        return generator.ToString();
    }
}
