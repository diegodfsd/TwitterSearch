using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TwitterSearch
{
    public class TwitterXmlParser
    {
        public IEnumerable<string> Deserialize(Stream content)
        {
            XDocument document = XDocument.Load(content);
            XNamespace xNamespace = "http://www.w3.org/2005/Atom";

            return document
                .Descendants(xNamespace + "entry")
                .Select(e => e.Element(xNamespace + "title").Value);
        }
    }
}