using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class From_End_Pair
    {
       public string from { get; set; }
        public string end { get; set; }
        public From_End_Pair(string from, string end)
        {
            this.from = from;
            this.end = end;
        }
    }
    class Read_Excel_For_Mission
    {
        public List<From_End_Pair> Read(string filePath)
        {
            List<From_End_Pair> list = new List<From_End_Pair>();

            try
            {
             

                FetchExcelData fetchExcel_data = new FetchExcelData(filePath);
                int row = fetchExcel_data.getRowCount();
                   for (int i = 2; i <= row; i++)
                {
                    string start = fetchExcel_data.getCellValue(i, 4);
                    string end = fetchExcel_data.getCellValue(i, 6);
                    list.Add(new From_End_Pair(start, end));
                }

               

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return list;
        }

    }
}
