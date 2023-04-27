using Common;
using DataFormatter.FormatterLogic.Model;
using System.Diagnostics.Tracing;
using System.Xml;

namespace DataFormatter.FormatterLogic.Strategies;

public class XMLFormatterStrategy : EventSource, IFormatterStrategy    
{
    public async Task<string> FormatTextAsync(FileData data)
    {
        using var activity = Monitoring.ActivitySource.StartActivity();

        // Create a new XML document asynchronously
        XmlDocument xmlDocument = await Task.Run(() =>
        {
            XmlDocument xmlDoc = new XmlDocument();

            // Create the root element with column names as attributes
            XmlElement rootElement = xmlDoc.CreateElement("data");
            rootElement.SetAttribute("column1", "Search word");
            rootElement.SetAttribute("column2", "Document number");
            rootElement.SetAttribute("column3", "Document");
            rootElement.SetAttribute("column4", "Search terms");
            rootElement.SetAttribute("column5", "Time used");
            xmlDoc.AppendChild(rootElement);
            Monitoring.Log.Information("Created a new xml document {Xml}", xmlDoc);

            return xmlDoc;
          
        });

        // Populate the XML document with data asynchronously
        await Task.Run(() =>
        {
            XmlElement dataRowElement1 = xmlDocument.CreateElement("row");
            dataRowElement1.SetAttribute("column1", data.SearchWord);
            dataRowElement1.SetAttribute("column5", data.Used.ToString());
            xmlDocument.DocumentElement.AppendChild(dataRowElement1);
            int idx = 0 ;
            foreach (var doc in data.Top10Details)
            {
                XmlElement dataRowElement2 = xmlDocument.CreateElement("row");
                dataRowElement2.SetAttribute("column2", idx.ToString());
                dataRowElement2.SetAttribute("column3", doc);
                dataRowElement2.SetAttribute("column4", data.DocIds[idx].Value.ToString());
                idx++;
                xmlDocument.DocumentElement.AppendChild(dataRowElement2);
            }
            Monitoring.Log.Information(" Populated the XML document with data {Xml}", xmlDocument);
        });

        // Serialize the XML document to a string
        string xmlString = xmlDocument.OuterXml;
        Monitoring.Log.Information("Serialize the XML document to a string {xmlString}", xmlString);

        return xmlString;
    }
}
