namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Generator of XML.
/// </summary>
public interface IXmlGeneratorDC
{
    /// <summary>
    /// Append line.
    /// </summary>
    void AppendLine();
    /// <summary>
    /// End comment.
    /// </summary>
    void EndComment();
    /// <summary>
    /// Insert.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="text">The text.</param>
    void Insert(int index, string text);
    /// <summary>
    /// Length.
    /// </summary>
    /// <returns>The length result.</returns>
    int Length();
    /// <summary>
    /// Start comment.
    /// </summary>
    void StartComment();
    /// <summary>
    /// Terminate tag.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    void TerminateTag(string tagName);
    /// <summary>
    /// To string.
    /// </summary>
    /// <returns>The to string result.</returns>
    string ToString();
    /// <summary>
    /// Write c data.
    /// </summary>
    /// <param name="innerCData">The inner c data.</param>
    void WriteCData(string innerCData);
    /// <summary>
    /// Write element.
    /// </summary>
    /// <param name="elementName">The element name.</param>
    /// <param name="inner">The inner.</param>
    void WriteElement(string elementName, string inner);
    /// <summary>
    /// Write non pair tag.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    void WriteNonPairTag(string tagName);
    /// <summary>
    /// Write non pair tag with 2 attrs.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attr1Name">The attr 1 name.</param>
    /// <param name="attr1Value">The attr 1 value.</param>
    /// <param name="attr2Name">The attr 2 name.</param>
    /// <param name="attr2Value">The attr 2 value.</param>
    void WriteNonPairTagWith2Attrs(string tagName, string attr1Name, string attr1Value, string attr2Name, string attr2Value);
    /// <summary>
    /// Write non pair tag with attr.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attrName">The attr name.</param>
    /// <param name="attrValue">The attr value.</param>
    void WriteNonPairTagWithAttr(string tagName, string attrName, string attrValue);
    /// <summary>
    /// Write non pair tag with attrs.
    /// </summary>
    /// <param name="appendNull">The append null.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">The attributes.</param>
    void WriteNonPairTagWithAttrs(bool appendNull, string tagName, params string[] attributes);
    /// <summary>
    /// Write non pair tag with attrs.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <param name="args">The args.</param>
    void WriteNonPairTagWithAttrs(string tag, List<string> args);
    /// <summary>
    /// Write non pair tag with attrs.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <param name="args">The args.</param>
    void WriteNonPairTagWithAttrs(string tag, params string[] args);
    /// <summary>
    /// Write raw.
    /// </summary>
    /// <param name="rawContent">The raw content.</param>
    void WriteRaw(string rawContent);
    /// <summary>
    /// Write tag.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    void WriteTag(string tagName);
    /// <summary>
    /// Write tag namespace manager.
    /// </summary>
    /// <param name="rss">The rss.</param>
    /// <param name="nsmgr">The nsmgr.</param>
    /// <param name="namespaceUri">The namespace uri.</param>
    /// <param name="prefix">The prefix.</param>
    void WriteTagNamespaceManager(object rss, XmlNamespaceManager nsmgr, string namespaceUri, string prefix);
    /// <summary>
    /// Write tag namespace manager.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="nsmgr">The nsmgr.</param>
    /// <param name="args">The args.</param>
    void WriteTagNamespaceManager(string tagName, XmlNamespaceManager nsmgr, params string[] args);
    /// <summary>
    /// Write tag with 2 attrs.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attr1Name">The attr 1 name.</param>
    /// <param name="attr1Value">The attr 1 value.</param>
    /// <param name="attr2Name">The attr 2 name.</param>
    /// <param name="attr2Value">The attr 2 value.</param>
    void WriteTagWith2Attrs(string tagName, string attr1Name, string attr1Value, string attr2Name, string attr2Value);
    /// <summary>
    /// Write tag with attr.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <param name="skipEmptyOrNull">The skip empty or null.</param>
    void WriteTagWithAttr(string tag, string attributeName, string attributeValue, bool skipEmptyOrNull = false);
    /// <summary>
    /// Write tag with attrs.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">The attributes.</param>
    void WriteTagWithAttrs(string tagName, List<string> attributes);
    /// <summary>
    /// Write tag with attrs.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">The attributes.</param>
    void WriteTagWithAttrs(string tagName, params string[] attributes);
    /// <summary>
    /// Write tag with attrs check null.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes">The attributes.</param>
    void WriteTagWithAttrsCheckNull(string tagName, params string[] attributes);
    /// <summary>
    /// Write xml declaration.
    /// </summary>
    void WriteXmlDeclaration();
}
