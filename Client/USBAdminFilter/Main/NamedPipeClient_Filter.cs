using AgentLib;
using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USBAdminFilter
{
    public class NamedPipeClient_Filter
    {
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
                    AgentLogger.Error("NamedPipeClient_Filter.PipeName is empty");
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
                AgentLogger.Error("NamedPipeClient_Filter.Start(): " + ex.Message);
            }
        }
        #endregion

        private void _client_Error(Exception exception)
        {
            AgentLogger.Error("NamedPipeClient_Filter: " + exception.Message);
        }

        #region _client_ServerMessage
        private void _client_ServerMessage(NamedPipeConnection<NamedPipeMsg, NamedPipeMsg> connection, NamedPipeMsg pipeMsg)
        {
            if (pipeMsg == null)
            {
                AgentLogger.Error("NamedPipeClient_Filter._client_ServerMessage: pipeMsg == null.");
            }

            switch (pipeMsg.MsgType)
            {
                case NamedPipeMsgType.ToCloseApp_USBFilterFormHandle:
                    ToCloseProcess_USBFilterHandle();
                    break;

                default:
                    break;
            }
        }

        private void ToCloseProcess_USBFilterHandle()
        {
            try
            {
                USBFilterForm.USBFilterForm_App.Close();
            }
            catch (Exception ex)
            {
                AgentLogger.Error(ex.Message);
            }
        }
        #endregion

        #region Send message
        public void SendMsg_To_ServerForward_UsbNotRegister(UsbBase usbBase)
        {
            try
            {
                var msg = new NamedPipeMsg
                {
                    MsgType = NamedPipeMsgType.UsbNotRegister_ServerForward,
                    Usb = usbBase
                };

                _client.PushMessage(msg);
            }
            catch (Exception ex)
            {
                AgentLogger.Error(ex.Message);
            }
        }
        #endregion
    }
}
