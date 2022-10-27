﻿using System;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using ToolsCommon;

namespace AgentLib
{
    public class ComputerInfoHelp
    {
        #region + public static string GetComputerIdentity()
        public static string GetComputerIdentity()
        {
            try
            {
                var com = new ComputerInfo();
                com.HostName = IPGlobalProperties.GetIPGlobalProperties().HostName;
                SetBiosSerial(com);

                if (string.IsNullOrWhiteSpace(com.ComputerIdentity))
                {
                    throw new Exception("ComputerIdentity is null or empty.");
                }
                return com.ComputerIdentity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region + public static ComputerInfo GetComputerInfo()
        public static ComputerInfo GetComputerInfo()
        {
            try
            {
                ComputerInfo userComputer = new ComputerInfo();
                userComputer.AgentVersion = AgentRegistry.AgentVersion;
                userComputer.HostName = IPGlobalProperties.GetIPGlobalProperties().HostName;
                userComputer.Domain = IPGlobalProperties.GetIPGlobalProperties().DomainName;
                //try
                //{
                //    userComputer.Domain = System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain().Name;
                //}
                //catch (Exception) { }

                SetNetworkInfo(userComputer);
                SetBiosSerial(userComputer);
                SetUserName(userComputer);

                return userComputer;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region + public static string GetSubnetAddr()
        public static string GetSubnetAddr()
        {
            try
            {
                var com = new ComputerInfo();
                SetNetworkInfo(com);
                return com.NetworkAddress;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region + public static void SetNetworkInfo()
        public static void SetNetworkInfo(ComputerInfo com)
        {
            NetworkInterface nic = NetworkInterface.GetAllNetworkInterfaces()
                                                .Where(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                                                .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up);

            //如果 wire nic 搵唔到, 嘗試搵 wireless nic
            if (nic == null)
            {
                nic = NetworkInterface.GetAllNetworkInterfaces()
                                    .Where(n => n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                                    .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up);
            }

            if (nic == null) throw new Exception("Cannot find ip address");

            // Set MAC Address            
            com.MacAddress = GetMACAddress(nic);

            UnicastIPAddressInformation ipV4 = nic.GetIPProperties().UnicastAddresses.First(n => n.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

            // set IP Address
            com.IPAddress = ipV4.Address.ToString();

            // IPv4Mask
            com.IPv4Mask = ipV4.IPv4Mask.ToString();

            // set NetwordAddress
            com.NetworkAddress = UtilityTools.GetNetworkAddress(ipV4.Address, ipV4.IPv4Mask).ToString();
        }
        #endregion

        #region + private static void SetBiosSerial()
        /// <summary>
        /// if Bios Serial is null, to get mainboard serial
        /// </summary>
        /// <param name="userComputer"></param>
        private static void SetBiosSerial(ComputerInfo userComputer)
        {
            using (ManagementObjectSearcher ComSerial = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS"))
            {
                using (var wmi = ComSerial.Get())
                {
                    foreach (var b in wmi)
                    {
                        userComputer.BiosSerial = Convert.ToString(b["SerialNumber"])?.Trim();
                        b?.Dispose();
                    }
                }
            }

            if (string.IsNullOrEmpty(userComputer.BiosSerial))
            {
                using (ManagementObjectSearcher ComSerial = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
                {
                    using (var wmi = ComSerial.Get())
                    {
                        foreach (var b in wmi)
                        {
                            userComputer.BiosSerial = Convert.ToString(b["SerialNumber"])?.Trim();
                            b?.Dispose();
                        }
                    }
                }
            }
        }
        #endregion

        #region + private static void SetUserName(ComputerInfo userComputer)
        private static void SetUserName(ComputerInfo userComputer)
        {
            using (ManagementObjectSearcher comSystem = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
            {
                using (var wmi = comSystem.Get())
                {
                    foreach (var b in wmi)
                    {
                        userComputer.UserName = Convert.ToString(b["UserName"])?.Trim();
                        b?.Dispose();
                    }
                }
            }
        }
        #endregion

        #region + private static string GetMACAddress(NetworkInterface nic)
        private static string GetMACAddress(NetworkInterface nic)
        {
            StringBuilder mac = new StringBuilder();
            byte[] bytes = nic.GetPhysicalAddress().GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                // Display the physical address in hexadecimal.
                mac.Append(bytes[i].ToString("X2"));
                // Insert a hyphen after each byte, unless we are at the end of the address.
                if (i != bytes.Length - 1) mac.Append("-");
            }
            return mac.ToString();
        }
        #endregion

    }
}
