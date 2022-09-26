using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
   public class testingClass
    {
       
        public TimeSpan show(string filePath)
        {
            FetchExcelData obj = new FetchExcelData(filePath);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();


            List<List<string>> dict = new List<List<string>>();

            int total_Excel_Row = obj.getRowCount();
            for (int i = 2; i < total_Excel_Row; i++)
            {
                List<string> list = obj.getRowValue(i);
                dict.Add(list);
            }

            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;

            //Console.WriteLine("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}",
            //                 ts.Minutes, ts.Seconds);
            return ts;
        }

    }
}
