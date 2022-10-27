using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;

namespace AgentLib
{
    public class AgentRegistry
    {
        private const string _agentRegistryKey = "SOFTWARE\\HipHing\\USBAdmin";


        // Registry

        public static string AgentHttpKey
        {
            get => ReadRegKey(nameof(AgentHttpKey));
            set => SetRegKey(nameof(AgentHttpKey), value, RegistryValueKind.String);
        }

        /// <summary>
        /// C:\ProgramData\USBAdmin
        /// </summary>
        public static string AgentDataDir
        {
            get => Environment.ExpandEnvironmentVariables(ReadRegKey(nameof(AgentDataDir)));
            set => SetRegKey(nameof(AgentDataDir), value, RegistryValueKind.String);
        }

        /// <summary>
        /// %ProgramFiles%\USBAdmin\UsbWhitelist.dat
        /// </summary>
        public static string UsbWhitelistPath
        {
            get => Environment.ExpandEnvironmentVariables(ReadRegKey(nameof(UsbWhitelistPath)));
            set => SetRegKey(nameof(UsbWhitelistPath), value, RegistryValueKind.String);
        }

        /// <summary>
        /// %ProgramFiles%\USBAdmin\USBAdminService.exe
        /// </summary>
        public static string USBAdminServicePath
        {
            get => Environment.ExpandEnvironmentVariables(ReadRegKey(nameof(USBAdminServicePath)));
            set => SetRegKey(nameof(USBAdminServicePath), value, RegistryValueKind.String);
        }

        /// <summary>
        /// %ProgramFiles%\USBAdmin\USBAdminTray.exe
        /// </summary>
        public static string USBAdminTrayPath
        {
            get => Environment.ExpandEnvironmentVariables(ReadRegKey(nameof(USBAdminTrayPath)));
            set => SetRegKey(nameof(USBAdminTrayPath), value, RegistryValueKind.String);
        }

        /// <summary>
        /// %ProgramFiles%\USBAdmin\USBAdminFilter.exe
        /// </summary>
        public static string USBAdminFilterPath
        {
            get => Environment.ExpandEnvironmentVariables(ReadRegKey(nameof(USBAdminFilterPath)));
            set => SetRegKey(nameof(USBAdminFilterPath), value, RegistryValueKind.String);
        }


        public static bool UsbFilterEnabled
        {
            get => ReadRegKey(nameof(UsbFilterEnabled)).ToLower() == "true" ? true : false;
            set => SetRegKey(nameof(UsbFilterEnabled), value, RegistryValueKind.String);
        }

        public static bool UsbLogEnabled
        {
            get => ReadRegKey(nameof(UsbLogEnabled)).ToLower() == "true" ? true : false;
            set => SetRegKey(nameof(UsbLogEnabled), value, RegistryValueKind.String);
        }

        public static bool PrintJobLogEnabled
        {
            get => ReadRegKey(nameof(PrintJobLogEnabled)).ToLower() == "true" ? true : false;
            set => SetRegKey(nameof(PrintJobLogEnabled), value, RegistryValueKind.String);
        }

        public static int AgentTimerMinute
        {
            get => Convert.ToInt32(ReadRegKey(nameof(AgentTimerMinute)));
            set => SetRegKey(nameof(AgentTimerMinute), value, RegistryValueKind.String);
        }

        public static string AgentVersion
        {
            get => ReadRegKey(nameof(AgentVersion));
            set => SetRegKey(nameof(AgentVersion), value, RegistryValueKind.String);
        }


        //========== Url

        
        public static string UsbWhitelistUrl
        {
            get => ReadRegKey(nameof(UsbWhitelistUrl));
            set => SetRegKey(nameof(UsbWhitelistUrl), value, RegistryValueKind.String);
        }

        public static string AgentConfigUrl
        {
            get => ReadRegKey(nameof(AgentConfigUrl));
            set => SetRegKey(nameof(AgentConfigUrl), value, RegistryValueKind.String);
        }

        public static string AgentRuleUrl
        {
            get => ReadRegKey(nameof(AgentRuleUrl));
            set => SetRegKey(nameof(AgentRuleUrl), value, RegistryValueKind.String);
        }

        public static string AgentUpdateUrl
        {
            get => ReadRegKey(nameof(AgentUpdateUrl));
            set => SetRegKey(nameof(AgentUpdateUrl), value, RegistryValueKind.String);
        }

        public static string PostComputerInfoUrl
        {
            get => ReadRegKey(nameof(PostComputerInfoUrl));
            set => SetRegKey(nameof(PostComputerInfoUrl), value, RegistryValueKind.String);
        }

        public static string PostUsbLogUrl
        {
            get => ReadRegKey(nameof(PostUsbLogUrl));
            set => SetRegKey(nameof(PostUsbLogUrl), value, RegistryValueKind.String);
        }

        public static string PostUsbRequestUrl
        {
            get => ReadRegKey(nameof(PostUsbRequestUrl));
            set => SetRegKey(nameof(PostUsbRequestUrl), value, RegistryValueKind.String);
        }

        public static string PostPrintJobLogUrl
        {
            get => ReadRegKey(nameof(PostPrintJobLogUrl));
            set => SetRegKey(nameof(PostPrintJobLogUrl), value, RegistryValueKind.String);
        }


        #region + private string ReadRegKey(string name)
        private static string ReadRegKey(string name)
        {
            try
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (var subKey = hklm.OpenSubKey(_agentRegistryKey))
                    {
                        var data = subKey.GetValue(name) as string;

                        if (string.IsNullOrEmpty(data))
                        {
                            throw new Exception("data is NullOrEmpty.");
                        }

                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"AgentRegistry.ReadRegKey({name}): " + ex.Message);
            }
        }
        #endregion

        #region + private static void SetRegKey(string name, object value, RegistryValueKind kind)
        private static void SetRegKey(string name, object value, RegistryValueKind kind)
        {
            try
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (var sub = hklm.CreateSubKey(_agentRegistryKey))
                    {
                        sub.SetValue(name, value, kind);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"AgentRegistry.SetRegKey({name}): " + ex.Message);
            }
        }
        #endregion
    }
}
