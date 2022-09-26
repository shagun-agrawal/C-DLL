using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ClassNameAndNomValue_Pair {
       
        public Dictionary<string, string> className_Nom = new Dictionary<string, string>();
        public ClassNameAndNomValue_Pair(Dictionary<string, string> dict)
        {
            foreach (KeyValuePair<string,string> pair in dict)
            {
                this.className_Nom.Add(pair.Key,pair.Value);
            }
        }
    }
    public class Read_Profile_TDG_For_Run_Time
    {
        public Dictionary<string, ClassNameAndNomValue_Pair> read()
        {
            Dictionary<string, ClassNameAndNomValue_Pair> PairDict = new Dictionary<string, ClassNameAndNomValue_Pair>();
            string[] lines = System.IO.File.ReadAllLines(@"C:\OGTData\current\BLR_6127\OGTconf\profiles.tdg");
            int tem = 0;
                while (tem<lines.Length){
                string line = lines[tem];
              //  TdgMPath:IS_PF_BA_1_PF_MT_1 % path
                if (line.Contains("TdgMPath:") && line.Contains(" % path"))
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>(); 
                    string PathName;
                    string value1;
                    string value2;
                    line = line.Replace("TdgMPath:", "").Replace(" % path", "");
                    line = line.Replace(" ","");
                    PathName = line.Trim();
                    tem++;
                    line = lines[tem];
                    while(!line.Contains(" % train type") && tem < lines.Length)
                    {
                        tem++;
                        line = lines[tem];
                    }
                 
                        line=line.Replace(" % train type","").Replace("\"","");
                        line = line.Replace(" ","");
                        
                 
                    value1 = line.Trim();

                    while (!lines[tem+2].Contains("% run profile names") && tem < lines.Length)
                    {
                        tem++;
                    }
                    line = lines[tem];
                    value2 = line.Trim();

                    if (!PairDict.ContainsKey(PathName))
                    {
                        dictionary.Add(value1,value2);
                        PairDict.Add(PathName, new ClassNameAndNomValue_Pair(dictionary));
                    }
                    else {
                        ClassNameAndNomValue_Pair classData = PairDict[PathName];
                        Dictionary<string, string> pairSaveDict = classData.className_Nom;
                        if (!pairSaveDict.ContainsKey(value1))
                        {
                            pairSaveDict.Add(value1,value2);

                            PairDict[PathName] = new ClassNameAndNomValue_Pair(pairSaveDict);
                        }
                    }
                   
                }
                tem++;
            }

            return PairDict;

    }
    }
}
