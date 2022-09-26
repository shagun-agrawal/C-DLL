using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;

namespace ClassLibrary
{
    public class LogFile
    {

        string LogFileName;
        string LogFilePath;
        public LogFile(string name, string path)
        {
            this.LogFileName = name;
            this.LogFilePath = path;
        }
        public string get_Log_FileName()
        {
            return LogFileName;
        }
        public string get_Log_FilePath()
        {
            return LogFilePath;
        }
    }
    public class FetchConfigData
    {
       public Dictionary<string, Dictionary<string, string>> dict { get; set; }


        public Dictionary<string, Dictionary<string, string>> FetchSections_Data(string Name_OF_Config_File)
        {
            dict = new Dictionary<string, Dictionary<string, string>>();
               string assemblyPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().CodeBase) + $"\\{Name_OF_Config_File}.config";
            try
            {
                XElement xelement = XElement.Load(assemblyPath);
                var xmlfiles = from el in xelement.Descendants("section") select el;
                List<string> sections = new List<string>();
                foreach (var section in xmlfiles)
                {
                    if (section.Attribute("name").Value.ToString() != "LogFile")
                    {
                        sections.Add(section.Attribute("name").Value.ToString());
                    }
                }
         
               // FetchXmlData xmlData = new FetchXmlData();

              //  dict = xmlData.Fetch(sections, ATS_Subsystem_File_Path);
            }
            catch(Exception)
            { }
            return dict;

        }

        public LogFile Fetch_LogFile_Name_And_Path(string Name_OF_Config_File)
        {
            //var val = Assembly.;
            string path = "";
            string name = "";
            string assemblyPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().CodeBase) + $"\\{Name_OF_Config_File}.config";
            XElement xelement = XElement.Load(assemblyPath);
            var xmlfiles = from el in xelement.Descendants("LogFile") select el;

            foreach (var tag in xmlfiles)
            {
                var xmlAddtag = from el in tag.Descendants("add") select el;
                foreach (var logData in xmlAddtag)
                {
                    if (logData.Attribute("key").Value.ToString() == "LogFileName")
                    {
                        name = logData.Attribute("value").Value.ToString();
                    }
                    if (logData.Attribute("key").Value.ToString() == "LogFilePath")
                    {
                        path = logData.Attribute("value").Value.ToString();
                    }
                }


            }
       
            //var config = ConfigurationManager.OpenExeConfiguration(assemblyPath);

            //var applicationSettingSectionGroup = config.SectionGroups["applicationSettings"];
            //var executableSection1 = applicationSettingSectionGroup.Sections.Keys;
            //for (int i = 0; i < executableSection1.Count; i++)
            //{
            //    sections.Add(executableSection1[i]);

            //}

            return new LogFile(name, path);

        }

    }
}
