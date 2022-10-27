using AgentLib;
using System;
using System.Windows;

namespace USBAdminTray
{
    public class TrayIcon
    {

        private static System.Windows.Forms.NotifyIcon _TrayIcon;

        #region + public void Stop()
        public void Stop()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (_TrayIcon != null)
                {
                    try
                    {
                        _TrayIcon.Visible = false;
                        _TrayIcon.ContextMenuStrip?.Dispose();
                        _TrayIcon.Icon?.Dispose();

                        _TrayIcon?.Dispose();
                        _TrayIcon = null;
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }
        #endregion

        #region + public void Start()
        public void Start()
        {
            try
            {
                Stop();

                _TrayIcon = new System.Windows.Forms.NotifyIcon
                {
                    Icon = Properties.Resources.icon,
                    Text = "USBAdmin"
                };

                _TrayIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

                _TrayIcon.ContextMenuStrip.Items.Add("USB Disk List", null, OpenUSBListWindow);
                _TrayIcon.ContextMenuStrip.Items.Add("Update Setting", null, UpdateSettingItem_Click);
                _TrayIcon.ContextMenuStrip.Items.Add("About", null, AboutItem_Click);
                //_TrayIcon.ContextMenuStrip.Items.Add("Close", null, CloseTrayItem_Click);
                _TrayIcon.ContextMenuStrip.Items.Add("");

                _TrayIcon.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }
        #endregion

        // Tray Item Click

        #region OpenUSBListWindow
        private void OpenUSBListWindow(object sender, EventArgs e)
        {
            try
            {
                if (!UniqueOpenWindow.USBListWindow)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        USBListWindow win = new USBListWindow();
                        win.Show();
                    });

                    UniqueOpenWindow.USBListWindow = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region UpdateSettingItem_Click
        private void UpdateSettingItem_Click(object sender, EventArgs e)
        {
            try
            {
                ServerManage_Tray.NamedPipeClient_Tray.SendMsg_UpdateSetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region AboutItem_Click
        private void AboutItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (UniqueOpenWindow.AoutWindow)
                {
                    return;
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    AboutWindow about = new AboutWindow();
                    about.txtAgentVersion.Text = AgentRegistry.AgentVersion;
                    about.Show();

                    UniqueOpenWindow.AoutWindow = true;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region CloseTrayItem_Click
        private void CloseTrayItem_Click(object sender, EventArgs e)
        {
            try
            {
                App.Current.Dispatcher.Invoke(new Action(() =>
                {
                    App.Current.MainWindow.Close();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        // BalloonTip
        #region BalloonTip
        public void ShowBalloonTip(string text)
        {
            try
            {
                if (_TrayIcon != null)
                {
                    _TrayIcon.BalloonTipText = text;
                    _TrayIcon.BalloonTipTitle = "Notice:";
                    _TrayIcon.ShowBalloonTip(10000); // 10s
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
