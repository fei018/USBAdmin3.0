using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AgentLib;
using NamedPipeWrapper;

namespace USBAdminService
{
    public class NamedPipeServer : IAdminServer
    {
        private string _pipeName;

        private NamedPipeServer<NamedPipeMsg> _server;

        #region + public void Start()
        public void Start()
        {
            try
            {
                _pipeName = AgentRegistry.AgentHttpKey;

                if (string.IsNullOrWhiteSpace(_pipeName))
                {
                    AgentLogger.Error("NamedPipeServer.Start(): PipeName is empty");
                    return;
                }

                Stop();

                PipeSecurity pipeSecurity = new PipeSecurity();

                pipeSecurity.AddAccessRule(new PipeAccessRule("CREATOR OWNER", PipeAccessRights.FullControl, AccessControlType.Allow));
                pipeSecurity.AddAccessRule(new PipeAccessRule("SYSTEM", PipeAccessRights.FullControl, AccessControlType.Allow));

                // Allow Everyone read and write access to the pipe.
                pipeSecurity.AddAccessRule(
                            new PipeAccessRule(
                            "Authenticated Users",
                            PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance,
                            AccessControlType.Allow));

                _server = new NamedPipeServer<NamedPipeMsg>(_pipeName, pipeSecurity);

                _server.ClientMessage += _server_ClientMessage; ;

                _server.Error += pipeConnection_Error;

                _server.Start();
            }
            catch (Exception ex)
            {
                AgentLogger.Error("NamedPipeServer.Start(): " + ex.Message);
            }
        }

        private void pipeConnection_Error(Exception exception)
        {
            AgentLogger.Error("NamedPipeServe.pipeConnection_Error(): " + exception.Message);
        }
        #endregion

        #region + public void Stop()
        public void Stop()
        {
            try
            {
                if (_server != null)
                {
                    _server.Error -= pipeConnection_Error;
                    _server.ClientMessage -= _server_ClientMessage;
                    _server.Stop();
                    _server = null;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region ClientMessage
        private void _server_ClientMessage(NamedPipeConnection<NamedPipeMsg, NamedPipeMsg> connection, NamedPipeMsg pipeMsg)
        {
            if (pipeMsg == null)
            {
                AgentLogger.Error("NamedPipeServer.ClientMessage is null.");
            }

            switch (pipeMsg.MsgType)
            {
                case NamedPipeMsgType.UpdateSetting_ServerHandle:
                    UpdateSetting_ServerHandle();
                    break;

                case NamedPipeMsgType.UsbNotRegister_ServerForward:
                    UsbNotRegister_ServerForward(pipeMsg?.Usb);
                    break;

                default:
                    break;
            }
        }

        #region Handle

        private void UpdateSetting_ServerHandle()
        {
            #region agent update
            try
            {
                if (AgentUpdate.CheckNeedUpdate())
                {
                    new AgentUpdate().Update();
                    SendMsg_To_Tray_BalloonTip("Agent new version checked,\r\nwaiting for update.");
                    SendMsg_To_Tray_Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                AgentLogger.Error(ex.Message);
                SendMsg_To_Tray_Message(ex.Message);
            }
            #endregion

            StringBuilder error = new StringBuilder();

            #region update setting
            try
            {
                new AgentHttpHelp().UpdateAgentRule();
                ServerManage_Service.ScheduleServer?.TryReset();
            }
            catch (Exception ex)
            {
                AgentLogger.Error(ex.Message);
                error.AppendLine("Error: " + ex.Message);
            }

            try
            {
                new AgentHttpHelp().UpdateUSBWhitelist();
            }
            catch (Exception ex)
            {
                AgentLogger.Error(ex.Message);
                error.AppendLine("Error: " + ex.Message);
            }
            #endregion

            if (string.IsNullOrEmpty(error.ToString()))
            {
                SendMsg_To_Tray_BalloonTip("Update setting done.");
            }
            else
            {
                SendMsg_To_Tray_Message(error.ToString());
            }
        }

        private void UsbNotRegister_ServerForward(UsbBase usbBase)
        {
            try
            {
                if (usbBase == null)
                {
                    throw new Exception("NamedPipeServer.UsbNotRegister_ServerForward(UsbBase usbBase): usbBase is null");
                }

                NamedPipeMsg msg = new NamedPipeMsg
                {
                    MsgType = NamedPipeMsgType.UsbNotRegister_TrayHandle,
                    Usb = usbBase
                };
                _server.PushMessage(msg);
            }
            catch (Exception ex)
            {
                AgentLogger.Error(ex.Message);
                SendMsg_To_Tray_Message(ex.Message);
            }
        }
        #endregion

        #endregion

        #region Send Message
        public void SendMsg_To_Tray_Message(string text)
        {
            var newMsg = new NamedPipeMsg
            {
                MsgType = NamedPipeMsgType.MsgBox_TrayHandle,
                Message = text
            };
            _server.PushMessage(newMsg);
        }

        public void SendMsg_To_Tray_BalloonTip(string text)
        {
            var newMsg = new NamedPipeMsg
            {
                MsgType = NamedPipeMsgType.BalloonTip_TrayHandle,
                Message = text
            };
            _server.PushMessage(newMsg);
        }

        public void SendMsg_To_Tray_Close()
        {
            try
            {
                NamedPipeMsg msg = new NamedPipeMsg
                {
                    MsgType = NamedPipeMsgType.ToCloseApp_TrayHandle
                };
                _server.PushMessage(msg);
            }
            catch (Exception)
            {
            }
        }

        public void SendMsg_To_USBFilterForm_Close()
        {
            try
            {
                NamedPipeMsg msg = new NamedPipeMsg
                {
                    MsgType = NamedPipeMsgType.ToCloseApp_USBFilterFormHandle
                };
                _server.PushMessage(msg);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
