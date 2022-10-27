using System;
using ToolsCommon;

namespace AgentLib
{
    [Serializable]
    public class UsbDisk : UsbBase
    {
        public string DiskDeviceId { get; set; }

        public string DiskPath { get; set; }

        public uint DiskNumber { get; set; }

        public ulong DiskSize { get; set; }

        public string DiskSizeString => UtilityTools.BytesToSizeSuffix((long)DiskSize);

        public bool IsReadOnly { get; set; }

        public string ReadOnly => IsReadOnly ? "True" : null;

        public override string ToString()
        {
            string s = "USB Details :" + Environment.NewLine +
                       Vid_Hex + " - " + Vid + Environment.NewLine +
                       Pid_Hex + " - " + Pid + Environment.NewLine +
                       "SerialNumber: " + SerialNumber + Environment.NewLine +
                       "Manufacturer: " + Manufacturer + Environment.NewLine +
                       "Product: " + Product + Environment.NewLine +
                       "DeviceDescription: " + DeviceDescription + Environment.NewLine +
                       "DeviceId: " + UsbDeviceId + Environment.NewLine + Environment.NewLine;

            return s;
        }

    }
}
