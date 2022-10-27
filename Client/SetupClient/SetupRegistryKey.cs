using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace SetupClient
{
    public class SetupRegistryKey
    {
        static Dictionary<string, string> Get_USBAdminKeys()
        {
            try
            {
                var keys = new Dictionary<string, string>()
                {
                    {"AgentDataDir",@"%ProgramData%\USBAdmin"},
                    {"UsbWhitelistPath",@"%ProgramFiles%\USBAdmin\UsbWhitelist.dat"},
                    {"USBAdminServicePath",@"%ProgramFiles%\USBAdmin\USBAdminService.exe"},
                    {"USBAdminTrayPath",@"%ProgramFiles%\USBAdmin\USBAdminTray.exe"},
                    {"USBAdminFilterPath",@"%ProgramFiles%\USBAdmin\USBAdminFilter.exe"},
                    {"UsbFilterEnabled","false"},
                    {"UsbLogEnabled","false"},
                    {"PrintJobLogEnabled","false"},
                    {"AgentTimerMinute","10"},
                    {"AgentHttpKey","usbb50ae7e95f144874a2739e119e8791e1"},
                    {"UsbWhitelistUrl","http://hhdmstest02.hiphing.com.hk/USBAdmin/ClientGet/UsbWhitelist"},
                    {"AgentConfigUrl","http://hhdmstest02.hiphing.com.hk/USBAdmin/ClientGet/AgentConfig"},
                    {"AgentRuleUrl","http://hhdmstest02.hiphing.com.hk/USBAdmin/ClientGet/AgentRule"},
                    {"AgentUpdateUrl","http://hhdmstest02.hiphing.com.hk/USBAdmin/ClientGet/AgentUpdate"},
                    {"PostUsbRequestUrl","http://hhdmstest02.hiphing.com.hk/USBAdmin/ClientPost/PostUsbRequest"},
                    {"PostComputerInfoUrl","http://hhdmstest02.hiphing.com.hk/USBAdmin/ClientPost/PostComputerInfo"},
                    {"PostUsbLogUrl","http://hhdmstest02.hiphing.com.hk/USBAdmin/ClientPost/PostUsbLog"},
                    {"PostPrintJobLogUrl","http://hhdmstest02.hiphing.com.hk/USBAdmin/ClientPost/PostPrintJobLog"}
                };

                keys.Add("AgentVersion", "3.0.0");

                return keys;
            }
            catch (Exception ex)
            {
                throw new Exception("SetupRegistryKey.Get_USBAdminKeys(): " + ex.Message);
            }
        }


        static string _registryKeyLocation = "SOFTWARE\\HipHing\\USBAdmin";

        #region + public void InitialRegistryKey()
        public static void InitialRegistryKey()
        {
            try
            {
                var keys = Get_USBAdminKeys();

                // Registry key location: Computer\HKEY_LOCAL_MACHINE
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    // delete old key
                    hklm.DeleteSubKey(_registryKeyLocation, false);

                    using (var usbKey = hklm.CreateSubKey(_registryKeyLocation, true))
                    {
                        foreach (var s in keys)
                        {
                            usbKey.SetValue(s.Key, s.Value, RegistryValueKind.String);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public static void DeleteRegKey_HHITtools()
        {
            string key = "SOFTWARE\\HipHing\\HHITtools";

            try
            {
                using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    // delete old key
                    hklm.DeleteSubKey(key, false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
