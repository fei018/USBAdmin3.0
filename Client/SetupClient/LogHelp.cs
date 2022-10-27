using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupClient
{
    public static class LogHelp
    {
        static string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup_log.txt");

        public static void Log(string log)
        {
            try
            {
                Console.WriteLine(log);
                File.AppendAllText(LogPath, log+"\r\n");
            }
            catch (Exception)
            {
            }
        }

        public static void DeleteOldLogFile()
        {
            if (File.Exists(LogPath))
            {
                File.Delete(LogPath);
            }
        }
    }
}
