using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Validate
    {
        public string Validate_Plateforms(List<string> list, Dictionary<string, Pair> plateform_dict)
        {
            string p1 = list[3];
            string p2 = list[5];
            foreach (KeyValuePair<string, Pair> entry in plateform_dict)
            {
                string key=entry.Key;
                Pair value=entry.Value;
                if (value.value1==p1 && value.value2==p2) 
                {
                    return key;
                }
            }

            return "";
        }
    }
}
