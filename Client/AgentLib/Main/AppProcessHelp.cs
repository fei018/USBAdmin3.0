using AgentLib.Win32API;
using System;
using System.Diagnostics;
using System.IO;

namespace AgentLib
{
    public class AppProcessHelp
    {
        // static function

        #region + public static Process StartupApp(string appFullPath, string userName=null)
        public static Process StartupApp(string appFullPath, string userName = null)
        {
            try
            {
                var startinfo = new ProcessStartInfo()
                {
                    UserName = userName,
                    FileName = appFullPath
                };

                var proc = Process.Start(startinfo);

                return proc;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region + public static Process StartupAppAsLogonUser(string appFullPath)
        /// <summary>
        ///  startup app as logon user
        /// </summary>
        public static Process StartupAppAsLogonUser(string appFullPath)
        {
            try
            {
                if (!File.Exists(appFullPath))
                {
                    throw new Exception($"AppProcessHelp.StartupProcessAsLogonUser():\r\n{appFullPath}, not exists.");
                }

                Process proc = null;

                var sessionid = ProcessApiHelp.GetCurrentUserSessionID();
                if (sessionid > 0)
                {
                    proc = ProcessApiHelp.CreateProcessAsUser(appFullPath, null);
                }

                return proc;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
