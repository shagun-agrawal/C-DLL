using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Pair {

       public string value1;
       public string value2;
       public Pair(string v1,string v2)
        {
            this.value1=v1;
            this.value2 = v2;
        }
    }
    public class Read_Network_TDG
    {
        public string[] lines;
        
        //C:\Users\485781\Documents\Visual Studio 2015\Projects\ClassLibrary\ClassLibrary\Read_Network_TDG.cs
        public Read_Network_TDG()
        {
             lines = System.IO.File.ReadAllLines(@"C:\OGTData\current\PR_REG_MANILA_1.6.0_2041\OGTconf\network.tdg");

          
        }
        public Dictionary<string, Pair> read_Plateform_Data()
        {
            Dictionary<string, Pair> dict = new Dictionary<string, Pair>();
            string key = "";
            string value1 = "";
            string value2 = "";
            for (int i=0;i<lines.Length;i++)
            {
                string line = lines[i];
              
                if (line.Contains("[TdgMPath:"))
                {
                   key= line.Replace("[TdgMPath:","").Replace(" ","");
                    key = key.Trim();
                }
                if (line.Contains("% platform 1"))
                {
                    value1 = line.Replace("TdgMPlatform:","").Replace(" ","");
                    value1 = value1.Replace("%platform1", "");
                    value1 = value1.Trim();
                }
                if (line.Contains("% platform 2"))
                {
                    value2 = line.Replace("TdgMPlatform:", "").Replace(" ", "");
                    value2 = value2.Replace("%platform2", "");
                    value2 = value2.Trim();
                }
                if (key.Length>0 && value1.Length>0 && value2.Length>0)
                {
                    dict.Add(key, new Pair(value1, value2));
                    key = "";
                    value1 = "";
                    value2 = "";
                }

            }

            return dict;

        }
    }
}
