using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ClassLibrary
{
   
   public class CreateXmlFile
    {
        public void Create(string Excel_file_path)
        {
            FetchExcelData obj = new FetchExcelData(Excel_file_path);
            Read_Network_TDG read = new Read_Network_TDG();
           Dictionary<string,Pair> PlatForm_dic= read.read_Plateform_Data();

            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //create the root element
            XmlElement root1 = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root1);

            //string.Empty makes cleaner code
            XmlElement root = doc.CreateElement(string.Empty, "ROOT", string.Empty);
            doc.AppendChild(root);

            
            XmlElement version = doc.CreateElement(string.Empty, "Versions", string.Empty);


            XmlElement ID1 = doc.CreateElement(string.Empty, "ID", string.Empty);
            ID1.SetAttribute("Name","OGT-G");
            ID1.SetAttribute("Version", "OGT-G-05.64");

            XmlElement ID2 = doc.CreateElement(string.Empty, "ID", string.Empty);
            ID2.SetAttribute("Name", "OGT-ATS-INTERFACE");
            ID2.SetAttribute("Version", "3.0");


            XmlElement ID3 = doc.CreateElement(string.Empty, "ID", string.Empty);
            ID3.SetAttribute("Name", "ODPT_APPLICATION_AREA");
            ID3.SetAttribute("Version", "PR_REG_MANILA_1.6.0_2041");

            XmlText title_text = doc.CreateTextNode("Schedule file");

            version.AppendChild(ID1);
            version.AppendChild(ID2);
            version.AppendChild(ID3);

            root.AppendChild(version);

            XmlElement title = doc.CreateElement(string.Empty, "TITLE", string.Empty);
            title.AppendChild(title_text);
            root.AppendChild(title);

            XmlElement schedule = doc.CreateElement(string.Empty, "SCHEDULE", string.Empty);
            schedule.SetAttribute("NAME","T1");
            schedule.SetAttribute("COMMENT", "");


            XmlElement trips = doc.CreateElement(string.Empty, "TRIPS", string.Empty);
            schedule.AppendChild(trips);
            List<List<string>> dict = new List<List<string>>();

            root.AppendChild(schedule);

           


            int total_Excel_Row= obj.getRowCount();
            for (int i=2;i<total_Excel_Row-7000;i++)
            {
                List<string> list = obj.getRowValue(i);
                dict.Add(list);
            }

           

            for (int i=2;i<total_Excel_Row-7000;i++) {

                Validate validate = new Validate();
                string Run_plateform_Name = validate.Validate_Plateforms(dict[i - 2], PlatForm_dic);
                if (Run_plateform_Name.Length>0)
                {

                    XmlElement trip = this.createTrip(i, dict, doc,Run_plateform_Name);

                    trips.AppendChild(trip);
                }

               

            }


            doc.Save(Directory.GetCurrentDirectory() + "//XMLFile.xml");
        }

        public XmlElement createTrip(int i, List<List<string>> dict, XmlDocument doc,string runPlateform_name)
        {
            List<string> list = dict[i - 2];
            XmlElement trip = doc.CreateElement(string.Empty, "TRIP", string.Empty);
            string number = list[15];
            string trip_Id = list[15];
            int len = trip_Id.Length;
            for (int l = 0; l < 4 -len ; l++)
            {
                trip_Id = "0" + trip_Id;
            }

            string service_Id = list[8];
            len = service_Id.Length;
            for (int l=0;l<3-len;l++)
            {
                service_Id = "0" + service_Id;
            }
           
            string direction = list[17] == "1" ? "LEFT" : "RIGHT";


          
            string entry_Time = list[2];
            int miters = Int32.Parse(list[12]) * 1000;
            string distance = (miters).ToString(); 
            string train_class = "TRFC_01";
            string mission_type = "Passenger";
            string Running_Mode = "Regulated";
            string previous_no = "";
            string next_No = "";
            string top1 = list[3];
            string dwelltime1 = "";
            string situation1 = "INITIAL_DEADRUN";

            string top2 = runPlateform_name;
            string dwelltime2 = "";
            string situation2 = "INITIAL_DEADRUN";

            string top3 = list[5];
            string dwelltime3 = "";
            string situation3 = "INITIAL_DEADRUN";
            trip.SetAttribute("NUMBER", number);
            trip.SetAttribute("TRIP_ID", trip_Id);
            trip.SetAttribute("SERVICE_ID", service_Id);
            trip.SetAttribute("DIRECTION", direction);
            trip.SetAttribute("ENTRY_TIME", entry_Time);
            trip.SetAttribute("DISTANCE", distance);
            trip.SetAttribute("TRAIN_CLASS", train_class);
            trip.SetAttribute("MISSION_TYPE", mission_type);
            trip.SetAttribute("RUNNING_MODE", Running_Mode);
            trip.SetAttribute("PREVIOUS_NUMBER", previous_no);
            trip.SetAttribute("PREVIOUS_NUMBER", next_No);
            XmlElement stop1 = doc.CreateElement(string.Empty, "STOP", string.Empty);
            stop1.SetAttribute("TOP", top1);
            stop1.SetAttribute("DWELLTIME", dwelltime1);
            stop1.SetAttribute("SITUATION", situation1);



            XmlElement run = doc.CreateElement(string.Empty, "RUN", string.Empty);
            run.SetAttribute("TOP", top2);
            run.SetAttribute("RUNTIME", dwelltime2);
            run.SetAttribute("SITUATION", situation2);



            XmlElement stop2 = doc.CreateElement(string.Empty, "STOP", string.Empty);
            stop2.SetAttribute("TOP", top3);
            stop2.SetAttribute("DWELLTIME", dwelltime3);
            stop2.SetAttribute("SITUATION", situation3);




            trip.AppendChild(stop1);
            trip.AppendChild(run);
            trip.AppendChild(stop2);
            return trip;
        }
    }
}
