﻿using System;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using ToolsCommon;

namespace AgentLib
{
    public class ComputerInfo : IComputerInfo
    {
        public string HostName { get; set; }

        public string Domain { get; set; }

        public string BiosSerial { get; set; }

        public string MacAddress { get; set; }

        public string IPAddress { get; set; }

        public string IPv4Mask { get; set; }

        public string NetworkAddress { get; set; }      

        public bool UsbFilterEnabled { get; set; }

        public string AgentVersion { get; set; }

        public string UserName { get; set; }

        public string ComputerIdentity => BiosSerial?.ToLower();

        public override string ToString()
        {
            return "HostName: " + HostName + "\r\n" +
                   "Domain: " + Domain + "\r\n" +
                   "BiosSerial: " + BiosSerial + "\r\n" +
                   "IPAddress: " + IPAddress + "\r\n" +
                   "MacAddress: " + MacAddress + "\r\n";
        }
    }
}
