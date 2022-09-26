using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClassLibrary
{
    public class mission_place_dwellTime_pair
    {
        public List<string> place = new List<string>();
        public List<string> dwell = new List<string>();
        public mission_place_dwellTime_pair(List<string> placeList,List<string> dwellList)
        {
            this.place.AddRange(placeList);
            this.dwell.AddRange(dwellList);
        }

    }
    public class CreateXmlUsingMission_TDG
    {
        public void Create(string Excel_file_path)
        {
      
          

            FetchExcelData obj = new FetchExcelData(Excel_file_path);

            Read_Mission_TDG readMissions = new Read_Mission_TDG();
         


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
            ID1.SetAttribute("Name", "OGT-G");
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
            schedule.SetAttribute("NAME", "T1");
            schedule.SetAttribute("COMMENT", "");


            XmlElement trips = doc.CreateElement(string.Empty, "TRIPS", string.Empty);
            schedule.AppendChild(trips);
            List<List<string>> dict = new List<List<string>>();

            root.AppendChild(schedule);

            List<string> startList = new List<string>();
            List<string> endList = new List<string>();
            List<string> fromList = new List<string>();
            List<string> toList = new List<string>();
            List<string> KiloMitterList = new List<string>();
            List<string> directionList = new List<string>();
            List<string> IdList = new List<string>();
            List<string> TrainList = new List<string>();
            int columncount = obj.getColumnCount();


            GetColumnFromCSV getColumn = new GetColumnFromCSV(Excel_file_path);
            
            for (int coll = getColumn.startColumn; coll < columncount+ getColumn.startColumn; coll++)
            {
                string cellValue=obj.getCellValue(getColumn.startRow,coll);
                cellValue = cellValue.ToUpper();
                if (cellValue == "START")
                {
                    startList.AddRange(getColumn.getColumnValue(cellValue));
                  
                }
                else if (cellValue == "FROM")
                {
                    fromList.AddRange(getColumn.getColumnValue(cellValue));
                }
                else if (cellValue == "END")
                {
                    endList.AddRange(getColumn.getColumnValue(cellValue));
                }
                else if (cellValue == "TO")
                {
                    toList.AddRange(getColumn.getColumnValue(cellValue));
                }
                else if (cellValue == "TRAIN")
                {
                    TrainList.AddRange(getColumn.getColumnValue(cellValue));
                }
                else if (cellValue == "KM")
                {
                    KiloMitterList.AddRange(getColumn.getColumnValue(cellValue));
                }
                else if (cellValue == "ID." || cellValue == "ID")
                {
                    IdList.AddRange(getColumn.getColumnValue(cellValue));
                }
                else if (cellValue=="DIRECTION")
                {
                    directionList.AddRange(getColumn.getColumnValue(cellValue));
                }
            }


            Dictionary_And_ExcelList_Pair dictAndList = readMissions.readMission(Excel_file_path, fromList,toList);
            Dictionary<string, stopList_And_pathList_Pair> missionDict = dictAndList.dict;

            if (startList.Count==0 || endList.Count == 0 || fromList.Count == 0 || toList.Count == 0 || KiloMitterList.Count == 0 || directionList.Count == 0 || IdList.Count == 0 || TrainList.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Some Column Is Missing",  "Execution End",System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                Environment.Exit(0);
            }
            //if(endList.Count==0){

            //System.Windows.Forms.MessageBox.Show("End Column Is Missing", "Execution End", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);

            //}
            //if (fromList.Count==0)
            //{
            //    System.Windows.Forms.MessageBox.Show("Form Column Is Missing", "Execution End", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);

            //}
            //if (toList.Count == 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("To Column Is Missing", "Execution End", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);

            //}
            //if (KiloMitterList.Count == 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("km Column Is Missing", "Execution End", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);

            //}
            //if (directionList.Count == 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("Direction Column Is Missing", "Execution End", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);

            //}
            //if (IdList.Count == 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("Id. Column Is Missing", "Execution End", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);

            //}
            //if (TrainList.Count == 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("Train Column Is Missing", "Execution End", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);

            //}


            int total_Excel_Row = obj.getRowCount();
            for (int f = 1; f <total_Excel_Row; f++)
            {
                List<string> list =new List<string>();

             
                list.Add(startList[f-1]);
                list.Add(endList[f - 1]);
                list.Add(fromList[f - 1]);
                list.Add(toList[f - 1]);
                list.Add(KiloMitterList[f - 1]);
                list.Add(directionList[f - 1]);
                list.Add(IdList[f - 1]);
                list.Add(TrainList[f - 1]);
              

                dict.Add(list);
            }




            List<From_End_Pair> listOf_From_And_To = dictAndList.list;

            //       public class Read_Profile_TDG_For_Run_Time
            //{
            //    public Dictionary<string, ClassNameAndNomValue_Pair> read()

            Read_Profile_TDG_For_Run_Time ReadProfile_TDG = new Read_Profile_TDG_For_Run_Time();
            Dictionary<string, ClassNameAndNomValue_Pair> dictOf_ClassName_Nom=ReadProfile_TDG.read();


             string[] arrOfLines = System.IO.File.ReadAllLines(@"C:\OGTData\current\BLR_6127\OGTconf\mission.tdg");
            int tem = 0;
            Dictionary<string, mission_place_dwellTime_pair> place_and_dwelltime_dict = new Dictionary<string, mission_place_dwellTime_pair>();
            while(tem < arrOfLines.Length)
            {
                string line = arrOfLines[tem];
                if (line.Contains("[TdgMMission:"))
                {

                    line = line.Replace("[TdgMMission:", "").Replace(" ", "");
                    line = line.Trim();
                    if (missionDict.ContainsKey(line))
                    {
                        string missionName = line;
                        List<string> place = new List<string>();
                        List<string> dwellTime = new List<string>();
                        line = arrOfLines[tem];
                        while (!line.Contains("TdgMPlace:"))
                        {
                            tem++;
                            line = arrOfLines[tem];
                        }
                      
                        while (line.Contains("TdgMPlace:"))
                            {
                            //have to store in list
                            line = line.Replace("TdgMPlace:", "");
                            line = line.Trim();
                            place.Add(line);
                                tem++;
                                line = arrOfLines[tem];
                            }

                        tem++;
                        line = arrOfLines[tem];
                        while (!line.Contains("running mode"))
                            {
                            tem++;
                            tem++;
                            tem++;
                            line = arrOfLines[tem];
                            line=line.Trim();
                            dwellTime.Add(line);
                            // need to store dwell time
                            }
                        mission_place_dwellTime_pair pairOfDwellAndPlace = new mission_place_dwellTime_pair(place,dwellTime);
                        place_and_dwelltime_dict.Add(missionName,pairOfDwellAndPlace);
                       
                    }
                   
                }
                //foreach (KeyValuePair<string, stopList_And_pathList_Pair> entry in missionDict)
                //{



                //}
                tem++;
            }


            int number = 1;
            for (int f = 2; f <= total_Excel_Row; f++) {

                foreach (KeyValuePair<string, stopList_And_pathList_Pair> entry in missionDict)
                {

                 
                    stopList_And_pathList_Pair value = entry.Value;
                    string missionKey = entry.Key;
                    From_End_Pair Pair_list = listOf_From_And_To[f-2];
                    string from=Pair_list.from;
                    string end = Pair_list.end;
                    List<string> stoplist = new List<string>();
                    stoplist.AddRange(value.stopList);
                    if (stoplist[0]==from && stoplist[value.stopList.Count-1]==end) {
                        mission_place_dwellTime_pair place_with_dwellTimePair = place_and_dwelltime_dict[missionKey];
                        XmlElement trip = this.createTrip(f, entry.Key, dict, doc, value, place_with_dwellTimePair, dictOf_ClassName_Nom,number);
                        number++;
                        trips.AppendChild(trip);
                        break;
                    }

                   

                }
                
            }

            doc.Save(Directory.GetCurrentDirectory() + "//XMLFile.xml");
        }

        public XmlElement createTrip(int i,string mission, List<List<string>> dict, XmlDocument doc, stopList_And_pathList_Pair value, mission_place_dwellTime_pair placeWithDwellTime, Dictionary<string, ClassNameAndNomValue_Pair> dictOf_className_Nom,int numberTrip)
        {
            //list.Add(startList[f - 1]);0
            //list.Add(endList[f - 1]);1
            //list.Add(fromList[f - 1]);2
            //list.Add(toList[f - 1]);3
            //list.Add(KiloMitterList[f - 1]);4
            //list.Add(directionList[f - 1]);5
            //list.Add(IdList[f - 1]);6
            //list.Add(TrainList[f - 1]);7

            List<string> list = dict[i-2];
            XmlElement trip = doc.CreateElement(string.Empty, "TRIP", string.Empty);
            string number = numberTrip.ToString();
            string trip_Id = list[6];
            int len = trip_Id.Length;
            for (int l = 0; l < 4 - len; l++)
            {
                trip_Id = "0" + trip_Id;
            }

            string service_Id = list[7];
            len = service_Id.Length;
            for (int l = 0; l < 3 - len; l++)
            {
                service_Id = "0" + service_Id;
            }

            string direction = list[5] == "1" ? "LEFT" : "RIGHT";



            string entry_Time = list[0];
            int miters = Int32.Parse(list[4]) * 1000;
            string distance = (miters).ToString();
           
            string mission_type = "Passenger";
            string Running_Mode = "Regulated";
            string previous_no = "";
            string next_No = "";

     
          
            string situation1 = "INITIAL_DEADRUN";

            string dwelltime2 = "";
            string situation2 = "INITIAL_DEADRUN";

            trip.SetAttribute("NUMBER", number);
            trip.SetAttribute("TRIP_ID", trip_Id);
            trip.SetAttribute("SERVICE_ID", service_Id);
            trip.SetAttribute("DIRECTION", direction);
            trip.SetAttribute("ENTRY_TIME", entry_Time);
            trip.SetAttribute("DISTANCE", distance);
            trip.SetAttribute("TRAIN_CLASS", value.classType);
            trip.SetAttribute("MISSION_TYPE", mission_type);
            trip.SetAttribute("RUNNING_MODE", Running_Mode);
            trip.SetAttribute("PREVIOUS_NUMBER", previous_no);
            trip.SetAttribute("NEXT_NUMBER", next_No);
       


            bool flag = true;
            int j = 0;
            int k=0;
            List<string> dwelltimeList = placeWithDwellTime.dwell;
                while (k < value.stopList.Count) { 
                if (flag==true)
                {
                    XmlElement stop1 = doc.CreateElement(string.Empty, "STOP", string.Empty);
                    stop1.SetAttribute("TOP", value.stopList[k]);
                    stop1.SetAttribute("DWELLTIME", dwelltimeList[k]);
                    stop1.SetAttribute("SITUATION", situation1);
                    flag = false;
                    trip.AppendChild(stop1);
                    k++;
                }
                else {

                    XmlElement run = doc.CreateElement(string.Empty, "RUN", string.Empty);
                    run.SetAttribute("TOP", value.pathList[j]);
                    if (dictOf_className_Nom.ContainsKey(value.pathList[j]))
                    {
                        ClassNameAndNomValue_Pair pair = dictOf_className_Nom[value.pathList[j]];
                        Dictionary<string, string> classTypeAndNomValueDict = pair.className_Nom;

                        run.SetAttribute("RUNTIME", classTypeAndNomValueDict[value.classType]);
                    }
                    else {
                        run.SetAttribute("RUNTIME", dwelltime2);
                    }
                   
                    run.SetAttribute("SITUATION", situation2);
                    trip.AppendChild(run);
                    j++;
                    flag = true;
                }
            }


       
            return trip;
        }
    }
}
