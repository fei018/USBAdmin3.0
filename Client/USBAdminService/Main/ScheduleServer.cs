using AgentLib;
using System;
using System.Timers;

namespace USBAdminService
{
    public class ScheduleServer : IAdminServer
    {
        private static Timer _Timer;

        private static double _Interval = 0;

        private void TaskAction(object sender, ElapsedEventArgs e)
        {
            #region PostComputerInfo
            try
            {
                new AgentHttpHelp().PostComputerInfo();
            }
            catch (Exception ex)
            {
                AgentLogger.Error("ServiceTimer.TaskAction(): " + ex.Message);
            }
            #endregion

            #region Agent CheckAndUpdate 
            try
            {
                if (AgentUpdate.CheckNeedUpdate())
                {
                    new AgentUpdate().Update();
                    return;
                }
            }
            catch (Exception ex)
            {
                AgentLogger.Error("ServerSchedule.TaskAction(): " + ex.Message);
            }
            #endregion

            #region update setting
            try
            {
                var http = new AgentHttpHelp();
                http.UpdateAgentRule();
                http.UpdateUSBWhitelist();
            }
            catch (Exception ex)
            {
                AgentLogger.Error("ServerSchedule.TaskAction(): " + ex.Message);
            }
            #endregion

            #region PrintJobLogServer
            try
            {
                if (AgentRegistry.PrintJobLogEnabled)
                {
                    if (ServerManage_Service.PrintJobLogServer == null)
                    {
                        ServerManage_Service.PrintJobLogServer = new PrintJobLogServer();
                        ServerManage_Service.PrintJobLogServer.Start();
                    }
                }
                else
                {
                    if (ServerManage_Service.PrintJobLogServer != null)
                    {
                        ServerManage_Service.PrintJobLogServer?.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                AgentLogger.Error("ServerSchedule.TaskAction(): " + ex.GetBaseException().Message);
            }
            #endregion

            #region timer TryReset();
            TryReset();
            #endregion
        }

        public void Start()
        {
            try
            {
                _Timer = new Timer
                {
                    AutoReset = true,
                    Interval = GetInterval()
                };

                _Interval = _Timer.Interval;

                _Timer.Elapsed += TaskAction;

                _Timer.Start();               
            }
            catch (Exception ex)
            {
                AgentLogger.Error("ServerSchedule.Start(): " + ex.Message);
            }
        }

        public void Stop()
        {
            if (_Timer != null)
            {
                try
                {
                    _Timer.Elapsed -= TaskAction;
                    _Timer.Stop();
                }
                catch (Exception)
                {
                }
            }
        }

        #region + private double GetInterval()
        private static double GetInterval()
        {
            int minutes;

            try
            {
                minutes = AgentRegistry.AgentTimerMinute;
            }
            catch (Exception)
            {
                return TimeSpan.FromMinutes(10).TotalMilliseconds;
            }

            // minimum 2 minutes
            if (minutes < 2)
            {
                minutes = 2;
            }

            // maximum 1 hours
            if (minutes > 60)
            {
                minutes = 60;
            }

            return TimeSpan.FromMinutes(minutes).TotalMilliseconds; ;
        }
        #endregion

        #region + private void TryReset()
        public void TryReset()
        {
            try
            {
                if (_Interval != GetInterval())
                {
                    ServerManage_Service.ScheduleServer?.Stop();
                    ServerManage_Service.ScheduleServer?.Start();
                }
            }
            catch (Exception ex)
            {
                AgentLogger.Error("ServerSchedule.Reset(): " + ex.Message);
            }
        }
        #endregion
    }
}
