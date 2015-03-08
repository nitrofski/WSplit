using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

public class XMLReader
{
    private Split split;
    private XmlDocument xmlDocument;

    public XMLReader(String file)
    {
        xmlDocument = new XmlDocument();
        xmlDocument.Load(file);
    }

    /// <summary>
    /// Read split information from the file and returns it.
    /// </summary>
    /// <returns>The split with all its information.</returns>
    public Split ReadSplit()
    {
        StringBuilder stringBuilder = new StringBuilder();
        String runTitle = "";
        int attempt = 0;
        int runOffset = 0;
        XmlNode node; 

        node = xmlDocument.DocumentElement.SelectSingleNode("/Run/GameName");
        stringBuilder.Append(node.InnerText);

        node = xmlDocument.DocumentElement.SelectSingleNode("/Run/Category");
        stringBuilder.Append(node.InnerText);
        runTitle = stringBuilder.ToString();

        node = xmlDocument.DocumentElement.SelectSingleNode("/Run/Offset");
        runOffset = Int32.Parse(node.InnerText);       

        return this.split;
    }
}

