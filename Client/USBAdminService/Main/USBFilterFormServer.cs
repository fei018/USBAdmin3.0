using AgentLib;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace USBAdminService
{
    public class USBFilterFormServer
    {
        private Process _usbFormProcess;

        public void Start()
        {
            Stop();

            try
            {
                _usbFormProcess = AppProcessHelp.StartupApp(AgentRegistry.USBAdminFilterPath);
            }
            catch (Exception ex)
            {
                AgentLogger.Error("USBFilterFormServer.Start(): " + ex.GetBaseException().Message);
            }
        }

        public void Stop()
        {
            try
            {
                if (_usbFormProcess != null)
                {
                    Task.Factory.StartNew(() =>
                    {
                        ServerManage_Service.NamedPipeServer?.SendMsg_To_USBFilterForm_Close();
                    });

                    Thread.Sleep(5000);

                    if (!_usbFormProcess.HasExited)
                    {
                        _usbFormProcess?.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                AgentLogger.Error("USBFilterFormServer.Stop(): " + ex.GetBaseException().Message);
            }
        }
    }
}
