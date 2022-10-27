using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgentLib
{
    public class AgentLogger
    {
        private static readonly string _baseDir = GetLogDir();


        //private static string LogPath => Path.Combine(_baseDir, "log.txt");

        private static string ErrorPath => Path.Combine(_baseDir, "error.txt");

        private static string GetLogDir()
        {
            try
            {
                return AgentRegistry.AgentDataDir;
            }
            catch (Exception)
            {
                return Environment.ExpandEnvironmentVariables("%ProgramData%\\USBAdmin");
            }
        }

        public static void Error(string error)
        {
            LogToFile(ErrorPath, error);
        }

        private readonly static object _locker = new object();
        private const string _lockerGuid = "91825b3e-7810-475d-9559-baf831952561";

        static void LogToFile(string path, string log)
        {
            CheckSize();

            if (!File.Exists(_baseDir))
            {
                Directory.CreateDirectory(_baseDir);
            }

            Task.Run(() =>
            {
                // lock multi process write one file
                Mutex mutex = null;
                try
                {
                    mutex = new Mutex(false, _lockerGuid);
                    mutex.WaitOne();

                    var l = Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + log + Environment.NewLine;
                    File.AppendAllText(path, l);
                }
                catch (Exception)
                {
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            });         
        }

        private static void CheckSize()
        {
            try
            {
                FileInfo file = new FileInfo(ErrorPath);
                if (file.Exists)
                {
                    if (file.Length > 20971520) // unit byte , 20 MB = 20971520 Bytes
                    {
                        file.Delete();
                    }
                }

                file = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
