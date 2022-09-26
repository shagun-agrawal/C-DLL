using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClassLibrary
{

    public class stopList_And_pathList_Pair
    {

        public string classType;
        public List<string> stopList { get; set; }
        public List<string> pathList { get; set; }
   
      //  public string classType;
        public stopList_And_pathList_Pair(List<string> l1, List<string> l2,string classtype)
        {
          
            this.stopList = new List<string>();
            this.pathList = new List<string>();
            this.stopList.AddRange(l1);

            this.pathList.AddRange(l2);
            this.classType = classtype;



        }
    }
    public class Dictionary_And_ExcelList_Pair {
        public Dictionary<string, stopList_And_pathList_Pair> dict=new Dictionary<string, stopList_And_pathList_Pair>();
        public List<From_End_Pair> list = new List<From_End_Pair>();
       
        public Dictionary_And_ExcelList_Pair(Dictionary<string, stopList_And_pathList_Pair> dict, List<From_End_Pair> list)
        {
            foreach (KeyValuePair<string, stopList_And_pathList_Pair> pair in dict)
            {
                this.dict.Add(pair.Key,pair.Value);
            }
           
            this.list.AddRange(list);
        }


    }
    public class Read_Mission_TDG
    {
        public string[] lines;
        public Read_Mission_TDG()
        {
        
        lines = System.IO.File.ReadAllLines(@"C:\OGTData\current\BLR_6127\OGTconf\mission.tdg");
        }

        public Dictionary_And_ExcelList_Pair readMission(string filePath,List<string> fromList, List<string> toList)
        {
            Read_Excel_For_Mission obj = new Read_Excel_For_Mission();
            //List<From_End_Pair> list = obj.Read(filePath);
            List<From_End_Pair> list = new List<From_End_Pair>();
            for (int m=0;m<fromList.Count;m++)
            {
                list.Add(new From_End_Pair(fromList[m],toList[m]));

            }
            int len = lines.Length;
       
           
            
            List<string> mission = new List<string>();
            List<List<string>> superpath = new List<List<string>>();
            List<List<string>> superstop = new List<List<string>>();



         
            int i = 0;
            List<string> classType = new List<string>();
            while (i<len)
            {
                List<string> path = new List<string>();
                List<string> stop = new List<string>();
                string line = lines[i];
                if (line.Contains("TdgMMission:") && !line.Contains("["))
                {

                

                    line = line.Replace("% mission", "");
                   line= line.Replace("TdgMMission:", "").Replace(" ", "");
                   line = line.Trim();
                    mission.Add(line);
                    i++;
                    line = lines[i];
                    if (line.Contains("TdgMTrainClass:") && line.Contains(" % train class"))
                    {
                        line = line.Replace("TdgMTrainClass:","").Replace(" % train class","");
                        line = line.Replace(" ","");
                         classType.Add(line.Trim());
                    }
                }
                if (line.Contains("TdgMPath:"))
                {
                    while (line.Contains("TdgMPath:"))
                    {
                        line = line.Replace("TdgMPath:", "").Replace(" ", "");
                        line = line.Trim();
                        path.Add(line);
                        i++;
                         line = lines[i];
                    }

                    superpath.Add(path);
                }

                if (line.Contains("TdgMNetworkPoint:"))
                {

                    while (line.Contains("TdgMNetworkPoint:"))
                    {
                        line = line.Replace("TdgMNetworkPoint:", "").Replace(" ", "");
                        line = line.Trim();
                        line = line.Substring(0,line.IndexOf("-"));
                        stop.Add(line);
                        i++;
                        line = lines[i];
                    }

                    superstop.Add(stop);
                }
                i++;
            }

            Dictionary<string, stopList_And_pathList_Pair> dict = new Dictionary<string, stopList_And_pathList_Pair>();
            foreach(From_End_Pair pair in list)
            {
                string from = pair.from;
                string end = pair.end;
                for (int k=0;k<superstop.Count;k++)
                {
                    List<string> stopLst = superstop[k];

                    if (stopLst[0]==from && stopLst[stopLst.Count-1]==end)
                    {
                        if (!dict.ContainsKey(mission[k])) {
                            dict.Add(mission[k], new stopList_And_pathList_Pair(stopLst, superpath[k],classType[k]));
                        }
                    }
                }

            }

        

            return new Dictionary_And_ExcelList_Pair(dict,list);

        }    
    }
}
