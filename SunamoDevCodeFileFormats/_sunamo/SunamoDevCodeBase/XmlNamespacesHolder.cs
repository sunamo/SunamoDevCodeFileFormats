namespace SunamoDevCode._sunamo.SunamoDevCodeBase;

/// <summary>
/// Parses XML and removes namespaces.
/// </summary>
internal class XmlNamespacesHolder
{
    //internal NameTable nt = new NameTable();
    internal XmlNamespaceManager NamespaceManager = null!;

    /// <summary>
    /// Parses XML to XmlDocument without namespaces.
    /// </summary>
    internal XmlDocument ParseAndRemoveNamespacesXmlDocument(string content)
    {
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument = ParseAndRemoveNamespacesXmlDocument(content, xmlDocument.NameTable);

        return xmlDocument;
    }

    // A3 is default prefix because cant be empty anytime (/:Tag or /Tag dont working but /prefix:Tag yes)
    // Return XmlDocument but dont use return value
    // Just use XHelper class, because with XmlDocument is still not working
    /// <summary>
    /// Parses XML to XmlDocument without namespaces.
    /// </summary>
    internal XmlDocument ParseAndRemoveNamespacesXmlDocument(string content, XmlNameTable nameTable, string defaultPrefix = "x")
    {
        XmlDocument xmlDocument = new XmlDocument();

        /*
        * In default state have already three keys:
        * "" = ""
        xmlns=http://www.w3.org/2000/xmlns/
        xml=http://www.w3.org/XML/1998/namespace
        */
        NamespaceManager = new XmlNamespaceManager(nameTable);

        xmlDocument.LoadXml(content);

        foreach (XmlNode item in xmlDocument.ChildNodes)
        {
            if (item.NodeType == XmlNodeType.XmlDeclaration)
            {
                continue;
            }
            var root = item;
            for (int index = root.Attributes!.Count - 1; index >= 0; index--)
            {
                var att = root.Attributes[index];
                //
                string key = defaultPrefix;
                if (att.Name.StartsWith("xmlns"))
                {
                    if (att.Name.Contains(":"))
                    {
                        key = att.Name.Substring(6);
                    }

                    NamespaceManager.AddNamespace(key, att.Value);
                    // TODO: Delete wrong attribute but in outerXml is still figuring
                    root.Attributes.RemoveAt(index);
                }
            }
        }

        return xmlDocument;
    }

    /// <summary>
    /// Parses XML to XDocument without namespaces.
    /// </summary>
    internal XDocument ParseAndRemoveNamespacesXDocument(string content)
    {
        var xDocument = ParseAndRemoveNamespacesXmlDocument(content);
        return XDocument.Parse(xDocument.OuterXml);
    }

    }