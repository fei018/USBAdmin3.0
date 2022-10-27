using AgentLib;
using System;
using System.Threading.Tasks;

namespace USBAdminService
{
    public class ServerManage_Service
    {
        #region server entity
        public static NamedPipeServer NamedPipeServer { get; set; }

        public static TrayWpfServer TrayServer { get; set; }

        public static PrintJobLogServer PrintJobLogServer { get; set; }

        public static USBFilterFormServer USBFilterServer { get; set; }

        public static ScheduleServer ScheduleServer { get; set; }
        #endregion

        #region OnStart
        public static void OnStart()
        {
            FirstRun();

            NamedPipeServer = new NamedPipeServer();
            NamedPipeServer.Start();

            USBFilterServer = new USBFilterFormServer();
            USBFilterServer.Start();

            TrayServer = new TrayWpfServer();
            TrayServer.Start();
            TrayServer.CreateTray();

            PrintJobLogServer = new PrintJobLogServer();
            PrintJobLogServer.Start();

            ScheduleServer = new ScheduleServer();
            ScheduleServer.Start();

        }
        #endregion

        #region OnStop
        public static void OnStop()
        {
            ScheduleServer?.Stop();

            USBFilterServer?.Stop();

            TrayServer?.Stop();

            PrintJobLogServer?.Stop();

            NamedPipeServer?.Stop();
        }
        #endregion

        #region FirstRun()
        private static void FirstRun()
        {
            if (ToolsHelp.CheckNetworkConnectivity())
            {
                #region PostComputerInfo
                try
                {
                    new AgentHttpHelp().PostComputerInfo();
                }
                catch (Exception ex)
                {
                    AgentLogger.Error("ServerManage_Service.FirstRun():PostComputerInfo:\r\n" + ex.Message);
                }
                #endregion

                #region update setting
                try
                {
                    var http = new AgentHttpHelp();
                    http.UpdateAgentRule();
                    http.UpdateUSBWhitelist();
                }
                catch (Exception ex)
                {
                    AgentLogger.Error("ServerManage_Service.FirstRun():Update Setting:\r\n" + ex.Message);
                }
                #endregion
            }

            #region UsbFilterEnabled Scan_All_USBDisk_To_Filter
            try
            {
                if (AgentRegistry.UsbFilterEnabled)
                {
                    new UsbFilter().Scan_All_USBDisk_To_Filter();
                }
            }
            catch (Exception)
            {
            }
            #endregion
        }
        #endregion
    }
}
