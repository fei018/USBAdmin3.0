using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBAdminTray
{
    public class ServerManage_Tray
    {
        #region server entity
        public static NamedPipeClient_Tray NamedPipeClient_Tray { get; set; }

        public static TrayIcon TrayIcon { get; set; }
        #endregion

        #region Start
        public static void Start()
        {
            try
            {
                NamedPipeClient_Tray = new NamedPipeClient_Tray();
                NamedPipeClient_Tray.Start();
            }
            catch (Exception)
            {
            }

            try
            {
                TrayIcon = new TrayIcon();
                TrayIcon.Start();
            }
            catch (Exception)
            {
            }

        }
        #endregion

        #region Stop
        public static void Stop()
        {
            try
            {
                TrayIcon.Stop();
            }
            catch (Exception)
            {
            }

            try
            {
                NamedPipeClient_Tray.Stop();
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
