using AgentLib;
using System.Windows;

namespace USBAdminTray
{
    /// <summary>
    /// UsbQRCodeWindow.xaml 的互動邏輯
    /// </summary>
    public partial class UsbQRCodeWindow : Window
    {
        public UsbQRCodeWindow()
        {
            InitializeComponent();

            ShowWinLocationRightBottom();
        }

        /// <summary>
        /// 右下角 顯示 Window
        /// </summary>
        private void ShowWinLocationRightBottom()
        {
            this.Top = SystemParameters.WorkArea.Bottom - this.Height;
            this.Left = SystemParameters.WorkArea.Right - this.Width;
        }

        #region + public void SetUSBInfo(UsbDisk usbDisk)
        public void SetUSBInfo(UsbBase usb)
        {
            txtBrand.Text = usb.Manufacturer;
            txtProduct.Text = usb.Product;
            txtVid.Text = usb.Vid.ToString();
            txtPid.Text = usb.Pid.ToString();
            txtSerial.Text = usb.SerialNumber;

            string qrText = $"Manufacturer:{usb.Manufacturer}|Product:{usb.Product}|Vid:{usb.Vid}|Pid:{usb.Pid}|SerialNumber:{usb.SerialNumber}";

            imgUSBQRCode.Source = UsbQRCodeHelp.GenerateBitmapImage(qrText);
        }
        #endregion
    }
}
