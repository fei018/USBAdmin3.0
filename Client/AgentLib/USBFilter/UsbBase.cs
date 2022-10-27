﻿using System;
using ToolsCommon;

namespace AgentLib
{
    [Serializable]
    public class UsbBase : IUsbBase
    {
        public int Vid { get; set; }

        public string Vid_Hex => "0x_" + Vid.ToString("X").PadLeft(4, '0');

        public int Pid { get; set; }

        public string Pid_Hex => "0x_" + Pid.ToString("X").PadLeft(4, '0');

        public string SerialNumber { get; set; }

        public string Manufacturer { get; set; }

        public string Product { get; set; }

        public string DeviceDescription { get; set; }

        public string UsbDevicePath { get; set; }

        public string UsbDeviceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Lower case string</returns>
        public string UsbIdentity => (Vid.ToString() + Pid.ToString() + SerialNumber).ToLower();
    }
}
