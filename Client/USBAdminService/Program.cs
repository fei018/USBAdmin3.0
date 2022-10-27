using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace USBAdminService
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new AdminService()
            };
            ServiceBase.Run(ServicesToRun);

            //ServerManage_Service.OnStart();
            //Console.WriteLine("Start...");

            //Console.ReadLine();

            //ServerManage_Service.OnStop();
            //Console.WriteLine("Stop...");
        }
    }
}
