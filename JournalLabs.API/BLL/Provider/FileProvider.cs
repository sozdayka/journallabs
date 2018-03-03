using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JournalLabs.API.BLL.Provider
{
    public class FileProvider
    {
        public void Write(string fileName,string data)
        {
            //string path = $@"{System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.RelativeSearchPath ?? "")}\{fileName}" ;
            //if (File.Exists(path))
            //{
            //    using (TextWriter tw = new StreamWriter(path,true))
            //    {
            //        tw.WriteLine(data);
            //        tw.Close();
            //    }
            //}
            //if (!File.Exists(path))
            //{
            //    File.Create(path).Dispose();
            //    using (TextWriter tw = new StreamWriter(path))
            //    {
            //        tw.WriteLine(data);
            //        tw.Close();
            //    }
            //}
        }

    }
}