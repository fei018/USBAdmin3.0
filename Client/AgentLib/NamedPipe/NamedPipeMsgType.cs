using System;

namespace AgentLib
{
    [Serializable]
    public enum NamedPipeMsgType
    {
        MsgBox_TrayHandle = 10,
        BalloonTip_TrayHandle,
        UsbNotRegister_TrayHandle,
        UsbNotRegister_ServerForward,
        UpdateSetting_ServerHandle,
        ToCloseApp_TrayHandle,
        ToCloseApp_USBFilterFormHandle
    }
}
