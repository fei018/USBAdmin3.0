using System;
using System.Windows;

namespace USBAdminTray
{
    /// <summary>
    /// AboutWindow.xaml 的互動邏輯
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UniqueOpenWindow.AoutWindow = false;
        }
    }
}
