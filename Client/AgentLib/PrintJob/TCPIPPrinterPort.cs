using System;
using System.Collections.Generic;
using System.Management;

namespace AgentLib
{
    public class TCPIPPrinterPort
    {
        public string Name { get; set; }

        public string HostAddress { get; set; }

        public string PortNumber { get; set; }


        #region + public static List<TCPIPPrinterPort> GetList()
        public static List<TCPIPPrinterPort> GetList()
        {
            var tcpIPProtList = new List<TCPIPPrinterPort>();

            ManagementScope mgmtscope = new ManagementScope(@"\root\cimv2");
            var query = new ObjectQuery("Select * from Win32_TCPIPPrinterPort");

            using (ManagementObjectSearcher objsearcher = new ManagementObjectSearcher(mgmtscope, query))
            using (var tcps = objsearcher.Get())
            {
                foreach (ManagementObject tcp in tcps)
                {
                    try
                    {
                        var t = new TCPIPPrinterPort()
                        {
                            Name = tcp["Name"]?.ToString(),
                            HostAddress = tcp["HostAddress"]?.ToString(),
                            PortNumber = tcp["PortNumber"]?.ToString()
                        };

                        tcpIPProtList.Add(t);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return tcpIPProtList;
        }
        #endregion
    }
}
