using System;

namespace ToolsCommon
{
    public interface IUsbLog : IUsbBase
    {

        string ComputerIdentity { get; set; }

        DateTime PluginTime { get; set; }

        string PluginTimeString { get; }
    }
}
