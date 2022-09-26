using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace ClassLibrary
{
    public class FetchXmlData
    {

        public Dictionary<string, Dictionary<string, string>> Fetch(List<string> list, string FilePath)
        {
            Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();
            try
            {



                XElement xelement = XElement.Load(FilePath);
                IEnumerable<XElement> menus = xelement.Elements();

                foreach (string name in list)
                {
                    var xmlfiles = from el in xelement.Descendants(name) select el;
                    foreach (var files in xmlfiles)
                    {

                        Dictionary<string, string> NameID = new Dictionary<string, string>();
                        string Tagname = name.Substring(0, name.Length - 1);
                        var xmlfile = from el in files.Descendants(Tagname) select el;
                        foreach (var file in xmlfile)
                        {


                            string Name = file.Attribute("Name").Value.ToString();
                            string Id = file.Attribute("ID").Value.ToString();

                            NameID.Add(Id, Name);


                        }
                        dict.Add(Tagname, NameID);

                    }
                }
            }
            catch (Exception)
            {

            }
            return dict;
        }
    }
}
