using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Windows;

namespace ClassLibrary
{
    public class FetchExcelData
    {
        Workbook wb;
        Worksheet ws;
        public FetchExcelData(string fileName)
        {
            _Excel.Application excel = new _Excel.Application();
            wb = excel.Workbooks.Open(fileName);
            ws = wb.Worksheets[1];
         
        }
        public string getCellValue(int row, int column)
        {

            Range cell = ws.Cells[row, column];

            return cell.Value;

        }

        public List<string> getRowValue(int row)
        {

            List<string> lst = new List<string>();
            //DateTime dt = DateTime.Parse(list[].ToString());
            //string HMS = String.Format("{0:HH:mm:ss}", dt);
            Range cell;
            Range xlRange = ws.UsedRange;
            int colCount = xlRange.Columns.Count;
       
            for (int i = 1; i < colCount; i++)
            {
                string value;
                cell = ws.Cells[row, i];
        
                if (i == 3 || i == 5)
                {
                   // DateTime dt = TimeSpan.FromDays(cell.Value);

                    value = TimeSpan.FromDays(cell.Value).ToString(@"hh\:mm\:ss");
                    // var value3 = Convert.ToDateTime(cell.Value);
                    
                  
                }
              else if (cell.Value == null)
                {
                    value = string.Empty;
                }
                else
                {
                    value = (cell.Value).ToString();
                }
                lst.Add(value);
                //  str += cell.Value.ToString();

            }
           
            return lst;
        }
        public List<string> getColumnValue(int column)
        {
            List<string> lst = new List<string>();

            Range cell;
            Range xlRange = ws.UsedRange;
            
            
            int rowCount = xlRange.Rows.Count;
            string str = "";
            for (int i = 1; i < rowCount; i++)
            {
                string value;
                cell = ws.Cells[i, column];
                if (cell.Value == null)
                {
                    value = string.Empty;
                }
                else
                {
                    value = (cell.Value).ToString();
                }
                lst.Add(value);
                //  str += cell.Value.ToString();
                
            }
            Console.WriteLine(str);
            return lst;
        }
        public int getRowCount()
        {
            Range xlRange = ws.UsedRange;

            int rowCount = xlRange.Rows.Count;
            return rowCount;

            
        }
        public int getColumnCount()
        {
            Range xlRange = ws.UsedRange;
            int colCount = xlRange.Columns.Count;
           
            
            return colCount;
        }
    }
}
