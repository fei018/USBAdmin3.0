using AgentLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace USBAdminService
{
    public class TrayWpfServer : IAdminServer
    {
        private static List<Process> _trayProcessList;

        public void Start()
        {
            try
            {
                _trayProcessList = new List<Process>();
            }
            catch (Exception ex)
            {
                AgentLogger.Error("TrayWpfServer.Start(): " + ex.GetBaseException().Message);
            }
        }

        public void Stop()
        {
            try
            {
                if (_trayProcessList != null)
                {
                    foreach (Process p in _trayProcessList)
                    {
                        try
                        {
                            Task.Factory.StartNew(() =>
                            {
                                ServerManage_Service.NamedPipeServer.SendMsg_To_Tray_Close();
                            });

                            Thread.Sleep(5000);

                            if (!p.HasExited)
                            {
                                p?.Kill();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    _trayProcessList.Clear();
                    _trayProcessList = null;
                }
            }
            catch (Exception ex)
            {
                AgentLogger.Error("TrayWpfServer.Stop(): " + ex.GetBaseException().Message);
            }
        }

        public void CreateTray()
        {
            try
            {
                if (_trayProcessList == null)
                {
                    _trayProcessList = new List<Process>(); 
                }

                Process proc = AppProcessHelp.StartupAppAsLogonUser(AgentRegistry.USBAdminTrayPath);

                _trayProcessList.Add(proc);
            }
            catch (Exception)
            {
            }
        }
    }
}
