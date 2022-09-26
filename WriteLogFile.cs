using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ClassLibrary
{
    public class WriteLogFile
    {
        LogFile logobj;
       public WriteLogFile()
        {
            FetchConfigData obj1 = new FetchConfigData();
            string configFileName = "ClassLibrarySettings";

            logobj = obj1.Fetch_LogFile_Name_And_Path(configFileName);
        }
        public bool WriteLog(string strMessage)
        {
            try
            {
                //Console.WriteLine(Path.GetTempPath());
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", logobj.get_Log_FilePath(), logobj.get_Log_FileName()), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(String.Format("{0}           {1}", DateTime.Now, strMessage));
               
                objStreamWriter.Close();
                objFilestream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
