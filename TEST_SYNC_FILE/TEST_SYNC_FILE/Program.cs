using BlinkSyncLib;
using System;
using System.IO;
using System.Linq;

namespace TEST_SYNC_FILE
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var thisFolder = System.IO.Path.GetDirectoryName(exePath);

                ConfigReader reader = new ConfigReader(thisFolder + @"\Config.ini");

                string sourceFolder = reader.IniReadString("CONFIG", "source");
                string destFolder = reader.IniReadString("CONFIG", "destination");
                string interval = reader.IniReadString("CONFIG", "interval");


                DateTime today_date = DateTime.Now;
                StreamWriter Trace = new StreamWriter("LogMsj.txt", append: true);

                if (Directory.Exists(destFolder))
                {
                   
                    var directory = new DirectoryInfo(sourceFolder);

                    DateTime date = today_date.AddDays(Convert.ToDouble(interval));
                    var files = directory.GetFiles().Where(file => file.LastWriteTime < date);
                    
                    Sync sync = new Sync(sourceFolder, destFolder);
                    sync.Start();

                    using (Trace)
                    {
                        Trace.WriteLine(today_date + " " + "Successfully");
                        Trace.Close();

                    }

                    foreach (FileInfo file in files)
                    {
                        Console.WriteLine(file.LastWriteTime);
                        File.Delete(file.FullName);
                    }
                    
                }

                else //if (!Directory.Exists(destFolder))
                {
                    using (Trace)
                    {
                        Trace.WriteLine(today_date + " " + "Destination not exixts");

                        Trace.Close();
                    }
                }
             
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong Input");
            }

        }
    }
}

