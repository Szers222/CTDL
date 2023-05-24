using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDoAnCTDLvsGiaiThuat.Services
{
    public static class IOFIle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <param name="arrString"></param>
        public static void WriteFile(string link, string[] arrString)
        {
            if (arrString.Length > 0)
            {
                StreamWriter streamWriter;
                if (File.Exists(link))
                {
                    File.SetAttributes(link, FileAttributes.Normal);
                    streamWriter = new StreamWriter(link);
                }
                else
                {
                    streamWriter = new StreamWriter(link);
                    File.SetAttributes(link, FileAttributes.Normal);
                }
                for (int i = 0; i < arrString.Length - 1; i++)
                {
                    streamWriter.WriteLine(arrString[i]);
                }
                streamWriter.Write(arrString[arrString.Length - 1]);
                streamWriter.Close();
                File.SetAttributes(link, FileAttributes.ReadOnly);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public static string[] ReadFile(string link)
        {
            try
            {
                string[] arrString = new string[0];
                StreamReader streamReader = new StreamReader(link);
                string str;
                while ((str = streamReader.ReadLine()) != null)
                {
                    Array.Resize(ref arrString, arrString.Length + 1);
                    arrString[arrString.Length - 1] = str;
                }
                streamReader.Close();
                return arrString;
            }
            catch
            {
                return null;
            }
        }
    }
}
