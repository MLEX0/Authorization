using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthorizationStudy
{
    public class FileSaveClass
    {
        public static void FileWrite(string Data, string FilePath)
        {
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.WriteLine(Data);
            }
        }

        public static string FileRead(string FilePath)
        {
            string Data = "";

            using (StreamReader sr = new StreamReader(FilePath))
            {
                Data = sr.ReadLine();
            }

            return Data;
        }

    }
}
