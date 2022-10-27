using AgentLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbMonitor;

namespace USBAdminFilter
{
    public partial class USBFilterForm : UsbMonitorForm
    {
        public static USBFilterForm USBFilterForm_App { get; private set; }

        public USBFilterForm()
        {
            InitializeComponent();
#if DEBUG
            Visible = true;
            ShowInTaskbar = true;
#else
            Visible = false;
            ShowInTaskbar = false;
#endif

            OnStart();
        }

        private UsbFilter _usbFilter;
        private NamedPipeClient_Filter _namedPipeClient_Filter;

        private void OnStart()
        {
            USBFilterForm_App = this;

            _namedPipeClient_Filter = new NamedPipeClient_Filter();
            _namedPipeClient_Filter.Start();

            _usbFilter = new UsbFilter();
            _usbFilter.UsbDeviceNotRegister += _usbFilter_UsbDeviceNotRegister;
        }

        private void USBFilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _usbFilter.UsbDeviceNotRegister -= _usbFilter_UsbDeviceNotRegister;

            _namedPipeClient_Filter.Stop();
        }

        private void _usbFilter_UsbDeviceNotRegister(object sender, UsbBase e)
        {
            // send message to tray
            _namedPipeClient_Filter?.SendMsg_To_ServerForward_UsbNotRegister(e);
        }

        #region OnUsbInterface
        private static readonly object _Locker = new object();

        public override void OnUsbInterface(UsbEventDeviceInterfaceArgs args)
        {
            if (args.Action == UsbDeviceChangeEvent.Arrival)
            {
                if (args.DeviceInterface == UsbMonitor.UsbDeviceInterface.Disk)
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            lock (_Locker)
                            {
                                _usbFilter.Filter_UsbDisk_By_DiskPath(args.Name);
                            }
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            // post usb log to http server
                            new AgentHttpHelp().PostUsbLog_byDisk(args.Name);
                        }
                        catch (Exception)
                        {
                        }
                    });
                }
            }
        }
        #endregion

    }
}
