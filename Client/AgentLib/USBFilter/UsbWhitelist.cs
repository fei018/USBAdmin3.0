using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsCommon;

namespace AgentLib
{
    public class UsbWhitelist
    {
        private static readonly object _Locker_Whitelist = new object();

        private static string _UsbWhitelistFilePath
        {
            get
            {
                try
                {
                    return AgentRegistry.UsbWhitelistPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usbwhitelist.dat");
                }
                catch (Exception)
                {
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usbwhitelist.dat");
                }
            }
        }

        #region + private static string[] ReadFile_UsbWhitelist()

        private static string[] ReadFile_UsbWhitelist()
        {
            lock (_Locker_Whitelist)
            {
                try
                {
                    if (File.Exists(AgentRegistry.UsbWhitelistPath))
                    {
                        //string read = File.ReadAllText(_UsbWhitelistFilePath, new UTF8Encoding(false));

                        string[] read = File.ReadAllText(_UsbWhitelistFilePath).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        if (read.Length <= 0)
                        {
                            throw new Exception("UsbWhitelist.ReadFile_UsbWhitelist(): usbwhitelist.dat content is empty.");
                        }

                        return read;
                    }
                    else
                    {
                        throw new Exception(_UsbWhitelistFilePath + " not exist.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        #region + private static void WriteFile_UsbWhitelist(string txt)
        private static void WriteFile_UsbWhitelist(string txt)
        {
            lock (_Locker_Whitelist)
            {
                try
                {
                    File.WriteAllText(_UsbWhitelistFilePath, txt);
                }
                catch (Exception ex)
                {
                    throw new Exception(_UsbWhitelistFilePath + "\r\nWrite file Error:\r\n" + ex.GetBaseException().Message);
                }
            }
        }
        #endregion

        #region + public static bool IsFind(UsbBase usb)
        public static bool IsFind(UsbBase usb)
        {
            try
            {
                string[] table = ReadFile_UsbWhitelist();

                foreach (string t in table)
                {
                    if (t == usb.UsbIdentity)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                AgentLogger.Error(ex.Message);
                return false;
            }
        }
        #endregion

        #region + public static void Write_UsbWhitelist_byHttp(UsbFilterDbHttp setting)
        public static void Write_UsbWhitelist_byHttp(string usbWhitelist)
        {
            try
            {
                WriteFile_UsbWhitelist(UtilityTools.Base64Decode(usbWhitelist));
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion      
    }
}
