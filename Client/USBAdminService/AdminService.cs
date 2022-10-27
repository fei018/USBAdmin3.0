using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using AgentLib;

namespace USBAdminService
{
    public partial class AdminService : ServiceBase
    {
        public AdminService()
        {
            InitializeComponent();

            this.CanHandleSessionChangeEvent = true;
        }

        protected override void OnStart(string[] args)
        {
            ServerManage_Service.OnStart();
        }

        protected override void OnStop()
        {
            ServerManage_Service.OnStop();
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);

            if (changeDescription.Reason == SessionChangeReason.SessionLogon)
            {               
                ServerManage_Service.TrayServer?.CreateTray();
            }
        }
    }
}
