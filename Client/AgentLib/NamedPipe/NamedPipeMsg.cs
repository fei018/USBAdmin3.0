using System.Collections.Generic;
using System;

namespace AgentLib
{
    [Serializable]
    public class NamedPipeMsg
    {
        public NamedPipeMsgType MsgType { get; set; }

        public UsbBase Usb { get; set; }

        public string Message { get; set; }

        public int WinlogonSessionId { get; set; }
    }
}
