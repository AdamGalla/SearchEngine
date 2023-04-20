using DataFormatter.FormatterLogic.Model;
using System.Xml;

namespace DataFormatter.FormatterLogic.Strategies;

public class ExcelFormatterStrategy : IFormatterStrategy    
{
    public async Task<string> FormatTextAsync(FileData data)
    {
        // Create a new XML document asynchronously
        XmlDocument xmlDocument = await Task.Run(() =>
        {
            XmlDocument xmlDoc = new XmlDocument();

            // Create the root element with column names as attributes
            XmlElement rootElement = xmlDoc.CreateElement("data");
            rootElement.SetAttribute("column1", "Search word");
            rootElement.SetAttribute("column2", "Document Ids");
            rootElement.SetAttribute("column3", "Top10");
            rootElement.SetAttribute("column4", "Top10Details");
            rootElement.SetAttribute("column5", "Time used");
            xmlDoc.AppendChild(rootElement);

            return xmlDoc;
        });

        // Populate the XML document with data asynchronously
        await Task.Run(() =>
        {
            XmlElement dataRowElement1 = xmlDocument.CreateElement("row");
            dataRowElement1.SetAttribute("column1", data.SearchWord);
            dataRowElement1.SetAttribute("column2", data.DocIds.ToString());
            dataRowElement1.SetAttribute("column3", data.Top10.ToString());
            dataRowElement1.SetAttribute("column4", data.Top10Details.ToString());
            dataRowElement1.SetAttribute("column5", data.Used.ToString());
            xmlDocument.DocumentElement.AppendChild(dataRowElement1);
        });

        // Serialize the XML document to a string
        string xmlString = xmlDocument.OuterXml;

        return xmlString;
    }
}
