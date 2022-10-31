using AgentLib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace USBAdminTray
{
    /// <summary>
    /// USBListWindow.xaml 的互動邏輯
    /// </summary>
    public partial class USBListWindow : Window
    {
        public USBListWindow()
        {
            InitializeComponent();

            this.Closed += USBListWindow_Closed;

            ShowWinLocationRightBottom();

            lv_ReadOnlyUSBList_Refresh();
        }

        /// <summary>
        /// 右下角 顯示 Window
        /// </summary>
        private void ShowWinLocationRightBottom()
        {
            this.Top = SystemParameters.WorkArea.Bottom - this.Height;
            this.Left = SystemParameters.WorkArea.Right - this.Width;
        }

        private void USBListWindow_Closed(object sender, EventArgs e)
        {
            UniqueOpenWindow.USBListWindow = false;
        }

        private void lv_ReadOnlyUSBList_Refresh()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    List<UsbDisk> usblist = new UsbFilter().Scan_All_USBDisk_Get_Details();
                    Dispatcher.Invoke(() =>
                    {
                        lv_ReadOnlyUSBList.ItemsSource = usblist;
                    });
                }
                catch (Exception)
                {
                }
            });
        }

        private void lv_ReadOnlyUSBList_cm_QRCode_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (lv_ReadOnlyUSBList.SelectedItem is UsbDisk usb)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        UsbQRCodeWindow usbQRWin = new UsbQRCodeWindow();
                        usbQRWin.SetUSBInfo(usb);
                        usbQRWin.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "USBAdmin");
                    }
                }));
            }
        }

        private void btnRefreshUSBList_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            lv_ReadOnlyUSBList_Refresh();
        }
    }
}
