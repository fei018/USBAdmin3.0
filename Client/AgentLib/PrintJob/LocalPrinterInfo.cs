using System;
using System.Collections.Generic;
using System.Management;

namespace AgentLib
{
    public class LocalPrinterInfo
    {
        public string Name { get; set; }

        public bool Local { get; set; }

        public bool Network { get; set; }

        public string PortName { get; set; }

        public string ServerName { get; set; }

        public TCPIPPrinterPort TCPIPPort { get; set; }

        public string GetIP()
        {
            return TCPIPPort?.HostAddress;
        }

        public bool IsTCPIPPrinter()
        {
            //string ipRegx = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            if (TCPIPPort != null)
            {
                return true;
            }

            return false;
        }

        #region + private static List<PrinterInfo> GetPrinterList()
        private static List<LocalPrinterInfo> GetPrinterList()
        {
            var printerList = new List<LocalPrinterInfo>();

            ManagementScope mgmtscope = new ManagementScope(@"\root\cimv2");
            var query = new ObjectQuery("Select * from Win32_Printer");

            using (var objsearcher = new ManagementObjectSearcher(mgmtscope, query))
            using (var printers = objsearcher.Get())
            {
                foreach (ManagementObject printer in printers)
                {
                    try
                    {
                        var p = new LocalPrinterInfo()
                        {
                            Name = printer["Name"]?.ToString(),
                            Local = (bool)printer["Local"],
                            Network = (bool)printer["Network"],
                            PortName = printer["PortName"]?.ToString(),
                            ServerName = printer["ServerName"]?.ToString()
                        };

                        printerList.Add(p);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        printer.Dispose();
                    }
                }
            }

            return printerList;
        }
        #endregion

        #region + public static List<PrinterInfo> GetIPPrinterList()
        public static List<LocalPrinterInfo> GetIPPrinterList()
        {
            var ipPrinters = new List<LocalPrinterInfo>();

            var printers = GetPrinterList();
            if (printers.Count <= 0)
            {
                return ipPrinters;
            }

            var tcpIPPorts = TCPIPPrinterPort.GetList();
            if (tcpIPPorts.Count <= 0)
            {
                return ipPrinters;
            }

            foreach (var port in tcpIPPorts)
            {
                var printer = printers.Find(p => p.PortName == port.Name);
                if (printer != null)
                {
                    printer.TCPIPPort = port;
                    ipPrinters.Add(printer);
                }
            }

            return ipPrinters;
        }
        #endregion
    }
}
