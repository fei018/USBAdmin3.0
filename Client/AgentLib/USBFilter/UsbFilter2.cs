﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using AgentLib.Win32API;

namespace AgentLib
{
    public partial class UsbFilter
    {

        #region + private UsbDisk Get_UsbDisk_DiskPath_by_DriveLetter_WMI(char driveLetter)
        /// <summary>
        /// NotifyUSB 可能為 Null
        /// </summary>
        /// <param name="driveLetter"></param>
        /// <returns>DiskPath, DiskNumber</returns>
        //private UsbDisk Get_UsbDisk_DiskPath_by_DriveLetter_WMI(char driveLetter)
        //{
        //    try
        //    {
        //        var usb = new UsbDisk();

        //        var scope = new ManagementScope(@"\\.\ROOT\Microsoft\Windows\Storage");
        //        var query = new ObjectQuery($"SELECT * FROM MSFT_Partition WHERE DriveLetter='{driveLetter}'");
        //        //var query = new ObjectQuery($"SELECT * FROM MSFT_Partition");
        //        using (var searcher = new ManagementObjectSearcher(scope, query))
        //        {
        //            using (var partitions = searcher.Get())
        //            {
        //                foreach (ManagementObject p in partitions)
        //                {
        //                    var number = Convert.ToUInt32(p["DiskNumber"]);
        //                    var diskId = Convert.ToString(p["DiskId"]);

        //                    usb.DiskPath = diskId;
        //                    usb.DiskNumber = number;
        //                    usb.DriveLetter = driveLetter.ToString() + ":";
        //                }
        //            }
        //        }
        //        return usb;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion

        #region + private UsbDisk Get_UsbDisk_by_DiskNumber_WMI(uint diskNumber)
        /// <summary>
        /// get disk info: Path, Number, Size
        /// </summary>
        /// <returns></returns>
        private UsbDisk Get_UsbDisk_by_DiskNumber_WMI(uint diskNumber)
        {
            try
            {
                var scope = new ManagementScope(@"\\.\ROOT\Microsoft\Windows\Storage");
                var query = new ObjectQuery($"SELECT * FROM MSFT_Disk WHERE Number={diskNumber}");
                using (var searcher = new ManagementObjectSearcher(scope, query))
                {
                    using (var disks = searcher.Get())
                    {
                        // one disk only
                        foreach (ManagementObject disk in disks)
                        {
                            return new UsbDisk
                            {
                                DiskPath = Convert.ToString(disk["Path"]),
                                DiskNumber = diskNumber,
                                DiskSize = Convert.ToUInt64(disk["Size"]),
                                IsReadOnly = Convert.ToBoolean(disk["IsReadOnly"])
                            };
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region + private void Set_Disk_IsReadOnly_by_DiskNumber(uint diskNumber, bool isReadOnly)
        private void Set_Disk_IsReadOnly_by_DiskNumber(uint diskNumber, bool isReadOnly)
        {
            try
            {
                var scope = new ManagementScope(@"\\.\ROOT\Microsoft\Windows\Storage");
                var query = new ObjectQuery($"SELECT * FROM MSFT_Disk WHERE Number={diskNumber}");
                using (var searcher = new ManagementObjectSearcher(scope, query))
                {
                    using (var disks = searcher.Get())
                    {
                        foreach (ManagementObject disk in disks)
                        {
                            bool IsReadOnly = bool.Parse(disk["IsReadOnly"].ToString());
                            bool IsSystem = bool.Parse(disk["IsSystem"].ToString());
                            bool IsBoot = bool.Parse(disk["IsBoot"].ToString());

                            if (IsReadOnly != isReadOnly && !IsBoot && !IsSystem)
                            {
                                var inParams = disk.GetMethodParameters("SetAttributes");
                                inParams["IsReadOnly"] = isReadOnly;
                                var result = disk.InvokeMethod("SetAttributes", inParams, null)["ReturnValue"].ToString();
                                if (!string.IsNullOrWhiteSpace(result))
                                {
                                    AgentLogger.Error("DiskNumber: " + diskNumber + "\r\n" + "Set readOnly result: \r\n" + result);
                                }
                            }
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

        #region + private void Set_Disk_IsReadOnly_by_DiskPath_WMI(string diskPath, bool isReadOnly)
        /// <summary>
        /// need admin right to set read only
        /// </summary>
        /// <param name="diskPath"></param>
        /// <param name="isReadOnly"></param>
        public void Set_Disk_IsReadOnly_by_DiskPath_WMI(string diskPath, bool isReadOnly)
        {
            try
            {
                string path = diskPath.TrimStart('\\', '\\', '?', '\\');
                ManagementScope scope = new ManagementScope(@"\\.\ROOT\Microsoft\Windows\Storage");
                ObjectQuery query = new ObjectQuery($"SELECT * FROM MSFT_Disk WHERE Path LIKE '%{path}'");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    using (ManagementObjectCollection disks = searcher.Get())
                    {
                        foreach (ManagementObject d in disks)
                        {
                            string result = Set_Disk_IsReadOnly_WMI(d, isReadOnly);
                            if (!string.IsNullOrWhiteSpace(result) && result != "0")
                            {
                                AgentLogger.Error(diskPath + "\r\n" + "Set ReadOnly result:\r\n" + result);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UsbFilter.Set_Disk_IsReadOnly_by_DiskPath_WMI():\r\n" + ex.Message);
            }
        }

        private string Set_Disk_IsReadOnly_WMI(ManagementObject disk, bool isReadOnly)
        {
            if (disk == null) return null;

            bool IsReadOnly = bool.Parse(disk["IsReadOnly"].ToString());
            bool IsSystem = bool.Parse(disk["IsSystem"].ToString());
            bool IsBoot = bool.Parse(disk["IsBoot"].ToString());

            if (IsReadOnly != isReadOnly && !IsBoot && !IsSystem)
            {
                ManagementBaseObject inParams = disk.GetMethodParameters("SetAttributes");
                inParams["IsReadOnly"] = isReadOnly;
                return Convert.ToString(disk.InvokeMethod("SetAttributes", inParams, null)["ReturnValue"]);
            }
            else
            {
                return null;
            }
        }
        #endregion


        #region + private List<UsbDisk> Get_All_UsbDisk_by_BusType_USB_WMI()
        /// <summary>
        /// get disk info: Path, Number, Size
        /// </summary>
        /// <returns></returns>
        private List<UsbDisk> Get_All_UsbDisk_by_BusType_USB_WMI()
        {
            List<UsbDisk> list = new List<UsbDisk>();

            ManagementScope scope = new ManagementScope(@"\\.\ROOT\Microsoft\Windows\Storage");
            ObjectQuery query = new ObjectQuery("SELECT * FROM MSFT_Disk where BusType=7"); // "BusType=7": USB Bus
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            {
                using (ManagementObjectCollection disks = searcher.Get())
                {
                    foreach (ManagementObject disk in disks)
                    {
                        UsbDisk usb = new UsbDisk
                        {
                            DiskPath = Convert.ToString(disk["Path"]),
                            DiskNumber = Convert.ToUInt32(disk["Number"]),
                            DiskSize = Convert.ToUInt64(disk["Size"]),
                            IsReadOnly = Convert.ToBoolean(disk["IsReadOnly"])
                        };

                        list.Add(usb);
                    };
                }
            }

            return list;
        }
        #endregion


        #region SetupDi

        #region + Get_UsbDeviceId_By_DiskPath_SetupDi(UsbDisk usbDisk)
        /// <summary>
        /// UsbDisk usbDisk 需要賦值 usbDisk.DiskPath <br />
        /// 只匹配 DeviceId format: ^USB\xxxx 
        /// </summary>
        /// <returns>UsbDeviceId, DiskDeviceId</returns>
        private bool Get_UsbDeviceId_By_DiskPath_SetupDi(UsbDisk usbDisk)
        {
            Guid interfaceGuid = USetupApi.GUID_DEVINTERFACE.GUID_DEVINTERFACE_DISK;

            int dicfg = (int)(USetupApi.DICFG.PRESENT | USetupApi.DICFG.DEVICEINTERFACE);

            var devInfoSet = USetupApi.SetupDiGetClassDevs(ref interfaceGuid, IntPtr.Zero, IntPtr.Zero, dicfg);

            try
            {
                if (devInfoSet == USetupApi.INVALID_HANDLE_VALUE)
                    return false;

                bool success = true;
                int index = 0;
                while (success)
                {
                    var devInterfaceData = new USetupApi.SP_DEVICE_INTERFACE_DATA();
                    devInterfaceData.cbSize = (uint)Marshal.SizeOf(devInterfaceData);

                    success = USetupApi.SetupDiEnumDeviceInterfaces(devInfoSet, IntPtr.Zero, ref interfaceGuid, index++, ref devInterfaceData);
                    if (success)
                    {
                        bool isDetail = USetupApi.SetupDiGetDeviceInterfaceDetail(devInfoSet, ref devInterfaceData, out string devPath,
                                                                                out USetupApi.SP_DEVINFO_DATA devInfoData);
                        if (isDetail)
                        {
                            // match enum path and notify path
                            if (devPath.ToLower() == usbDisk.DiskPath.ToLower())
                            {
                                usbDisk.DiskDeviceId = Get_DevInterface_DeviceId(devInfoData.devInst);
                                usbDisk.UsbDeviceId = Get_UsbDeviceId_Use_Recursion_ParentDev(devInfoData.devInst);
                                if (string.IsNullOrEmpty(usbDisk.UsbDeviceId))
                                {
                                    return false;
                                }
                                else
                                {
                                    return true; // 結束循環
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (Win32Exception)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (devInfoSet != USetupApi.INVALID_HANDLE_VALUE)
                {
                    USetupApi.SetupDiDestroyDeviceInfoList(devInfoSet);
                }
            }
        }
        #endregion

        #region + Get_DevInterface_DeviceId(uint devInst)
        private string Get_DevInterface_DeviceId(uint devInst)
        {
            if (USetupApi.CM_Get_Device_ID_Size(out uint size, devInst, 0) == 0)
            {
                StringBuilder deviceID = new StringBuilder { Length = (int)size };

                if (USetupApi.CM_Get_Device_ID(devInst, deviceID, size, 0) == 0)
                {
                    return deviceID.ToString();
                }
            }
            return null;
        }
        #endregion

        #region + Get_UsbDeviceId_Use_Recursion_ParentDev(uint devInst)
        /// <summary>
        /// 遞歸 查詢 符合 USB\VID_0000&PID_FFFF 路徑格式的 DeviceId
        /// </summary>
        /// <param name="devInst"></param>
        /// <returns></returns>
        private string Get_UsbDeviceId_Use_Recursion_ParentDev(uint devInst)
        {
            if (USetupApi.CM_Get_Parent(out uint parentInst, devInst, 0) == 0)
            {
                if (USetupApi.CM_Get_Device_ID_Size(out uint size, parentInst, 0) == 0)
                {
                    StringBuilder deviceID = new StringBuilder { Length = (int)size };

                    if (USetupApi.CM_Get_Device_ID(parentInst, deviceID, size, 0) == 0)
                    {
                        var regex = RegexDeviceIdPrefix_USB(deviceID.ToString());

                        if (!string.IsNullOrEmpty(regex))
                        {
                            return regex;
                        }

                        return Get_UsbDeviceId_Use_Recursion_ParentDev(parentInst);
                    }
                }
            }

            return null;
        }

        private string RegexDeviceIdPrefix_USB(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            var match = Regex.Match(path, "^USB\\\\VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return path.TrimEnd();
            }

            // 匹配路徑高於 USB 返回
            if (Regex.Match(path, "^PCI", RegexOptions.IgnoreCase).Success)
            {
                return null;
            }
            if (Regex.Match(path, "^ACPI", RegexOptions.IgnoreCase).Success)
            {
                return null;
            }
            if (Regex.Match(path, "^ROOT", RegexOptions.IgnoreCase).Success)
            {
                return null;
            }

            return null;
        }
        #endregion
        #endregion
    }
}
