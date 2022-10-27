using AgentLib;
using NamedPipeWrapper;
using System;
using System.Windows;
using System.Linq;

namespace USBAdminTray
{
    public class NamedPipeClient_Tray
    {
        // private
        private string _pipeName;

        private NamedPipeClient<NamedPipeMsg> _client;


        #region Stop()
        public void Stop()
        {
            try
            {
                if (_client != null)
                {
                    _client.ServerMessage -= _client_ServerMessage;
                    _client.Stop();
                    _client = null;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Start()
        public void Start()
        {
            try
            {
                Stop();

                _pipeName = AgentRegistry.AgentHttpKey;

                if (string.IsNullOrWhiteSpace(_pipeName))
                {
                    AgentLogger.Error("NamedPipeClient_Tray.PipeName is empty");
                    return;
                }

                _client = new NamedPipeClient<NamedPipeMsg>(_pipeName);
                _client.AutoReconnect = true;

                _client.ServerMessage += _client_ServerMessage;
                _client.Error += _client_Error;

                _client.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private void _client_Error(Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
        #endregion

        #region _client_ServerMessage
        private void _client_ServerMessage(NamedPipeConnection<NamedPipeMsg, NamedPipeMsg> connection, NamedPipeMsg pipeMsg)
        {
            if (pipeMsg == null)
            {
                MessageBox.Show("NamedPipeClient_Tray ServerMessage is null.");
            }

            switch (pipeMsg.MsgType)
            {
                case NamedPipeMsgType.MsgBox_TrayHandle:
                    Msg_TrayHandle(pipeMsg?.Message);
                    break;

                case NamedPipeMsgType.BalloonTip_TrayHandle:
                    BalloonTip_TrayHandle(pipeMsg?.Message);
                    break;

                case NamedPipeMsgType.UsbNotRegister_TrayHandle:
                    //UsbNotRegister_TrayHandle(pipeMsg?.Usb);
                    break;

                case NamedPipeMsgType.ToCloseApp_TrayHandle:
                    ToCloseApp_TrayHandle();
                    break;

                default:
                    break;
            }
        }

        private void Msg_TrayHandle(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, "From PipeServer");
            }
        }

        private void BalloonTip_TrayHandle(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                ServerManage_Tray.TrayIcon?.ShowBalloonTip(msg);
            }
        }

        private void UsbNotRegister_TrayHandle(UsbBase usbBase)
        {
            App.Current.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if (usbBase == null)
                    {
                        throw new Exception("NamedPipeClient_Tray.UsbNotRegister_TrayHandle(): usbBase == null");
                    }

                    var usbQRWin = new UsbQRCodeWindow();
                    usbQRWin.SetUSBInfo(usbBase);
                    usbQRWin.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "USBAdmin");
                }
            }));
        }

        private void ToCloseApp_TrayHandle()
        {
            try
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    App.Current.MainWindow.Close();
                });
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Send Message
        public void SendMsg_UpdateSetting()
        {
            try
            {
                NamedPipeMsg msg = new NamedPipeMsg
                {
                    MsgType = NamedPipeMsgType.UpdateSetting_ServerHandle
                };

                _client.PushMessage(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
