using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ClassLibrary
{
   public class UpdateCSV_UsingXML
    {
        Workbook wb;
        Worksheet ws;
        Microsoft.Office.Interop.Excel.Application xlApp = new Application();
        public UpdateCSV_UsingXML(string fileName)
        {
         //   _Excel.Application excel = new _Excel.Application();
            wb = xlApp.Workbooks.Open(fileName);

            ws = (Worksheet)wb.Worksheets.get_Item(1);



        }

        public void Update()
        {
            try {
                string FilePath = @"C:\Users\485781\Documents\Visual Studio 2015\Projects\T2.xml";

         
                XmlDocument xml = new XmlDocument();
                xml.Load(FilePath); // suppose that myXmlString contains "<Names>...</Names>"

                XmlNodeList trips = xml.SelectNodes("//ROOT/SCHEDULE/TRIPS/TRIP");
                int i = 2;
             

                foreach (XmlNode trip in trips)
                {
                    
                    string id = trip.Attributes["NUMBER"].Value.ToString();
                    string train = trip.Attributes["SERVICE_ID"].Value.ToString();
                    string direction = trip.Attributes["DIRECTION"].Value.ToString();
                    direction=(direction=="RIGHT")?"2" : "1";

                    string start = trip.Attributes["ENTRY_TIME"].Value.ToString();
                    string distance = trip.Attributes["DISTANCE"].Value.ToString();
                    int km = Int32.Parse(distance) / 1000;
                    distance = km.ToString();
                    int n = trip.ChildNodes.Count;

                    string from = trip.ChildNodes[0].Attributes["TOP"].Value;
                    string to = trip.ChildNodes[n - 1].Attributes["TOP"].Value;

                    RowUpdate(i,id,train,direction,start,km,from,to);
                    i++;
                }


                wb.Save();
                xlApp.Quit();
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(xlApp);
                //   < TRIP NUMBER = "1" TRIP_ID = "0001" SERVICE_ID = "01" DIRECTION = "RIGHT" ENTRY_TIME = "03:59:30" DISTANCE = "18632" TRAIN_CLASS = "TRFC_01" MISSION_TYPE = "Passenger" RUNNING_MODE = "Regulated" PREVIOUS_NUMBER = "" NEXT_NUMBER = "3" >




            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        public void RowUpdate(int row ,string id,string train, string direction, string start,int km,string from,string to)
        {


            Range cell;
            Range xlRange = ws.UsedRange;
            int colCount = xlRange.Columns.Count;

            cell = ws.Cells[row, 3];
            cell.Value = start;
            cell = ws.Cells[row, 4];
            cell.Value = from;
            cell = ws.Cells[row, 6];
            cell.Value = to;
            cell = ws.Cells[row, 9];
            cell.Value = train;
            cell = ws.Cells[row, 13];
            cell.Value = km;
            cell = ws.Cells[row, 16];
            cell.Value = id;
            cell = ws.Cells[row, 18];
            cell.Value = direction;
      


        }
    }
}
