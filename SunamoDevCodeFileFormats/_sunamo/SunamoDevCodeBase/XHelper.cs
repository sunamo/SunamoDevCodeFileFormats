namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Helpers for working with XML.
/// </summary>
internal partial class XHelper
{
    internal static Dictionary<string, string> ns = new Dictionary<string, string>();
    /// <summary>
    /// Returns child elements of the name whose attribute contains the value.
    /// </summary>
    internal static IList<XElement> GetElementsOfNameWithAttrContains(XElement group, string tag, string attr, string value /*, bool caseSensitive = false*/)
    {
        return GetElementsOfNameWithAttrWorker(group, tag, attr, value /*, true, caseSensitive*/);
    }

    /// <summary>
    /// Returns elements of the name with the attribute value.
    /// </summary>
    internal static List<XElement> GetElementsOfNameWithAttrWorker(System.Xml.Linq.XElement xElement, string tag, string attr, string value /*, bool enoughIsContainsAttribute, bool caseSensitive*/)
    {
        var elements = new List<XElement>();
        var foundElements = XHelper.GetElementsOfNameRecursive(xElement, tag);
        foreach (XElement item in foundElements)
        {
            var attrValue = XHelper.Attr(item, attr);
            if (attrValue!.Contains(value) /*SH.ContainsBoolBool(attrValue, value, enoughIsContainsAttribute, caseSensitive)*/)
            {
                elements.Add(item);
            }
        }

        return elements;
    }

    /// <summary>
    /// Returns elements of the name recursively.
    /// </summary>
    internal static List<XElement> GetElementsOfNameRecursive(XElement node, string name)
    {
        var elements = new List<XElement>();
        if (name.Contains(":"))
        {
            var(prefix, localName) = SH.GetPartsByLocationNoOut(name, ':');
            prefix = XHelper.ns[prefix];
            foreach (XElement item in node.DescendantsAndSelf())
            {
                if (item.Name.LocalName == localName && item.Name.NamespaceName == prefix)
                {
                    elements.Add(item);
                }
            }
        }
        else
        {
            foreach (XElement item in node.DescendantsAndSelf())
            {
                if (item.Name.LocalName == name)
                {
                    elements.Add(item);
                }
            }
        }

        return elements;
    }

    /// <summary>
    /// Registers XML namespaces to the manager.
    /// </summary>
    internal static void AddXmlNamespaces(XmlNamespaceManager nsmgr)
    {
        foreach (string item in nsmgr)
        {
            // What is the type of item, so I do not need to use a dictionary
            var value = nsmgr.LookupNamespace(item);
            if (!ns.ContainsKey(item))
            {
                ns.Add(item, value!);
            }
        }
    }

    /// <summary>
    /// Returns the first child element of the name with the attribute value.
    /// </summary>
    internal static XElement? GetElementOfNameWithAttr(XElement node, string name, string attr, string value)
    {
        if (name.Contains(":"))
        {
            var(prefix, localName) = SH.GetPartsByLocationNoOut(name, ':');
            prefix = XHelper.ns[prefix];
            foreach (XElement item in node.Elements())
            {
                if (item.Name.LocalName == localName && item.Name.NamespaceName == prefix)
                {
                    if (Attr(item, attr) == value)
                    {
                        return item;
                    }
                }
            }
        }
        else
        {
            foreach (XElement item in node.DescendantsAndSelf())
            {
                if (item.Name.LocalName == name)
                {
                    if (Attr(item, attr) == value)
                    {
                        return item;
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Returns value of the attribute or null.
    /// </summary>
    internal static string? Attr(XElement item, string attr)
    {
        var attribute = item.Attribute(XName.Get(attr));
        if (attribute != null)
        {
            return attribute.Value;
        }

        return null;
    }

    /// <summary>
    /// Puts all elements to the default namespace.
    /// </summary>
    internal static XElement MakeAllElementsWithDefaultNs(XElement settings)
    {
        var ns2 = XHelper.ns[string.Empty];
        var toInsert = new List<object>();
        // shift ALL elements in the settings document into the target namespace
        foreach (XElement element in settings.DescendantsAndSelf())
        {
            element.Name = XName.Get(element.Name.LocalName, ns2);
        }

        //t
        var newElement = new XElement(XName.Get(settings.Name.LocalName, ns2), settings.Attributes(), settings.Descendants());
        return newElement;
    }

    /// <summary>
    /// Checks whether the name matches the tag.
    /// </summary>
    internal static bool IsRightTag(XElement xName, string name)
    {
        return IsRightTag(xName.Name, name);
    }

    /// <summary>
    /// Checks whether the name matches the tag.
    /// </summary>
    internal static bool IsRightTag(XName xName, string name)
    {
        var(prefix, tagName) = SH.GetPartsByLocationNoOut(name, ':');
        prefix = XHelper.ns[prefix];
        if (xName.LocalName == tagName && xName.NamespaceName == prefix)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns child elements of the name.
    /// </summary>
    internal static List<XElement> GetElementsOfName(XElement node, string name)
    {
        var result = new List<XElement>();
        if (name.Contains(":"))
        {
            foreach (XElement item in node.Elements())
            {
                if (IsRightTag(item, name))
                {
                    result.Add(item);
                }
            }
        }
        else
        {
            foreach (XElement item in node.Elements())
            {
                if (item.Name.LocalName == name)
                {
                    result.Add(item);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Checks whether the name matches the tag.
    /// </summary>
    internal static bool IsRightTag(XElement xName, string localName, string namespaceName)
    {
        return IsRightTag(xName.Name, localName, namespaceName);
    }

    /// <summary>
    /// Checks whether the name matches the tag.
    /// </summary>
    internal static bool IsRightTag(XName xName, string localName, string namespaceName)
    {
        if (xName.LocalName == localName && xName.NamespaceName == namespaceName)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns the first child element of the name.
    /// </summary>
    internal static XElement? GetElementOfName(XContainer node, string name)
    {
        if (name.Contains(":"))
        {
            var(prefix, localName) = SH.GetPartsByLocationNoOut(name, ':');
            prefix = XHelper.ns[prefix];
            foreach (XElement item in node.Elements())
            {
                if (IsRightTag(item, localName, prefix))
                {
                }

                if (item.Name.LocalName == localName && item.Name.NamespaceName == prefix)
                {
                    return item;
                }
            }
        }
        else
        {
            foreach (XElement item in node.Elements())
            {
                if (item.Name.LocalName == name)
                {
                    return item;
                }
            }
        }

        return null;
    }

    internal static 
    async Task<XDocument>
    CreateXDocument(string contentOrFn)
    {
        if (File.Exists(contentOrFn))
        {
            contentOrFn = 
            await
            FileAsync.ReadAllTextAsync(contentOrFn);
        }

        var enB = Encoding.UTF8.GetBytes(contentOrFn).ToList();
        XDocument? xDocument = null;
        using (MemoryStream oStream = new MemoryStream(enB.ToArray()))
        using (XmlReader oReader = XmlReader.Create(oStream))
        {
            xDocument = XDocument.Load(oReader);
        }

        return xDocument;
    }
}
