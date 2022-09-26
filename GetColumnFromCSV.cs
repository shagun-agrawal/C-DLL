using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
namespace ClassLibrary
{
    class GetColumnFromCSV
    {

        Workbook wb;
        Worksheet ws;
        public int startRow = 1;

        public int startColumn = 1;
        public GetColumnFromCSV(string fileName)
        {
            _Excel.Application excel = new _Excel.Application();
            wb = excel.Workbooks.Open(fileName);
            ws = wb.Worksheets[1];

            Range cell;
            Range xlRange = ws.UsedRange;
            bool flag = true;
           
            for (int i = 1; i < 100; i++)
            {
                for (int j = 1; j < 100; j++)
                {
                    cell = ws.Cells[i, j];
                    if (cell.Value != null)
                    {
                        flag = false;
                        this.startRow = i;
                        this.startColumn = j;
                        break;
                    }

                }
                if (flag == false)
                {
                    break;
                }

            }


        }

      
        public List<string> getColumnValue(string columnName)
        {


            List<string> lst = new List<string>();

            Range cell;
            Range xlRange = ws.UsedRange;
            

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            int row= 1;
            int column = 1;
           
           
            int startRow = this.startRow;
            int startColumn = this.startColumn;
            //for (int i=1;i<100;i++)
            //{
            //    for (int j=1;j<100;j++)
            //    {
            //        cell = ws.Cells[i, j];
            //        if (cell.Value!=null)
            //        {
            //            flag = false;
            //            startRow = i;
            //            startColumn = j;
            //            break;
            //        }

            //    }
            //    if (flag==false)
            //    {
            //        break;
            //    }

            //}
            for (int j=startColumn;j<colCount+startColumn;j++)
                {
                    cell = ws.Cells[startRow, j];
                 
                    if (cell.Value == null)
                    {
                        continue;
                    }
                    else if (columnName == (cell.Value.ToUpper()).ToString())
                    {
                        row = startRow;
                        column = j;
                        
                        break;
                    }
                }
             
           
            for (int i = row+1; i <rowCount+startRow; i++)
            {
                string value;
                cell = ws.Cells[i, column];
                if (cell.Value == null)
                {
                    value = string.Empty;
                }
                else if (columnName=="START" || columnName=="END")
                {
                    value = TimeSpan.FromDays(cell.Value).ToString(@"hh\:mm\:ss");
                }
                else
                {
                    //if(cell.Value.GetType() != typeof(string))
                    //{
                    //    System.Windows.Forms.MessageBox.Show(columnName+" Column's Data Type Is Miss-Matched", "Execution End", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //    Environment.Exit(0);
                    //}

                    value = (cell.Value).ToString();

                }
                lst.Add(value);
              
            }
           
            return lst;
        }
    }
}
